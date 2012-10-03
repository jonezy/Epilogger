using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Epilogger.Common;
using Epilogger.Data;
using Epilogger.Web.Core.Email;
using Epilogger.Web.Models;
using Epilogger.Web.Views.Emails;
using Facebook;
using RichmondDay.Helpers;
using Twitterizer;

namespace Epilogger.Web.Controllers {
    public class JoinController : BaseController 
    {

        UserService _us;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (_us == null) _us = new UserService();

            base.Initialize(requestContext);
        }

        /* Sign up page Step 1 or 2 */
        public ActionResult Signup()
        {
            if (Request.QueryString["NoFrame"] != null)
                ViewBag.NoFrame = true;

            return View();
        }

        /* Create Account with Twitter Button */
        [HttpGet]
        public ActionResult Twitter()
        {
            try
            {
                var twitterAuth = Request.QueryString["oauth_token"];
                if (String.IsNullOrEmpty(twitterAuth))
                {
                    this.StoreError("There was a problem connecting to your Twitter account");
                    return RedirectToAction("Signup");
                }

                var accessTokenResponse = OAuthUtility.GetAccessTokenDuringCallback(TwitterHelper.TwitterConsumerKey, TwitterHelper.TwitterConsumerSecret);

                var twitterUser = TwitterHelper.GetUser(accessTokenResponse.Token,
                                                        accessTokenResponse.TokenSecret,
                                                        accessTokenResponse.ScreenName);

                if (twitterUser == null)
                {
                    this.StoreError("There was a problem connecting to your Twitter account");
                    return RedirectToAction("Signup");
                }

                //Check if this user already exists. If so login and redirect to the homepage
                var userId = CheckConnectedAccountUserExists(accessTokenResponse.ScreenName, AuthenticationServices.TWITTER, accessTokenResponse.Token, accessTokenResponse.TokenSecret);
                if (userId != Guid.Empty)
                {
                    CookieHelpers.WriteCookie("lc", "uid", userId.ToString());
                    RecordTheLogin(userId);

                    //this.StoreInfo("The Twitter account you used is already associated with an Epilogger account. We have logged you in.");
                    return RedirectToAction("Index", "Home");
                }



                //Get the larger profile pic from twitter
                var req = (HttpWebRequest)WebRequest.Create(string.Format("https://api.twitter.com/1/users/profile_image?screen_name={0}&size=original", twitterUser.ResponseObject.ScreenName));
                req.Method = "HEAD";
                var myResp = (HttpWebResponse)req.GetResponse();
                var profileImage = myResp.StatusCode == HttpStatusCode.OK ? myResp.ResponseUri.AbsoluteUri : twitterUser.ResponseObject.ProfileImageLocation;

                var splitName = twitterUser.ResponseObject.Name.Split(new[] { " " }, StringSplitOptions.None);

                var model = new CreateAccountModel()
                {
                    AuthToken = accessTokenResponse.Token,
                    AuthTokenSecret = accessTokenResponse.TokenSecret,
                    AuthScreenname = accessTokenResponse.ScreenName,
                    FirstName = splitName[0],
                    LastName = splitName[1],
                    DisplayProfileImage = profileImage,
                    ProfileImage = twitterUser.ResponseObject.ProfileImageLocation,
                    ServiceUserName = twitterUser.ResponseObject.ScreenName,
                    AuthService = "twitter"
                };

                //Continue to Step 2
                return View(model);
            }
            catch (Exception)
            {
                this.StoreError("There was a problem connecting to your twitter account");
                return RedirectToAction("Signup");
            }
        }

        [HttpPost]
        public ActionResult Twitter(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                if (!AreAccountDetailsValid(model))
                {
                    return View(model);
                }

                var user = new User();
                try
                {
                    ShortCreateFormSaveUser(model);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    this.StoreError("There was a problem creating your account");
                    if (user.ID != Guid.Empty) _us.DeleteUser(user.ID);
                    return View(model);
                }
            }

