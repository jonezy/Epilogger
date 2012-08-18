using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

using AutoMapper;

using DevOne.Security.Cryptography.BCrypt;
using Epilogger.Common;
using Epilogger.Data;
using Epilogger.Web.Core.Email;
using Epilogger.Web.Epilogr;
using Epilogger.Web.Models;
using Epilogger.Web.Views.Emails;
using Links;
using RichmondDay.Helpers;
using Twitterizer;

namespace Epilogger.Web.Controllers {
    public partial class AccountController : BaseController {
        UserService service;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (service == null) service = new UserService();

            base.Initialize(requestContext);
        }

        /* New Account stuff section */
        #region New Account Stuff

        /* Sign up page Step 1 or 2 */
        public virtual ActionResult Signup()
        {
            return View();
        }


        /* Create Account with Twitter Button */
        [HttpGet]
        public virtual ActionResult Twitter()
        {
            try
            {
                var twitterAuth = Request.QueryString["oauth_token"];
                if (String.IsNullOrEmpty(twitterAuth))
                {
                    this.StoreError("There was a problem connecting to your twitter account");
                    return RedirectToAction("Signup");
                }

                /****** FOR DEBUGGING *****/
                //var accessTokenResponse = OAuthUtility.GetAccessTokenDuringCallback(TwitterHelper.TwitterConsumerKey, TwitterHelper.TwitterConsumerSecret);

                //var twitterUser = TwitterHelper.GetUser(accessTokenResponse.Token,
                //                                        accessTokenResponse.TokenSecret,
                //                                        accessTokenResponse.ScreenName);


                var accessTokenResponse = new OAuthTokenResponse();
                var twitterUser = TwitterHelper.GetUser("8181322-SlGwMhPRfg1yhDxpnMSidFSG5yyhvvAX8wDra6linE",
                                                        "NujAswPuqFYYTaKaqoxtAABiaaj1unkgfbhU2oleLyo",
                                                        "Cbrooker");
                
                /**********************/

                if (twitterUser == null)
                {
                    this.StoreError("There was a problem connecting to your twitter account");
                    return RedirectToAction("Signup");
                }

                //Check if this user already exists.
                //var userAuthService = new UserAuthenticationProfileService();
                //var userAuth = userAuthService.UserAuthorizationByServiceScreenNameAndPlatform(accessTokenResponse.ScreenName, "Web", AuthenticationServices.TWITTER);
                //if (userAuth != null) {
                //    userAuth.Token = accessTokenResponse.Token;
                //    userAuth.TokenSecret = accessTokenResponse.TokenSecret;
                //    userAuthService.Save(userAuth);

                //    var user = userAuth.Users.FirstOrDefault();
                //    if (user != null)
                //    {
                //        CookieHelpers.WriteCookie("lc", "uid", user.ID.ToString());
                //        CookieHelpers.WriteCookie("lc", "tz", user.TimeZoneOffSet.ToString(CultureInfo.InvariantCulture));

                //        //Record the Login
                //        var ut = new UserLoginTracking()
                //        {
                //            UserId = user.ID,
                //            LoginMethod = "Twitter",
                //            DateTime = DateTime.UtcNow,
                //            IPAddress = HttpContext.Request.UserHostAddress
                //        };
                //        new UserLoginTrackingService().Save(ut);
                //        return RedirectToAction("Index", "Home");
                //    }
                //} 


                //Get the larger profile pic from twitter
                var req = (HttpWebRequest)WebRequest.Create(string.Format("https://api.twitter.com/1/users/profile_image?screen_name={0}&size=original", twitterUser.ResponseObject.ScreenName));
                req.Method = "HEAD";
                var myResp = (HttpWebResponse)req.GetResponse();
                var profileImage = myResp.StatusCode == HttpStatusCode.OK ? myResp.ResponseUri.AbsoluteUri : twitterUser.ResponseObject.ProfileImageLocation;

                var splitName = twitterUser.ResponseObject.Name.Split(new[] { " " }, StringSplitOptions.None);

                var model = new CreateAccountModel()
                {
                    TwitterClientToken = accessTokenResponse.Token,
                    TwitterClientSecret = accessTokenResponse.TokenSecret,
                    TwitterScreenname = accessTokenResponse.ScreenName,
                    FirstName = splitName[0],
                    LastName = splitName[1],
                    DisplayProfileImage = profileImage,
                    ProfileImage = twitterUser.ResponseObject.ProfileImageLocation,
                    ServiceUserName = twitterUser.ResponseObject.ScreenName
                };

                //Continue to Step 2
                return View(model);
            }
            catch (Exception)
            {
                this.StoreError("There was a problem connecting to your twitter account");
                return RedirectToAction("Signup", "join");
            }
        }

