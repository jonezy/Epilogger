using System;
using System.Linq;
using System.Web.Mvc;

using Epilogger.Data;

using RichmondDay.Helpers;

namespace Epilogger.Web.Controllers {
    public class BaseController : Controller {
        protected Guid CurrentUserID {
            get {
                Guid userId;
                Guid.TryParse(CookieHelpers.GetCookieValue("lc", "uid").ToString(), out userId);

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
            get {
                return CurrentUser.UserAuthenticationProfiles.Where(ua => ua.Service == AuthenticationServices.TWITTER.ToString()).FirstOrDefault();
            }
        }

        protected UserAuthenticationProfile CurrentUserFacebookAuthorization {
            get {
                return CurrentUser.UserAuthenticationProfiles.Where(ua => ua.Service == AuthenticationServices.FACEBOOK.ToString()).FirstOrDefault();
            }
        }

        protected UserRoleType CurrentUserRole {
            get {
                if (CurrentUser == null)
                    return UserRoleType.Unknown;

                return (UserRoleType) Enum.Parse(typeof(UserRoleType), CurrentUser.RoleID.ToString());
            }
        }
    }
}