            return View(model);
        }


        /* Create Account with Facebook Button */
        [HttpGet]
        public ActionResult Facebook(string returnUrl)
        {
            try
            {
                var client = new FacebookClient();
                var oauthResult = client.ParseOAuthCallbackUrl(Request.Url);

                // Build the Return URI form the Request Url
                var redirectUri = new UriBuilder(Url.Action("facebook", "join", null, "http"));

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

                if (facebookUser == null)
                {
                    this.StoreError("There was a problem connecting to your Facebook account");
                    return RedirectToAction("Signup");
                }

                //Check if this user already exists. If so login and redirect to the homepage
                var userId = CheckConnectedAccountUserExists(facebookUser.username, AuthenticationServices.FACEBOOK, accessToken, null);
                if (userId != Guid.Empty)
                {
                    CookieHelpers.WriteCookie("lc", "uid", userId.ToString());
                    RecordTheLogin(userId);

                    //this.StoreInfo("The Facebook  account you used is already associated with an Epilogger account. We have logged you in.");
                    return RedirectToAction("Index", "Home", new { area = "" });
                }


                var model = new CreateAccountModel()
                {
                    AuthToken = accessToken,
                    AuthScreenname = facebookUser.username,
                    FirstName = facebookUser.first_name,
                    LastName = facebookUser.last_name,
                    DisplayProfileImage = FacebookHelper.GetProfilePictureWithSize(accessToken, "large"),
                    ProfileImage = FacebookHelper.GetProfilePicture(accessToken),
                    ServiceUserName = facebookUser.username,
                    AuthService = "facebook"
                };

                //Continue to Step 2
                return View(model);
            }
            catch (Exception)
            {
                this.StoreError("There was a problem connecting to your Facebook account");
                return RedirectToAction("Signup");
            }
        }

        [HttpPost]
        public ActionResult Facebook(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                if (!AreAccountDetailsValid(model))
                {
                    return View(model);
                }

                var user = new User();
                try
                {
                    ShortCreateFormSaveUser(model);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    this.StoreError("There was a problem creating your account");
                    if (user.ID != Guid.Empty) _us.DeleteUser(user.ID);
                    return View(model);
                }
            }

            return View(model);
        }


        /* Create Account - Full */
        [HttpGet]
        public ActionResult Email()
        {
            try
            {

                var model = new CreateAccountModel()
                {
                    AuthToken = string.Empty,
                    AuthTokenSecret = string.Empty,
                    AuthScreenname = string.Empty,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    DisplayProfileImage = Helpers.ResolveServerUrl(VirtualPathUtility.ToAbsolute("~/Public/images/signup/NoAvatar.png"), false),
                    ProfileImage = Helpers.ResolveServerUrl(VirtualPathUtility.ToAbsolute("~/Public/images/signup/NoAvatar.png"), false),
                    ServiceUserName = String.Empty,
                    AuthService = string.Empty
                };

                return View(model);
            }
            catch (Exception)
            {
                this.StoreError("There was a problem. Please try again later.");
                return RedirectToAction("Signup", "join");
            }
        }

        [HttpPost]
        public ActionResult Email(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                if (!AreAccountDetailsValid(model))
                {
                    return View(model);
                }

                var user = new User();
                try
                {
                    FullCreateFormSaveUser(model);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    this.StoreError("There was a problem creating your account");
                    if (user.ID != Guid.Empty) _us.DeleteUser(user.ID);
                    return View(model);
                }
            }

            return View(model);
        }



        /* Called from a link, there is no page.*/
        [HttpGet]
        public ActionResult ResendVerificationEmail()
        {
            try
            {
                SendVerificationEmail(CurrentUser);
                this.StoreSuccess("Verification Email has been resent to " + CurrentUser.EmailAddress + ". Please check your email.");
            }
            catch (Exception ex)
            {
                this.StoreError("There was a problem resending your email verification, please try again.");
            }

            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }




        #region Private Methods


        
        private Guid CheckConnectedAccountUserExists(string userScreenName, AuthenticationServices authService, string token, string tokenSecret)
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

        private bool AreAccountDetailsValid(CreateAccountModel model)
        {
            //Check to see if this account already exists
            var userTest = _us.GetUserByUsername(model.Username);
            if (userTest != null)
            {
                ModelState.AddModelError(string.Empty, string.Format("The username {0} is already in use, please try another username", userTest.Username));
                model.Username = "";
                return false;
            }

            //Check to see if this email address already exists
            var emailTest = _us.GetUserByEmail(model.EmailAddress);
            if (emailTest != null)
            {
                ModelState.AddModelError(string.Empty, string.Format("The email address {0} is already in use, please a different email address", emailTest.EmailAddress));
                model.EmailAddress = "";
                return false;
            }

            //Ensure the User is over 13
            if (model.DateOfBirth != null && (DateTime.Now - DateTime.Parse(model.DateOfBirth)).Days / 366 < 13)
            {
                ModelState.AddModelError(string.Empty, "You must be must be 13 years of age or older to use Epilogger.");
                return false;
            }

            return true;
        }

        private User SaveUser(CreateAccountModel model)
        {
            //Save the User
            var user = Mapper.Map<CreateAccountModel, User>(model);
            user.IsActive = true;
            user.DateOfBirth = DateTime.Parse(model.DateOfBirth);
            _us.Save(user);
            return user;
        }

        private static void ConnectAuthAccountToUser(CreateAccountModel model, User user)
        {
            //Store the auth tokens for whatever service
            var userAuth = new UserAuthenticationProfile
            {
                UserID = user.ID,
                Platform = "Web",
                ServiceUsername = model.AuthScreenname,
                Token = model.AuthToken,
                TokenSecret = model.AuthTokenSecret
            };

            switch (model.AuthService)
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

        private static void SendWelcomeEmail(User user)
        {
            //Send the user the Welcome Email

            var parser = new TemplateParser();
            var replacements = new Dictionary<string, string>
                                   {
                                       {"[FIRST_NAME]", user.FirstName},
                                       {"[year]", DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) }
                                   };

            var message = parser.Replace(AccountEmails.WelcomeEmail, replacements);

            var sfEmail = new SpamSafeMail
            {
                EmailSubject = "Welcome to epilogger.com!",
                HtmlEmail = message,
                TextEmail = message
            };
            sfEmail.ToEmailAddresses.Add(user.EmailAddress);
            sfEmail.SendMail();
        }

        private static void SendVerificationEmail(User user)
        {
            //Send the Validate Email Address Email

            //Changed to UserID GUID to prevent problems with duplicate email addresses.
            var validateUrl = string.Format("{0}account/validate/{1}", App.BaseUrl, Helpers.base64Encode(user.ID.ToString()));

            var parser = new TemplateParser();
            var replacements = new Dictionary<string, string>
                                   {
                                       {"[FIRST_NAME]", user.FirstName},
                                       {"[VALIDATE_ACCOUNT_URL]", validateUrl},
                                       {"[year]", DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) }
                                   };

            var message = parser.Replace(AccountEmails.ValidateEmail, replacements);

            var sfEmail = new SpamSafeMail
            {
                EmailSubject = "Epilogger.com - Please validate your email address",
                HtmlEmail = message,
                TextEmail = message
            };
            sfEmail.ToEmailAddresses.Add(user.EmailAddress);
            sfEmail.SendMail();
        }

        private void ShortCreateFormSaveUser(CreateAccountModel model)
        {
            var user = SaveUser(model);
            ConnectAuthAccountToUser(model, user);

            SendWelcomeEmail(user);
            SendVerificationEmail(user);

            this.StoreSuccess("Your account was created successfully");
            CookieHelpers.WriteCookie("lc", "uid", user.ID.ToString(), DateTime.Now.AddDays(30));
        }

        private void FullCreateFormSaveUser(CreateAccountModel model)
        {
            var user = SaveUser(model);

            SendWelcomeEmail(user);
            SendVerificationEmail(user);

            this.StoreSuccess("Your account was created successfully");
            CookieHelpers.WriteCookie("lc", "uid", user.ID.ToString(), DateTime.Now.AddDays(30));
        }

        private void RecordTheLogin(Guid userId)
        {
            //Record the Login
            var ut = new UserLoginTracking()
            {
                UserId = userId,
                LoginMethod = "Epilogger Account",
                DateTime = DateTime.UtcNow,
                IPAddress = HttpContext.Request.UserHostAddress
            };
            new UserLoginTrackingService().Save(ut);
        }


    #endregion






    }
}