        [HttpPost]
        public virtual ActionResult Twitter(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                //Check to see if this account already exists
                var userTest = service.GetUserByUsername(model.Username);
                if (userTest != null)
                {
                    ModelState.AddModelError(string.Empty, string.Format("The username {0} is already in use, please try another username", userTest.Username));
                    model.Username = "";
                    return View(model);
                }

                //Check to see if this email address already exists
                var emailTest = service.GetUserByEmail(model.EmailAddress);
                if (emailTest != null)
                {
                    ModelState.AddModelError(string.Empty, string.Format("The email address {0} is already in use, please a different email address", emailTest.EmailAddress));
                    model.Username = "";
                    return View(model);
                }

                //Ensure the User is over 13
                if (model.DateOfBirth != null && (DateTime.Now - DateTime.Parse(model.DateOfBirth)).Days / 366 < 13)
                {
                    this.StoreError("You must be must be 13 years of age or older to use Epilogger.");
                    ModelState.AddModelError(string.Empty, "You must be must be 13 years of age or older to use Epilogger.");
                    return View(model);
                }


                var user = new User();
                try
                {
                    //Save the User
                    user = Mapper.Map<CreateAccountModel, User>(model);
                    user.IsActive = false; // ensure this is set.
                    service.Save(user);

                    //Store the auth tokens for whatever service
                    switch (model.AuthService)
                    {
                        case "twitter":
                            {
                                //Store the Twitter Auth Record

                                break;
                            }

                            

                        default:
                            {

                                break;
                            }
                            
                    }
                       









                    // build a message to send to the user.
                    //string validateUrl = string.Format("{0}account/validate/{1}", App.BaseUrl, Helpers.base64Encode(user.EmailAddress));

                    //Changed to UserID GUID to prevent problems with duplicate email addresses.
                    var validateUrl = string.Format("{0}account/validate/{1}", App.BaseUrl, Helpers.base64Encode(user.ID.ToString()));

                    var parser = new TemplateParser();
                    var replacements = new Dictionary<string, string>
                                        {
                                            {"[BASE_URL]", App.BaseUrl},
                                            {"[FIRST_NAME]", user.EmailAddress},
                                            {"[VALIDATE_ACCOUNT_URL]", validateUrl}
                                        };

                    var message = parser.Replace(AccountEmails.ValidateAccount, replacements);

                    var sfEmail = new SpamSafeMail
                    {
                        EmailSubject = "Thank you for registering on epilogger.com",
                        HtmlEmail = message,
                        TextEmail = message
                    };
                    sfEmail.ToEmailAddresses.Add(model.EmailAddress);
                    sfEmail.SendMail();


                    this.StoreSuccess("Your account was created successfully<br /><br/>Please check your inbox for our validation message, your account will be inaccessable until you validate it.");

                    CookieHelpers.WriteCookie("lc", "tempid", user.ID.ToString());

                    return RedirectToAction("AccountActivationNeeded", "account");

                }
                catch (Exception ex)
                {
                    this.StoreError("There was a problem creating your account");
                    if (user != null) service.DeleteUser(user.ID);
                    return View(model);
                }
            }
            return View(model);
        }













        #endregion






















        #region Old Account Stuff

        



        [RequiresAuthentication(ValidUserRole=UserRoleType.RegularUser, AccessDeniedMessage="You must be logged in to edit your account")]
        public virtual ActionResult Index () {
            AccountModel model = Mapper.Map<User, AccountModel>(CurrentUser);
            model.ConnectedNetworks = Mapper.Map<List<UserAuthenticationProfile>, List<ConnectedNetworksViewModel>>(CurrentUser.UserAuthenticationProfiles.ToList());
            
            return View(model);
        }


        [HttpPost]
        public virtual ActionResult Index(AccountModel model, FormCollection c)
        {
            model.ConnectedNetworks = Mapper.Map<List<UserAuthenticationProfile>, List<ConnectedNetworksViewModel>>(CurrentUser.UserAuthenticationProfiles.ToList());
            var theUser = Mapper.Map<User, AccountModel>(CurrentUser);
            model.TwitterProfilePicture = theUser.TwitterProfilePicture;
            model.FacebookProfilePicture = theUser.FacebookProfilePicture;
            model.ProfilePicture = theUser.ProfilePicture;

            if (ModelState.IsValid)
            {
                try
                {
                    var user = CurrentUser;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.EmailAddress = model.EmailAddress;
                    user.DateOfBirth = DateTime.Parse(model.DateOfBirth);
                    //user.TimeZoneOffSet = model.TimeZone;

                    string imagePath = string.Empty;
                    if (c["ProfilePictures"] != null)
                    {
                        string pictureProvider = c["ProfilePictures"] as string ?? "";

                        if (pictureProvider.ToLower().Contains("twitter"))
                        {
                            imagePath =
                                TwitterHelper.GetUser(CurrentUserTwitterAuthorization.Token,
                                                      CurrentUserTwitterAuthorization.TokenSecret,
                                                      CurrentUserTwitterAuthorization.ServiceUsername).ResponseObject.
                                    ProfileImageLocation;
                        }
                        else if (pictureProvider.ToLower().Contains("facebook"))
                        {
                            imagePath = FacebookHelper.GetProfilePicture(CurrentUserFacebookAuthorization.Token);
                        }
                    }
                    user.ProfilePicture = imagePath;

                    service.Save(user);

                    //this.StoreSuccess("Your account was updated successfully");
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






        







        [HttpGet]
        public virtual ActionResult Create() {
            return View(new CreateAccountModel());
        }

        [HttpPost]
        public virtual ActionResult Create(CreateAccountModel model)
        {
            //Twitter Auth Token is authed through twitter
            //Request.QueryString["oauth_token"]


            if (ModelState.IsValid) {
                // TEMP: check to make sure the email address provided is in the beta invite table.
                //if(!service.IsBetaUser(model.EmailAddress)) {
                //    this.StoreError("You must be invited to use the epilogger alpha, please click the create account link in your invite email to create your account, if you would like to be invited email team@epilogger.com");
                //    return View(model);
                //}

                //Check to see if this account already exists
                var userTest = service.GetUserByUsername(model.Username);
                if (userTest != null)
                {
                    ModelState.AddModelError(string.Empty, "The username " + userTest.Username + " is already in use, please try another username");
                    model.Username = "";
                    return View(model);
                }


                //Ensure the User is over 13
                if (model.DateOfBirth != null && (DateTime.Now - DateTime.Parse(model.DateOfBirth)).Days / 366 < 13)
                {
                    this.StoreError("You must be must be 13 years of age or older to use Epilogger.");
                    ModelState.AddModelError(string.Empty, "You must be must be 13 years of age or older to use Epilogger.");
                    return View(model);
                }
                //Ensure the passwords match
                //if (model.Password != model.ConfirmPassword)
                //{
                //    this.StoreError("The passwords you entered do not match.");
                //    ModelState.AddModelError(string.Empty, "The passwords you entered do not match.");
                //    return View(model);
                //}

                User user = null;
                try {
                    user = Mapper.Map<CreateAccountModel, User>(model);
                    user.IsActive = false; // ensure this is set.
                    service.Save(user);

                    // build a message to send to the user.
                    //string validateUrl = string.Format("{0}account/validate/{1}", App.BaseUrl, Helpers.base64Encode(user.EmailAddress));
                    
                    //Changed to UserID GUID to prevent problems with duplicate email addresses.
                    string validateUrl = string.Format("{0}account/validate/{1}", App.BaseUrl, Helpers.base64Encode(user.ID.ToString()));

                    var parser = new TemplateParser();
                    Dictionary<string, string> replacements = new Dictionary<string, string>
                                                                  {
                                                                      {"[BASE_URL]", App.BaseUrl},
                                                                      {"[FIRST_NAME]", user.EmailAddress},
                                                                      {"[VALIDATE_ACCOUNT_URL]", validateUrl}
                                                                  };

                    string message = parser.Replace(AccountEmails.ValidateAccount, replacements);

                    var sfEmail = new SpamSafeMail
                    {
                        EmailSubject = "Thank you for registering on epilogger.com",
                        HtmlEmail = message,
                        TextEmail = message
                    };
                    sfEmail.ToEmailAddresses.Add(model.EmailAddress);

                    sfEmail.SendMail();
                    
                    this.StoreSuccess("Your account was created successfully<br /><br/>Please check your inbox for our validation message, your account will be inaccessable until you validate it.");

                    CookieHelpers.WriteCookie("lc", "tempid", user.ID.ToString());

                    return RedirectToAction("AccountActivationNeeded", "account");

                } catch (Exception ex) {
                    this.StoreError("There was a problem creating your account");
                    if (user != null) service.DeleteUser(user.ID);
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Validate(string validationCode) {
            if (string.IsNullOrEmpty(validationCode)) {
                this.StoreError("The verification code couldn't be determined, please try clicking the link in your email again.");
                return View();
            }
            // decode the email address from base64
            // look up the usre account using the email address
            // set isactive = true and update, redirect to login with thank you message.
            Guid userid = Guid.Parse(Helpers.base64Decode(validationCode));
            if (userid== Guid.Empty)
            {
                this.StoreError("The verificatoin code couldn't be determined, please try clicking the link in your email again.");
                return View();
            }

            User user = service.GetUserByID(userid);
            if (user == null) {
                this.StoreError("We couldn't find an account to activate with that verification code");
                return View();
            }

            user.IsActive = true;
            
            service.Save(user);

            CookieHelpers.DestroyCookie("lc");

            this.StoreSuccess("Your account has been activated!  You can go ahead and login to unleash the epicness!");
            return RedirectToAction("login", "account");
        }

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

        [HttpGet]
        public virtual ActionResult Login() {
            CookieHelpers.DestroyCookie("lc");

            // store this here so that we can redirect the user back later.
            if (Request.QueryString["returnUrl"] != null)
                TempData["returnUrl"] = Request.QueryString["returnUrl"];

            return View(new LoginModel());
        }

        [HttpPost]
        public virtual ActionResult Login(LoginModel model) {
            // 1. First we need to check if the person logging in has a blank password, if they do then we need to redirect them 
            // to a page to create a new password.

            if (ModelState.IsValid) {
                User user = service.GetUserByUsername(model.Username);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "There is a problem with your username or password. Please try again or create an account.");
                    return View(model);
                }

                // the user is a valid user but they need to update there password.
                if (user.Password == string.Empty) {
                    CookieHelpers.WriteCookie("lc", "tempid", user.ID.ToString());
                    return RedirectToAction("UpdatePassword");
                }
                
                user = service.GetUserByUsername(model.Username);
                if (!BCryptHelper.CheckPassword(model.Password, user.Password)) {
                    ModelState.AddModelError(string.Empty, "There is a problem with your username or password. Please try again or create an account.");
                    return View(model);
                }

                if (user.IsActive == false) {
                    ModelState.AddModelError(string.Empty, "Your account has not been activated yet, please click the link in the verification email that was sent to you.");
                    return RedirectToAction("AccountActivationNeeded");
                }

                

                // write the login cookie, redirect. 
                if (model.RememberMe) 
                {
                    CookieHelpers.WriteCookie("lc", "uid", user.ID.ToString(), DateTime.Now.AddDays(30));
                    CookieHelpers.WriteCookie("lc", "tz", user.TimeZoneOffSet.ToString(), DateTime.Now.AddDays(30));
                }
                else
                {
                    CookieHelpers.WriteCookie("lc", "uid", user.ID.ToString());
                    CookieHelpers.WriteCookie("lc", "tz", user.TimeZoneOffSet.ToString());
                }

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


                return RedirectToAction("index","home");

            }

            return View(model);
        }

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
                var user = service.GetUserByEmail(model.EmailAddress.Trim());
                if (user == null) {
                    this.StoreWarning("There is no account on epilogger.com that uses that email address");
                    return View();
                }

                user.ForgotPasswordHash = passwordResetHash;
                service.Save(user);

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
            User user = service.GetUserByResetHash(passwordResetHash);
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
                User user = service.GetUserByID(Guid.Parse(model.UserID));
                user.Password = PasswordHelpers.EncryptPassword(model.NewPassword);
                user.ForgotPasswordHash = null;

                service.Save(user);
                
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
                User user = service.GetUserByID(CurrentUserID);
                user.Password = password;

                service.Save(user);

                this.StoreSuccess("Your password has been updated, please login using the password you just created.");

                return RedirectToAction("Login");
                
            } 

            return View();
        }


        public virtual ActionResult AccountActivationNeeded()
        {
            return View();
        }



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

            var message = parser.Replace(AccountEmails.ValidateAccount, replacements);

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

        public virtual ActionResult TwitterAuthTest()
        {

            var apiClient = new APISoapClient();
            var container = apiClient.CreateUrl("http://www.google.com");


            

            //var urlRequestBody = new Epilogr.CreateUrlRequestBody("http://www.google.com");
            //var urlRequest = new Epilogr.CreateUrlRequest(urlRequestBody);

            //var test = new Epilogr.CreateUrlResponse(new CreateUrlResponseBody());
           
            //var containdter = new Epilogr.Container();
            
            //var urlResponseBody = new CreateUrlResponseBody();
            //var mytest = new Epilogr.CreateUrlResponse(urlResponseBody);
            

            //Epilogr.CreateUrlResponse =

            var test = "test";

            return View();
        }

        [HttpPost]
        public virtual ActionResult TwitterAuthTest(TwitterAuthTestViewModel model)
        {

            var tokens = new OAuthTokens { ConsumerKey = "qV0GasfpuvDRmXhnaDA", ConsumerSecret = "q3ftmYti8d4ws2iNieidofWYLswdHT3BRwmu813EA", AccessToken = "280687481-XyZq3P6v7qivApsYjES8V3LjTRgcRZIx2XRO755V", AccessTokenSecret = "Z7MG32TDldpx5USDdOUXjzsop1ZtaEbLMm1bzTnuk" };
            TwitterResponse<TwitterStatus> tweetResponse = TwitterStatus.Update(tokens, "Test");

            return View();
        }

        #endregion

    }
}
