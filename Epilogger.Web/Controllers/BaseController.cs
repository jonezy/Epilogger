using System;
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
                var service = new UserService();
                return service.GetUserByID(CurrentUserID);
            }
        }

        public bool IsEmailVerified
        {
            get
            {
                 if (CurrentUser != null)
                 {
                     return CurrentUser.EmailVerified != null && (bool) CurrentUser.EmailVerified;
                 }
                return false;
            }
        }

        public bool InPopUp
        {
            get
            {
                var inPopUp = Request.QueryString["InPopUp"] != null;
                inPopUp = inPopUp && bool.Parse(Request.QueryString["InPopUp"]);
                return inPopUp;
            }
        }


        protected UserAuthenticationProfile CurrentUserTwitterAuthorization {
            get
            {
                if (CurrentUser==null)
                {
                    return null;
                }
                return CurrentUser != null && CurrentUser.UserAuthenticationProfiles.FirstOrDefault(ua => ua.Service == AuthenticationServices.TWITTER.ToString() && ua.Platform == "Web") == null ? null : CurrentUser.UserAuthenticationProfiles.FirstOrDefault(ua => ua.Service == AuthenticationServices.TWITTER.ToString() && ua.Platform == "Web");
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