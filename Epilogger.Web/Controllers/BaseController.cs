﻿using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Epilogger.Data;
using RichmondDay.Helpers;

namespace Epilogger.Web.Controllers {

    public partial class BaseController : Controller
    {
        protected Guid CurrentUserID {
            get {

                Guid userId = new Guid();
                try
                {
                    Guid.TryParse(CookieHelpers.GetCookieValue("lc", "uid").ToString(), out userId);   
                }
                catch (Exception)
                {
                }

                return userId;
                
            }
        }
        
        public User CurrentUser {
            get {
                UserService service = new UserService();
                return service.GetUserByID(CurrentUserID);
            }
        }

        protected UserAuthenticationProfile CurrentUserTwitterAuthorization {
            get
            {
                if (CurrentUser==null)
                {
                    return null;
                }
                return CurrentUser != null && CurrentUser.UserAuthenticationProfiles.FirstOrDefault(ua => ua.Service == AuthenticationServices.TWITTER.ToString()) == null ? null : CurrentUser.UserAuthenticationProfiles.FirstOrDefault(ua => ua.Service == AuthenticationServices.TWITTER.ToString());
            }
        }

        protected UserAuthenticationProfile CurrentUserFacebookAuthorization {
            get {
                return CurrentUser.UserAuthenticationProfiles.FirstOrDefault(ua => ua.Service == AuthenticationServices.FACEBOOK.ToString());
            }
        }

        protected UserRoleType CurrentUserRole {
            get {
                if (CurrentUser == null)
                    return UserRoleType.Unknown;

                return (UserRoleType) Enum.Parse(typeof(UserRoleType), CurrentUser.RoleID.ToString());
            }
        }


        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }


    }
}