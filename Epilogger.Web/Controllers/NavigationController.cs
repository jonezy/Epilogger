using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epilogger.Web.Models;

namespace Epilogger.Web.Controllers {
    public class NavigationController : BaseController {
        public ActionResult GlobalNavigation() {
            GlobalNavigationModel model = new GlobalNavigationModel { UserLoggedIn = CurrentUserID != Guid.Empty ? true : false };
            return PartialView("GlobalNavigation", model);
        }

    }
}
