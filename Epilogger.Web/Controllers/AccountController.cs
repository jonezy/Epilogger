using System;
using System.Web.Mvc;

using AutoMapper;

using DevOne.Security.Cryptography.BCrypt;

using Epilogger.Data;
using Epilogger.Web.Models;

using RichmondDay.Helpers;

namespace Epilogger.Web.Controllers {
    public class AccountController : BaseController {
        UserService service;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (service == null) service = new UserService();

            base.Initialize(requestContext);
        }

        [RequiresAuthentication(AccessDeniedMessage="You must be logged in to edit your account")]
        public ActionResult Index () {
            AccountModel model = Mapper.Map<User, AccountModel>(CurrentUser);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create() {
            return View(new CreateAccountModel());
        }

        [HttpPost]
        public ActionResult Create(CreateAccountModel model) {
            try {
                User user = Mapper.Map<CreateAccountModel, User>(model);
                service.Save(user);
                
                user.ID = Guid.NewGuid();
                service.Save(user); 
                //TODO: send a confirmation email.

                this.StoreSuccess("Your account was created successfully");
                
                return RedirectToAction("Index", "Home");
            } catch (Exception ex) {
                this.StoreError("There was a problem creating your account");

                return View(model);
            }
        }

        [HttpPost]
        [RequiresAuthentication(AccessDeniedMessage = "You must be logged in to edit your account")]
        public ActionResult Update(AccountModel model) {
            try {
                User user = CurrentUser;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.EmailAddress = model.EmailAddress;

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
                    return RedirectToAction("UpdatePassword");
                }
                
                user = service.GetUserByUsername(model.Username);
                if (!BCryptHelper.CheckPassword(model.Password, user.Password)) {
                    this.StoreError("The password you entered does not match the password for your account");
                    return View(model);
                }

                if (user == null) {
                    this.StoreError("Login failed");
                    return View(model);
                }

                // write the login cookie, redirect. 
                if(model.RememberMe)
                    CookieHelpers.WriteCookie("lc", "uid", user.ID.ToString(), DateTime.Now.AddDays(30));
                 
                CookieHelpers.WriteCookie("lc", "uid", user.ID.ToString());

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Logout() {
            RichmondDay.Helpers.CookieHelpers.DestroyCookie("lc");
            
            this.StoreInfo("You have been logged out of our epilogger account");

            return RedirectToAction("Index");
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
                User user = service.GetUserByEmail(model.EmailAddress);

                user.Password = password;

                service.Save(user);

                this.StoreSuccess("Your password has been updated, please login");

                return RedirectToAction("Login");
                
            } 

            return View();
        }
    }
}
