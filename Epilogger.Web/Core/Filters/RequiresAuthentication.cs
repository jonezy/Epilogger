﻿using System;
using System.Web.Mvc;
using System.Web.Routing;

using Epilogger.Data;
using Epilogger.Web.Controllers;

namespace Epilogger.Web {
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RequiresAuthentication : ActionFilterAttribute {
        public UserRoleType ValidUserRole { get; set; }
        public bool IsAdmin { get; set; }
        public string AccessDeniedMessage { get; set; }

        private RouteValueDictionary HomepageRouteDictionary {
            get {
                RouteValueDictionary homepageRouteDictionary = new RouteValueDictionary();
                homepageRouteDictionary.Add("action", "Index");
                homepageRouteDictionary.Add("controller", "Home");

                return homepageRouteDictionary;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            BaseController controller = (BaseController)filterContext.Controller;
            User user = controller.CurrentUser;

            bool skipRoleCheck = false;

            if (user == null) {
                //controller.StoreWarning(AccessDeniedMessage ?? "Please login");
                controller.Receive(MessageType.Warning, AccessDeniedMessage ?? "Please login");
                filterContext.Result = new RedirectToRouteResult(BuildLoginRouteDictionary(filterContext.HttpContext.Request.Url.AbsoluteUri));
                skipRoleCheck = true;
            }

            // TODO: Implement role checking here.
            if (!skipRoleCheck)
            {
                if (!string.IsNullOrEmpty(ValidUserRole.ToString())) {
                    int currentRole = (int)Enum.Parse(typeof(UserRoleType), ValidUserRole.ToString());
                    if (user == null || user.RoleID > currentRole) {
                        if (!string.IsNullOrEmpty(AccessDeniedMessage)) {
                            //controller.StoreInfo(AccessDeniedMessage);
                            controller.Receive(MessageType.Warning, AccessDeniedMessage);
                        }

                        filterContext.Result = new RedirectToRouteResult(BuildLoginRouteDictionary(filterContext.HttpContext.Request.Url.AbsoluteUri));
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

        private RouteValueDictionary BuildLoginRouteDictionary(string returnUrl) {
            RouteValueDictionary loginpageRouteDictionary = new RouteValueDictionary();
            loginpageRouteDictionary.Add("action", "login");
            loginpageRouteDictionary.Add("controller", "account");
            
            if(!string.IsNullOrEmpty(returnUrl))
                loginpageRouteDictionary.Add("returnUrl", returnUrl);

            return loginpageRouteDictionary;
        }
    }
}
