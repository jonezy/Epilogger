using System;
using System.Web.Mvc;

using Epilogger.Web.Models;

namespace Epilogger.Web.Controllers {
    public class NavigationController : BaseController {
        [ChildActionOnly]
        public ActionResult GlobalNavigation() {
            GlobalNavigationModel model = new GlobalNavigationModel { UserLoggedIn = CurrentUserID != Guid.Empty ? true : false };
            return PartialView("GlobalNavigation", model);
        }

    }
}
