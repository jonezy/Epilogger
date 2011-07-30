using System.Web.Mvc;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Models;
using System;

namespace Epilogger.Web.Controllers {
    public class AccountController : Controller {
        UserService service;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (service == null) service = new UserService();

            base.Initialize(requestContext);
        }

        public ActionResult Index() {
            return View();
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
                this.StoreSuccess("Your account was created successfully");
                return RedirectToAction("Index", "Home");
            } catch (Exception ex) {
                this.StoreError("There was a problem creating your account");

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Login() {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model) {
            return View(model);
        }
    }
}
