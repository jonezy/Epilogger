using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using DevOne.Security.Cryptography.BCrypt;
using Epilogger.Common;
using Epilogger.Data;
using Epilogger.Web.Core.Email;
using Epilogger.Web.Core.Helpers;
using Epilogger.Web.Epilogr;
using Epilogger.Web.Models;
using Epilogger.Web.Views.Emails;
using Facebook;
using Links;
using RichmondDay.Helpers;
using Twitterizer;

namespace Epilogger.Web.Controllers {
    public partial class AccountController : BaseController {
        UserService _service;
        private UserAuthenticationProfileService _ua;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (_service == null) _service = new UserService();
            if (_ua == null) _ua = new UserAuthenticationProfileService();
            
            base.Initialize(requestContext);
        }

    /* New Account stuff section */
    #region New Account Stuff

        //These are used for Various account functions in a number of areas

        [HttpPost] //Called by Ajax
        public bool CheckUsername(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return false;
                }
                if (username=="Username")
                {
                    return false;
                }

                //Check if username exists
                return _service.IsUsernameAvailable(username);    
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost] //Called by Ajax
        public bool CheckEmailAddress(string emailaddress)
        {
            try
            {
                if (string.IsNullOrEmpty(emailaddress))
                {
                    return false;
                }
                if (emailaddress == "Email Address")
                {
                    return false;
                }

                //Check if username exists
                return _service.IsEmailAddressAvailable(emailaddress);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost] //Called by Ajax
        public virtual ActionResult UploadAvatar(string qqfile)
        {
            var path = @"C:\\";
            var file = string.Empty;

            Uri imageurl;

            try
            {
                var stream = Request.InputStream;
                if (String.IsNullOrEmpty(Request["qqfile"]))
                {
                    // IE
                    var postedFile = Request.Files[0];
                    if (postedFile != null) stream = postedFile.InputStream;
                    file = Path.Combine(path, System.IO.Path.GetFileName(Request.Files[0].FileName));
                }
                else
                {
                    //Webkit, Mozilla
                    file = Path.Combine(path, qqfile);
                }

                //Resize the image
                Stream resizedImage = null;
                Helpers.ResizeImageStream(stream, 250, 500, true, out resizedImage);

                //Azure Storage Code - Full Profile pic
                imageurl = AzureImageStorageHelper.StoreProfileImage("profileimage-" + Session.SessionID, "profilepicturesfull", resizedImage);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, "application/json");
            }

            return Json(new { success = true, imageurl = imageurl.AbsoluteUri }, "text/html");
        }

        //Processes the validation code when a user validates their email address.
        [HttpGet]
        public virtual ActionResult Validate(string validationCode)
        {
            if (string.IsNullOrEmpty(validationCode))
            {
                this.StoreError("The verification code couldn't be determined, please try clicking the link in your email again.");
                return View();
            }
            // decode the email address from base64
            // look up the user account using the email address
            // set EmailVerified = true and update, redirect to the home page.
            var userid = Guid.Parse(Helpers.base64Decode(validationCode));
            if (userid == Guid.Empty)
            {
                this.StoreError("The verificatoin code couldn't be determined, please try clicking the link in your email again.");
                return View();
            }

            var user = _service.GetUserByID(userid);
            if (user == null)
            {
                this.StoreError("We couldn't find an account to activate with that verification code.");
                return View();
            }

            user.EmailVerified = true;
            _service.Save(user);

            SendVerificationThankYouEmail(user);

            this.StoreSuccess("Your email address has been verified. You can go ahead and unleash epicness!");
            return RedirectToAction("Index", "Home");
        }

        //Sends the Thank you email after user validates their email address
        private static void SendVerificationThankYouEmail(User user)
        {
            //Send the Validate Email Thank You

            //Build the trending list
            var es = new EventService();
            var imgs = new ImageService();
            
            var trending = new StringBuilder();

            //trending.Append("<ul style='margin:20px 0 0 0; padding: 0; list-style: none;'>");

            //foreach (var item in es.GetHomepageActivity().Where(e=> e.ActivityType==ActivityType.PHOTOS_VIDEOS).Take(6))
            //{
            //    item.Image = imgs.FindByID(Convert.ToInt32(item.ActivityContent));
                
            //    trending.Append(
            //        string.Format("<li style='display:inline-block;width:150px;overflow:hidden;margin-right: 20px;'><a href='{0}'>{1}<br /><img src='{2}' width='150px' alt='' /></a></li>", "http://epilogger.com/events/" + item.EventSlug, item.EventName, item.Image.Fullsize));
                
            //}

            //trending.Append("</ul>");

            trending.Append("<table style='margin:20px 0 0 0; padding: 0; list-style: none;'>");

            int i = 1;
            foreach (var item in es.GetHomepageActivity().Where(e => e.ActivityType == ActivityType.PHOTOS_VIDEOS).Take(6))
            {
                if (i == 1) { trending.Append("<tr valign='top'>"); }

                item.Image = imgs.FindByID(Convert.ToInt32(item.ActivityContent));
                
                trending.Append(
                    string.Format("<td style='width:150px;overflow:hidden;padding-right: 20px;' ><a href='{0}'>{1}<br /><img src='{2}' width='150px' alt='' /></a></td>", "http://epilogger.com/events/" + item.EventSlug, item.EventName, item.Image.Fullsize));

                if (i == 2) { trending.Append("</tr>");
                    i = 1;
                } else
                {
                    i++;
                }

            }

            trending.Append("</table>");



            var parser = new TemplateParser();
            var replacements = new Dictionary<string, string>
                                   {
                                       {"[FIRST_NAME]", user.FirstName},
                                       {"[year]", DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) },
                                       {"[TRENDING_EVENTS]", trending.ToString()}
                                   };

            var message = parser.Replace(AccountEmails.ValidateEmailThankYou, replacements);

            var sfEmail = new SpamSafeMail
            {
                EmailSubject = "Epilogger.com - Thank You for validating your email address",
                HtmlEmail = message,
                TextEmail = message
            };
            sfEmail.ToEmailAddresses.Add(user.EmailAddress);
            sfEmail.SendMail();
        }

        public static bool ConnectAuthAccountToUser(Guid userId, string authService, string authScreenName, string authToken, string authTokenSecret, string platform)
        {
            try
            {
                if (userId == Guid.Empty) return false;
                if (string.IsNullOrEmpty(authService)) return false;
                if (string.IsNullOrEmpty(authScreenName)) return false;
                if (string.IsNullOrEmpty(authToken)) return false;
                if (string.IsNullOrEmpty(platform)) return false;


                var userAuthService = new UserAuthenticationProfileService();
                UserAuthenticationProfile userAuth = null;
            
                switch (authService.ToLower())
                {
                    case "twitter":
                        {
                            userAuth = userAuthService.UserAuthorizationByServiceScreenNameAndPlatform(authScreenName, platform, AuthenticationServices.TWITTER);
                            break;
                        }
                    case "facebook":
                        {
                            userAuth = userAuthService.UserAuthorizationByServiceScreenNameAndPlatform(authScreenName, platform, AuthenticationServices.FACEBOOK);
                            break;
                        }
                }

                if (userAuth == null)
                {
                    //Store the auth tokens for this user
                    userAuth = new UserAuthenticationProfile
                    {
                        UserID = userId,
                        Platform = platform,
                        ServiceUsername = authScreenName,
                        Token = authToken,
                        TokenSecret = authTokenSecret
                    };

                    switch (authService.ToLower())
                    {
                        case "twitter":
                            {
                                userAuth.Service = AuthenticationServices.TWITTER.ToString();
                                break;
                            }
                        case "facebook":
                            {
                                userAuth.Service = AuthenticationServices.FACEBOOK.ToString();
                                break;
                            }
                    }

                    new UserAuthenticationProfileService().Save(userAuth);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        
        public Guid CheckConnectedAccountUserExists(string userScreenName, AuthenticationServices authService, string token, string tokenSecret)
        {
            try
            {

                var userAuthService = new UserAuthenticationProfileService();
                var userAuth = userAuthService.UserAuthorizationByServiceScreenNameAndPlatform(userScreenName, "Web", authService);
                if (userAuth != null)
                {
                    string logInMethod = null;
                    switch (authService)
                    {
                        case AuthenticationServices.TWITTER:
                            userAuth.Token = token;
                            userAuth.TokenSecret = tokenSecret;
                            logInMethod = "Twitter";
                            break;
                        case AuthenticationServices.FACEBOOK:
                            userAuth.Token = token;
                            logInMethod = "Facebook";
                            break;
                    }
                    userAuthService.Save(userAuth);

                    var user = userAuth.Users.FirstOrDefault();
                    if (user != null)
                    {

                        //Record the Login
                        var ut = new UserLoginTracking()
                        {
                            UserId = user.ID,
                            LoginMethod = logInMethod,
                            DateTime = DateTime.UtcNow,
                            IPAddress = HttpContext.Request.UserHostAddress
                        };
                        new UserLoginTrackingService().Save(ut);
                        return user.ID;
                    }
                }

                return Guid.Empty;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        [RequiresAuthentication(ValidUserRole = UserRoleType.RegularUser, AccessDeniedMessage = "You must be logged in to edit your account")]
        public virtual ActionResult Index()
        {
            var model = Mapper.Map<User, AccountModel>(CurrentUser);
            
            model.Password = string.Empty;
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Index(AccountModel model, FormCollection c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = CurrentUser;
                    
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.DateOfBirth = DateTime.Parse(model.DateOfBirth);

                    user.ProfilePicture = model.ProfilePicture;
                    user.ProfilePictureLarge = model.ProfilePictureLarge;

                    if (user.EmailAddress != model.EmailAddress)
                    {
                        user.EmailVerified = false;
                    }
                    user.EmailAddress = model.EmailAddress;
                    

                    if (model.Password != null)
                    {
                        user.Password = PasswordHelpers.EncryptPassword(model.Password);
                    }
                    
                    _service.Save(user);

                    this.Receive(MessageType.Success, "Your account was updated successfully");

                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    this.StoreError("There was a problem updating your account");
                }
            }
            return View(model);
        }


        public virtual ActionResult GetProfileOptions()
        {
            var model = new ProfileImageChoiceViewModel();

            var connectedNetworks = _ua.UserAuthorizationBPlatformAndUserId("Web", CurrentUserID);
            foreach (var cn in connectedNetworks)
            {
                if (cn.Service == AuthenticationServices.TWITTER.ToString())
                {
                    //Get the profile pic from Twitter
                    try
                    {
                        var twitterUser = TwitterHelper.GetUser(cn.Token, cn.TokenSecret, cn.ServiceUsername);
                        model.TwitterProfilePicture = twitterUser.ResponseObject.ProfileImageLocation;

                        var req = (HttpWebRequest)WebRequest.Create(string.Format("https://api.twitter.com/1/users/profile_image?screen_name={0}&size=original", twitterUser.ResponseObject.ScreenName));
                        req.Method = "HEAD";
                        var myResp = (HttpWebResponse)req.GetResponse();
                        model.TwitterProfilePictureLarge = myResp.StatusCode == HttpStatusCode.OK ? myResp.ResponseUri.AbsoluteUri : twitterUser.ResponseObject.ProfileImageLocation;
                    }
                    catch (Exception)
                    {
                        model.TwitterProfilePicture = null;
                    }
                }
                if (cn.Service == AuthenticationServices.FACEBOOK.ToString())
                {
                    //Get the profile pic from Facebook
                    try
                    {
                        model.FacebookProfilePicture = FacebookHelper.GetProfilePicture(cn.Token);
                        model.FacebookProfilePictureLarge = FacebookHelper.GetProfilePictureWithSize(cn.Token, "large");
                    }
                    catch (Exception)
                    {
                        model.FacebookProfilePicture = null;
                    }
                }
            }

            return PartialView("_profileImageChoice", model);

        }

        public virtual ActionResult GetTwitterConnect()
        {
            try
            {
                var connectionProfile = _ua.UserAuthorizationByServicePlatformAndUserId(AuthenticationServices.TWITTER, "Web", CurrentUserID);
                if (connectionProfile != null)
                {
                    var twitterUser = TwitterHelper.GetUser(connectionProfile.Token,
                                                        connectionProfile.TokenSecret,
                                                        connectionProfile.ServiceUsername);
                    if (twitterUser != null)
                    {
                        var splitName = twitterUser.ResponseObject.Name.Split(new[] { " " }, StringSplitOptions.None);

                        return PartialView("_DisconnectService", new DisconnectServiceViewModel() { ServiceName = "Twitter", ConnectedAs = string.Format("{0} {1}", splitName[0], splitName[1]) });
                    }
                }
            }
            catch (Exception)
            {
            }
            
            return PartialView("_ConnectService", new ConnectServiceViewModel() { ServiceName = "Twitter", ServiceUseDescription = "Tweet and retweet<br />Favorite items" });
        }

        public virtual ActionResult GetFacebookConnect()
        {
            try
            {
                var connectionProfile = _ua.UserAuthorizationByServicePlatformAndUserId(AuthenticationServices.FACEBOOK, "Web", CurrentUserID);
                if (connectionProfile != null)
                {
                    var fbClient = new FacebookClient(connectionProfile.Token);
                    dynamic facebookUser = fbClient.Get("me");

                    if (facebookUser != null)
                    {
                        return PartialView("_DisconnectService", new DisconnectServiceViewModel() { ServiceName = "Facebook", ConnectedAs = string.Format("{0} {1}", facebookUser.first_name, facebookUser.last_name) });    
                    }
                }
            }
            catch (Exception)
            {
            }
            
            return PartialView("_ConnectService", new ConnectServiceViewModel() { ServiceName = "Facebook", ServiceUseDescription = "Like items<br />&nbsp;" });
        }

        public virtual ActionResult TwitterAuth()
        {
            if (Request.QueryString["oauth_token"] != null)
            {
                var accessTokenResponse = OAuthUtility.GetAccessTokenDuringCallback(TwitterHelper.TwitterConsumerKey,
                                                                                    TwitterHelper.TwitterConsumerSecret);

                // check to see if we already have the screen name in the userauth table.
                var userAuthService = new UserAuthenticationProfileService();
                var userAuth = userAuthService.UserAuthorizationByServiceScreenNameAndPlatform(accessTokenResponse.ScreenName,
                                                                                    "Web",
                                                                                    AuthenticationServices.TWITTER);

                if (userAuth == null)
                {
                    //This is a connection to Twitter
                    userAuth = new UserAuthenticationProfile
                    {
                        UserID = CurrentUserID,
                        Platform = "Web",
                        Service = AuthenticationServices.TWITTER.ToString(),
                        ServiceUsername = accessTokenResponse.ScreenName,
                        Token = accessTokenResponse.Token,
                        TokenSecret = accessTokenResponse.TokenSecret
                    };
                    userAuthService.Save(userAuth);
                }
            }

            return View();
        }

        public virtual ActionResult FacebookAuth()
        {
            var client = new FacebookClient();
            var oauthResult = client.ParseOAuthCallbackUrl(Request.Url);

            // Build the Return URI form the Request Url
            var redirectUri = new UriBuilder(Url.Action("FacebookAuth", "account", null, "http"));

            // Exchange the code for an access token
            dynamic result = client.Get("/oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FacebookAppId"],
                redirect_uri = redirectUri.Uri.AbsoluteUri,
                client_secret = ConfigurationManager.AppSettings["FacebookAppSecret"],
                code = oauthResult.Code,
            });

            // Read the auth values
            string accessToken = result.access_token;
            DateTime expires = DateTime.UtcNow.AddSeconds(Convert.ToDouble(result.expires));

            var fbClient = new FacebookClient(accessToken);
            dynamic facebookUser = fbClient.Get("me");

            var authorizationService = new UserAuthenticationProfileService();
            var userAuth = authorizationService.UserAuthorizationByServiceScreenNameAndPlatform(facebookUser.username, "Web", AuthenticationServices.FACEBOOK);

            if (userAuth == null)
            {
                userAuth = new UserAuthenticationProfile
                    {
                        UserID = CurrentUserID,
                        Platform = "Web",
                        Service = AuthenticationServices.FACEBOOK.ToString(),
                        Token = accessToken,
                        ServiceUsername = facebookUser.username
                    };
            
                authorizationService.Save(userAuth);

            }
            
            return View();
        }

        [HttpGet]
        public virtual ActionResult Login()
        {
            CookieHelpers.DestroyCookie("lc");

            var model = new LoginModel() {ReturnUrl = Request.UrlReferrer};

            // store this here so that we can redirect the user back later.
            if (Request.QueryString["returnUrl"] != null)
                TempData["returnUrl"] = Request.QueryString["returnUrl"];

            //If this is open in a Window, this disables the Epilogger Header/Footer
            if (Request.QueryString["NoFrame"] != null)
            {
                model.InPopUp = true;
            }

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var user = _service.GetUserByUsername(model.Username);

                if (user == null)
                {
                    user = _service.GetUserByEmail(model.Username);
                    if (user==null)
                    {
                        ModelState.AddModelError(string.Empty, "There is a problem with your username or password. Please try again or <a href='/join/signup'/>create an account</a>.");
                        return View(model);   
                    }
                }

                // the user is a valid user but they need to update there password.
                if (user.Password == string.Empty)
                {
                    CookieHelpers.WriteCookie("lc", "tempid", user.ID.ToString());
                    return RedirectToAction("UpdatePassword");
                }

                if (!BCryptHelper.CheckPassword(model.Password, user.Password))
                {
                    ModelState.AddModelError(string.Empty, "There is a problem with your username or password. Please try again or <a href='/join/signup'/>create an account</a>.");
                    return View(model);
                }

                //Always remember
                CookieHelpers.WriteCookie("lc", "uid", user.ID.ToString(), DateTime.Now.AddDays(30));
                CookieHelpers.WriteCookie("lc", "tz", user.TimeZoneOffSet.ToString(), DateTime.Now.AddDays(30));
                

                //if (TempData["returnUrl"] != null)
                //    return Redirect(TempData["returnUrl"].ToString());

                //Record the Login
                var ut = new UserLoginTracking()
                {
                    UserId = user.ID,
                    LoginMethod = "Epilogger Account",
                    DateTime = DateTime.UtcNow,
                    IPAddress = HttpContext.Request.UserHostAddress
                };
                new UserLoginTrackingService().Save(ut);


                if (model.InPopUp)
                {
                    //Check if this user has twitter connected. If not, redirect to twitter connect.
                    var connectionProfile = _ua.UserAuthorizationByServicePlatformAndUserId(AuthenticationServices.TWITTER, "Web", Guid.Parse(user.ID.ToString()));
                    if (connectionProfile == null)
                    {
                        return RedirectToAction("ConnectTwitter");
                    }
                    return RedirectToAction("CloseAndRefresh");
                }
                    

                if (model.ReturnUrl != null)
                    return Redirect(model.ReturnUrl.AbsoluteUri);

                return RedirectToAction("index", "home");

            }

            return View(model);
        }


        [HttpGet]
        public virtual ActionResult CloseAndRefresh()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult ConnectTwitter()
        {
            return View();
        }

    #endregion











        #region Old Account Stuff

        



        






        







        //[HttpGet]
        //public virtual ActionResult Create() {
        //    return View(new CreateAccountModel());
        //}

        //[HttpPost]
        //public virtual ActionResult Create(CreateAccountModel model)
        //{
        //    //Twitter Auth Token is authed through twitter
        //    //Request.QueryString["oauth_token"]


        //    if (ModelState.IsValid) {
        //        // TEMP: check to make sure the email address provided is in the beta invite table.
        //        //if(!service.IsBetaUser(model.EmailAddress)) {
        //        //    this.StoreError("You must be invited to use the epilogger alpha, please click the create account link in your invite email to create your account, if you would like to be invited email team@epilogger.com");
        //        //    return View(model);
        //        //}

        //        //Check to see if this account already exists
        //        var userTest = service.GetUserByUsername(model.Username);
        //        if (userTest != null)
        //        {
        //            ModelState.AddModelError(string.Empty, "The username " + userTest.Username + " is already in use, please try another username");
        //            model.Username = "";
        //            return View(model);
        //        }


        //        //Ensure the User is over 13
        //        if (model.DateOfBirth != null && (DateTime.Now - DateTime.Parse(model.DateOfBirth)).Days / 366 < 13)
        //        {
        //            this.StoreError("You must be must be 13 years of age or older to use Epilogger.");
        //            ModelState.AddModelError(string.Empty, "You must be must be 13 years of age or older to use Epilogger.");
        //            return View(model);
        //        }
        //        //Ensure the passwords match
        //        //if (model.Password != model.ConfirmPassword)
        //        //{
        //        //    this.StoreError("The passwords you entered do not match.");
        //        //    ModelState.AddModelError(string.Empty, "The passwords you entered do not match.");
        //        //    return View(model);
        //        //}

        //        User user = null;
        //        try {
        //            user = Mapper.Map<CreateAccountModel, User>(model);
        //            user.IsActive = false; // ensure this is set.
        //            service.Save(user);

        //            // build a message to send to the user.
        //            //string validateUrl = string.Format("{0}account/validate/{1}", App.BaseUrl, Helpers.base64Encode(user.EmailAddress));
                    
        //            //Changed to UserID GUID to prevent problems with duplicate email addresses.
        //            string validateUrl = string.Format("{0}account/validate/{1}", App.BaseUrl, Helpers.base64Encode(user.ID.ToString()));

        //            var parser = new TemplateParser();
        //            Dictionary<string, string> replacements = new Dictionary<string, string>
        //                                                          {
        //                                                              {"[BASE_URL]", App.BaseUrl},
        //                                                              {"[FIRST_NAME]", user.EmailAddress},
        //                                                              {"[VALIDATE_ACCOUNT_URL]", validateUrl}
        //                                                          };

        //            string message = parser.Replace(AccountEmails.ValidateAccount, replacements);

        //            var sfEmail = new SpamSafeMail
        //            {
        //                EmailSubject = "Thank you for registering on epilogger.com",
        //                HtmlEmail = message,
        //                TextEmail = message
        //            };
        //            sfEmail.ToEmailAddresses.Add(model.EmailAddress);

        //            sfEmail.SendMail();
                    
        //            this.StoreSuccess("Your account was created successfully<br /><br/>Please check your inbox for our validation message, your account will be inaccessable until you validate it.");

        //            CookieHelpers.WriteCookie("lc", "tempid", user.ID.ToString());

        //            return RedirectToAction("AccountActivationNeeded", "account");

        //        } catch (Exception ex) {
        //            this.StoreError("There was a problem creating your account");
        //            if (user != null) service.DeleteUser(user.ID);
        //            return View(model);
        //        }
        //    }
        //    return View(model);
        //}

        

        //[HttpPost]
        //public ActionResult Update(AccountModel model, FormCollection c) {
            
        //    try
        //    {
        //        User user = CurrentUser;
        //        user.FirstName = model.FirstName;
        //        user.LastName = model.LastName;
        //        user.EmailAddress = model.EmailAddress;
        //        user.DateOfBirth = DateTime.Parse(model.DateOfBirth);
        //        //user.TimeZoneOffSet = model.TimeZone;

        //        string imagePath = string.Empty;
        //        if (c["ProfilePictures"] != null)
        //        {
        //            string pictureProvider = c["ProfilePictures"] as string ?? "";

        //            if (pictureProvider.ToLower().Contains("twitter"))
        //            {
        //                imagePath =
        //                    TwitterHelper.GetUser(CurrentUserTwitterAuthorization.Token,
        //                                            CurrentUserTwitterAuthorization.TokenSecret,
        //                                            CurrentUserTwitterAuthorization.ServiceUsername).ResponseObject.
        //                        ProfileImageLocation;
        //            }
        //            else if (pictureProvider.ToLower().Contains("facebook"))
        //            {
        //                imagePath = FacebookHelper.GetProfilePicture(CurrentUserFacebookAuthorization.Token);
        //            }
        //        }
        //        user.ProfilePicture = imagePath;

        //        service.Save(user);

        //        //this.StoreSuccess("Your account was updated successfully");
        //        this.Receive(MessageType.Success, "Your account was updated successfully");

        //    }
        //    catch (Exception ex)
        //    {
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //        this.StoreError("There was a problem updating your account");
        //    }
            
        //    return RedirectToAction("Index");
        //}

        

        // GET: Account/TwitterLogon/
        public virtual ActionResult TwitterLogon(string oauth_token, string oauth_verifier, string ReturnUrl)
        {

            if (string.IsNullOrEmpty(oauth_token) || string.IsNullOrEmpty(oauth_verifier))
            {
                var httpRequestBase = Request;
                if (httpRequestBase != null)
                {
                    if (httpRequestBase.Url != null)
                    {
                        var builder = new UriBuilder(httpRequestBase.Url);
                        builder.Query = string.Concat(
                            builder.Query,
                            string.IsNullOrEmpty(builder.Query) ? string.Empty : "&",
                            "ReturnUrl=",
                            ReturnUrl);

                        var token = OAuthUtility.GetRequestToken(
                            ConfigurationManager.AppSettings["TwitterConsumerKey"],
                            ConfigurationManager.AppSettings["TwitterConsumerSecret"],
                            builder.ToString()).Token;

                        return Redirect(OAuthUtility.BuildAuthorizationUri(token, true).ToString());
                    }
                }
            }

            var tokens = OAuthUtility.GetAccessToken(
                ConfigurationManager.AppSettings["TwitterConsumerKey"],
                ConfigurationManager.AppSettings["TwitterConsumerSecret"],
                oauth_token,
                oauth_verifier);


            var _us = new UserService();
            var _uaps = new UserAuthenticationProfileService();

            var userAuthByScreenName = _uaps.UserAuthorizationByServiceScreenName(tokens.ScreenName).ToList();

            if (userAuthByScreenName.Count > 0)
            {
                //This person has never logged in before. Not on the web or on Mobile.

                //No auth exists. This a new user, create the account and redirect to the profile page.
                var theNewUser = new User()
                                     {
                                         CreatedDate = DateTime.UtcNow,
                                         IsActive = true,
                                         RoleID = 2,
                                         Username = tokens.ScreenName + "twitter",
                                         Password = " ",
                                         EmailAddress = " "
                                     };
                _us.Save(theNewUser);

                //Put the auth in the auth table.
                var theAuth = new UserAuthenticationProfile
                                  {
                                      UserID = theNewUser.ID,
                                      Platform = "Web",
                                      Service = "TWITTER",
                                      ServiceUsername = tokens.ScreenName,
                                      Token = tokens.Token,
                                      TokenSecret = tokens.TokenSecret
                                  };
                _uaps.Save(theAuth);

                CookieHelpers.WriteCookie("lc", "uid", theNewUser.ID.ToString());
                CookieHelpers.WriteCookie("lc", "tz", theNewUser.TimeZoneOffSet.ToString(CultureInfo.InvariantCulture));

                return RedirectToAction("Index");
            }


            //This user already has a token, log them in.
            var userAuthenticationProfile = userAuthByScreenName.FirstOrDefault(u => u.Platform == "Web");
            if (userAuthenticationProfile != null)
            {
                var theUser = _us.GetUserByID(userAuthenticationProfile.UserID);
                // write the login cookie, redirect. 
                CookieHelpers.WriteCookie("lc", "uid", theUser.ID.ToString());
                CookieHelpers.WriteCookie("lc", "tz", theUser.TimeZoneOffSet.ToString(CultureInfo.InvariantCulture));
            }



            //var web = false;
            //var mobile = false;
            //foreach (var item in userAuthByScreenName)
            //{
            //    if (item.Platform == "Web") web = true;
            //    if (item.Platform == "Mobile") mobile = true;
                
            //}

            //if (mobile && !web)
            //{
            //    //Only mobile, no web.

            //}



            //if (web)
            //{
            //    ////This user already has a token, log them in.
            //    //var userAuthenticationProfile = userAuthByScreenName.FirstOrDefault(u => u.Platform == "Web");
            //    //if (userAuthenticationProfile != null)
            //    //{
            //    //    var theUser = _us.GetUserByID(userAuthenticationProfile.UserID);
            //    //    // write the login cookie, redirect. 
            //    //    CookieHelpers.WriteCookie("lc", "uid", theUser.ID.ToString());
            //    //    CookieHelpers.WriteCookie("lc", "tz", theUser.TimeZoneOffSet.ToString(CultureInfo.InvariantCulture));
            //    //}
            //}


            return RedirectToAction("index", "home");



            //if (string.IsNullOrEmpty(ReturnUrl))
            //    return Redirect("/");
            //else
            //    return Redirect(ReturnUrl);
        }





        public virtual ActionResult Logout() {
            CookieHelpers.DestroyCookie("lc");
            
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);

            return RedirectToAction("index", "Home");
        }

        [HttpGet]
        public virtual ActionResult ForgotPassword() {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult ForgotPassword(ForgotPasswordViewModel model) {
            try {
                var passwordResetHash = Guid.NewGuid();
                var user = _service.GetUserByEmail(model.EmailAddress.Trim());
                if (user == null) {
                    this.StoreWarning("There is no account on epilogger.com that uses that email address");
                    return View();
                }

                user.ForgotPasswordHash = passwordResetHash;
                _service.Save(user);

                string resetPasswordUrl = string.Format("{0}account/resetpassword?hash={1}", App.BaseUrl, passwordResetHash);

                var parser = new TemplateParser();
                var replacements = new Dictionary<string, string>();
                replacements.Add("[BASE_URL]", App.BaseUrl);
                replacements.Add("[USERNAME]", user.Username);
                replacements.Add("[USER_EMAIL]", user.EmailAddress);
                replacements.Add("[RESET_PASSWORD_URL]", resetPasswordUrl);

                var message = parser.Replace(AccountEmails.ForgotPassword, replacements);

                var sfEmail = new SpamSafeMail
                {
                    EmailSubject = "Reset your password on epilogger.com",
                    HtmlEmail = message,
                    TextEmail = message
                };
                sfEmail.ToEmailAddresses.Add(model.EmailAddress);

                sfEmail.SendMail();



                this.StoreSuccess("We\\'ve emailed you instructions on how to reset your password.");
            } catch (Exception ex) {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StoreError("There was a problem sending you instructions to reset your password");
            }

            return View();
        }

        [HttpGet]
        public virtual ActionResult ResetPassword() {
            Guid passwordResetHash = Guid.Parse(Request.QueryString["hash"].ToString());
            User user = _service.GetUserByResetHash(passwordResetHash);
            if (user == null) {
                this.StoreWarning("Something isn't right, the user didn't request this action?");
                return View();
            }

            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.UserID = user.ID.ToString();

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult ResetPassword(ResetPasswordViewModel model) {
            try {
                User user = _service.GetUserByID(Guid.Parse(model.UserID));
                user.Password = PasswordHelpers.EncryptPassword(model.NewPassword);
                user.ForgotPasswordHash = null;

                _service.Save(user);
                
                this.StoreSuccess("Your password has been updated, you can now login!");

                return RedirectToAction("login");
            } catch (Exception ex) {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StoreError("There was a problem updating your password");
                return View();
            }
        }

        [HttpGet]
        public virtual ActionResult UpdatePassword() {
            return View(new UpdatePasswordModel());
        }

        [HttpPost]
        public virtual ActionResult UpdatePassword(UpdatePasswordModel model) {
            if (ModelState.IsValid) {
                // update password.
                string salt = BCryptHelper.GenerateSalt();
                string password = BCryptHelper.HashPassword(model.NewPassword, salt);
                User user = _service.GetUserByID(CurrentUserID);
                user.Password = password;

                _service.Save(user);

                this.StoreSuccess("Your password has been updated, please login using the password you just created.");

                return RedirectToAction("Login");
                
            } 

            return View();
        }


        //public virtual ActionResult AccountActivationNeeded()
        //{
        //    return View();
        //}



        public virtual ActionResult ActivationSent()
        {

            var userId = new Guid();
            try
            {
                Guid.TryParse(CookieHelpers.GetCookieValue("lc", "tempid").ToString(), out userId);
            }
            catch (Exception)
            {
            }

            var uservice = new UserService();
            var theUser = uservice.GetUserByID(userId);

            // build a message to send to the user.
            string validateUrl = string.Format("{0}account/validate/{1}", App.BaseUrl, Helpers.base64Encode(theUser.ID.ToString()));

            var parser = new TemplateParser();
            Dictionary<string, string> replacements = new Dictionary<string, string>
                                                                  {
                                                                      {"[BASE_URL]", App.BaseUrl},
                                                                      {"[FIRST_NAME]", theUser.EmailAddress},
                                                                      {"[VALIDATE_ACCOUNT_URL]", validateUrl}
                                                                  };

            var message = parser.Replace(AccountEmails.OldValidateAccount, replacements);

            var sfEmail = new SpamSafeMail
                              {
                                  EmailSubject = "Thank you for registering on epilogger.com",
                                  HtmlEmail = message,
                                  TextEmail = message
                              };
            sfEmail.ToEmailAddresses.Add(theUser.EmailAddress);

            sfEmail.SendMail();
            
            return View();
        }

        //public virtual ActionResult TwitterAuthTest()
        //{

        //    var apiClient = new APISoapClient();
        //    var container = apiClient.CreateUrl("http://www.google.com");


            

        //    //var urlRequestBody = new Epilogr.CreateUrlRequestBody("http://www.google.com");
        //    //var urlRequest = new Epilogr.CreateUrlRequest(urlRequestBody);

        //    //var test = new Epilogr.CreateUrlResponse(new CreateUrlResponseBody());
           
        //    //var containdter = new Epilogr.Container();
            
        //    //var urlResponseBody = new CreateUrlResponseBody();
        //    //var mytest = new Epilogr.CreateUrlResponse(urlResponseBody);
            

        //    //Epilogr.CreateUrlResponse =

        //    var test = "test";

        //    return View();
        //}

        //[HttpPost]
        //public virtual ActionResult TwitterAuthTest(TwitterAuthTestViewModel model)
        //{

        //    var tokens = new OAuthTokens { ConsumerKey = "qV0GasfpuvDRmXhnaDA", ConsumerSecret = "q3ftmYti8d4ws2iNieidofWYLswdHT3BRwmu813EA", AccessToken = "280687481-XyZq3P6v7qivApsYjES8V3LjTRgcRZIx2XRO755V", AccessTokenSecret = "Z7MG32TDldpx5USDdOUXjzsop1ZtaEbLMm1bzTnuk" };
        //    TwitterResponse<TwitterStatus> tweetResponse = TwitterStatus.Update(tokens, "Test");

        //    return View();
        //}

        #endregion

    }
}
