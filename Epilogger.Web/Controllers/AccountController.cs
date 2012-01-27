using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using DevOne.Security.Cryptography.BCrypt;

using Epilogger.Data;
using Epilogger.Web.Core.Email;
using Epilogger.Web.Models;
using Epilogger.Web.Views.Emails;

using RichmondDay.Helpers;

namespace Epilogger.Web.Controllers {
    public class AccountController : BaseController {
        UserService service;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (service == null) service = new UserService();

            base.Initialize(requestContext);
        }

        [RequiresAuthentication(ValidUserRole=UserRoleType.RegularUser, AccessDeniedMessage="You must be logged in to edit your account")]
        public ActionResult Index () {
            AccountModel model = Mapper.Map<User, AccountModel>(CurrentUser);
            model.ConnectedNetworks = Mapper.Map<List<UserAuthenticationProfile>, List<ConnectedNetworksViewModel>>(CurrentUser.UserAuthenticationProfiles.ToList());
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Create() {
            return View(new CreateAccountModel());
        }

        [HttpPost]
        public ActionResult Create(CreateAccountModel model) {
            if (ModelState.IsValid) {
                // TEMP: check to make sure the email address provided is in the beta invite table.
                //if(!service.IsBetaUser(model.EmailAddress)) {
                //    this.StoreError("You must be invited to use the epilogger alpha, please click the create account link in your invite email to create your account, if you would like to be invited email team@epilogger.com");
                //    return View(model);
                //}

                //Ensure the User is over 13
                if (model.DateOfBirth != null && (DateTime.Now - DateTime.Parse(model.DateOfBirth)).Days / 366 < 13)
                {
                    this.StoreError("You must be must be 13 years of age or older to use Epilogger.");
                    return View(model);
                }
                User user = null;
                try {
                    user = Mapper.Map<CreateAccountModel, User>(model);
                    user.IsActive = false; // ensure this is set.
                    service.Save(user);

                    // build a message to send to the user.
                    string validateUrl = string.Format("{0}account/validate/{1}", App.BaseUrl, Helpers.base64Encode(user.EmailAddress));

                    TemplateParser parser = new TemplateParser();
                    Dictionary<string, string> replacements = new Dictionary<string, string>();
                    replacements.Add("[BASE_URL]", App.BaseUrl);
                    replacements.Add("[FIRST_NAME]", user.EmailAddress);
                    replacements.Add("[VALIDATE_ACCOUNT_URL]", validateUrl);

                    string message = parser.Replace(AccountEmails.ValidateAccount, replacements);

                    EmailSender sender = new EmailSender();
                    sender.Send(App.MailConfiguration, model.EmailAddress, "", "Thank you for registering on epilogger.com", message);

                    this.StoreSuccess("Your account was created successfully<br /><br/>Please check your inbox for our validation message, your account will be inaccessable until you validate it.");

                    return RedirectToAction("login", "account");
                } catch (Exception ex) {
                    this.StoreError("There was a problem creating your account");
                    service.DeleteUser(user.ID);
                    return View(model);
                }
            } else
                return View(model);
        }

        [HttpGet]
        public ActionResult Validate(string validationCode) {
            if (string.IsNullOrEmpty(validationCode)) {
                this.StoreError("The verification code couldn't be determined, please try clicking the link in your email again.");
                return View();
            }
            // decode the email address from base64
            // look up the usre account using the email address
            // set isactive = true and update, redirect to login with thank you message.
            string email = Helpers.base64Decode(validationCode);
            if (string.IsNullOrEmpty(email)) {
                this.StoreError("The verificatoin code couldn't be determined, please try clicking the link in your email again.");
                return View();
            }

            User user = service.GetUserByEmail(email);
            if (user == null) {
                this.StoreError("We couldn't find an account to activate with that verification code");
                return View();
            }

            user.IsActive = true;
            
            service.Save(user);
            this.StoreSuccess("Your account has been activated!  You can go ahead and login to unleash the epicness!");
            return RedirectToAction("login", "account");
        }

        [HttpPost]
        public ActionResult Update(AccountModel model, FormCollection c) {
            try {
                User user = CurrentUser;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.EmailAddress = model.EmailAddress;
                user.DateOfBirth = DateTime.Parse(model.DateOfBirth);
                user.TimeZoneOffSet = model.TimeZone;

                string imagePath = string.Empty;
                if (c["ProfilePictures"] != null) {
                    string pictureProvider = c["ProfilePictures"] as string ?? "";

                    if (pictureProvider.ToLower().Contains("twitter")) {
                        imagePath = TwitterHelper.GetUser(CurrentUserTwitterAuthorization.Token, CurrentUserTwitterAuthorization.TokenSecret, CurrentUserTwitterAuthorization.ServiceUsername).ResponseObject.ProfileImageLocation;
                    } else if (pictureProvider.ToLower().Contains("facebook")) {
                        imagePath = FacebookHelper.GetProfilePicture(CurrentUserFacebookAuthorization.Token);
                    }
                }
                user.ProfilePicture = imagePath;

                service.Save(user);

                this.StoreSuccess("Your account was updated successfully");
            } catch (Exception ex) {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StoreError("There was a problem updating your account");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login() {
            // store this here so that we can redirect the user back later.
            if (Request.QueryString["returnUrl"] != null)
                TempData["returnUrl"] = Request.QueryString["returnUrl"];

            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model) {
            // 1. First we need to check if the person logging in has a blank password, if they do then we need to redirect them 
            // to a page to create a new password.

            if (ModelState.IsValid) {
                User user = service.GetUserByUsername(model.Username);

                // the user is a valid user but they need to update there password.
                if (user != null && user.Password == string.Empty) {
                    CookieHelpers.WriteCookie("lc", "uid", user.ID.ToString());
                    return RedirectToAction("UpdatePassword");
                }
                
                user = service.GetUserByUsername(model.Username);
                if (!BCryptHelper.CheckPassword(model.Password, user.Password)) {
                    this.StoreError("The password you entered does not match the password for your account");
                    return View(model);
                }

                if (user.IsActive == false) {
                    this.StoreError("Your account has not been activated yet, please click the link in the verification email that was sent to you.");
                    return RedirectToAction("login");
                }

                if (user == null) {
                    this.StoreError("Login failed");
                    return View(model);
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

                return RedirectToAction("index","home");
            }

            return View(model);
        }

        public ActionResult Logout() {
            RichmondDay.Helpers.CookieHelpers.DestroyCookie("lc");
            
            this.StoreInfo("You have been logged out of your epilogger account");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ForgotPassword() {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model) {
            try {
                Guid passwordResetHash = Guid.NewGuid();
                User user = service.GetUserByEmail(model.EmailAddress.Trim());
                if (user == null) {
                    this.StoreWarning("There is no account on epilogger.com that uses that email address");
                    return View();
                }

                user.ForgotPasswordHash = passwordResetHash;
                service.Save(user);

                string resetPasswordUrl = string.Format("{0}account/resetpassword?hash={1}", App.BaseUrl, passwordResetHash);

                TemplateParser parser = new TemplateParser();
                Dictionary<string, string> replacements = new Dictionary<string, string>();
                replacements.Add("[BASE_URL]", App.BaseUrl);
                replacements.Add("[USERNAME]", user.Username);
                replacements.Add("[USER_EMAIL]", user.EmailAddress);
                replacements.Add("[RESET_PASSWORD_URL]", resetPasswordUrl);

                string message = parser.Replace(AccountEmails.ForgotPassword, replacements);

                EmailSender sender = new EmailSender();
                sender.Send(App.MailConfiguration, model.EmailAddress, "", "Reset your password on epilogger.com", message);

                this.StoreSuccess("We\\'ve emailed you instructions on how to reset your password.");
            } catch (Exception ex) {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                this.StoreError("There was a problem sending you instructions to reset your password");
            }

            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword() {
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
        public ActionResult ResetPassword(ResetPasswordViewModel model) {
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
        public ActionResult UpdatePassword() {
            return View(new UpdatePasswordModel());
        }

        [HttpPost]
        public ActionResult UpdatePassword(UpdatePasswordModel model) {
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
    }
}
