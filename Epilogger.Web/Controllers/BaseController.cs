using System;
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

        //protected UserAuthorization CurrentUserTwitterAuthorization {
        //    get {
        //        return CurrentUser.UserAuthorizations.Where(ua => ua.Service == AuthenticationServices.TWITTER.ToString()).FirstOrDefault();
        //    }
        //}
        //protected UserAuthorization CurrentUserFacebookAuthorization {
        //    get {
        //        return CurrentUser.UserAuthorizations.Where(ua => ua.Service == AuthenticationServices.FACEBOOK.ToString()).FirstOrDefault();
        //    }
        //}
    }
}