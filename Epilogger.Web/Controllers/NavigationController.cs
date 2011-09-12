using System;
using System.Web.Mvc;

using Epilogger.Web.Models;
using System.Collections.Generic;

namespace Epilogger.Web.Controllers {
    public class NavigationController : BaseController {
        [ChildActionOnly]
        public ActionResult GlobalNavigation() {
            GlobalNavigationModel model = new GlobalNavigationModel { UserLoggedIn = CurrentUserID != Guid.Empty ? true : false };
            return PartialView("GlobalNavigation", model);
        }

        public ActionResult RenderBreadCrumb() {
            Dictionary<string, string> trail = new Dictionary<string, string>();
            trail.Add("home", App.BaseUrl);
            foreach (var item in Request.RequestContext.RouteData.Values) {
                if (item.Value.ToString().ToLower() != "index") {
                    string url = string.Format("{0}{1}", App.BaseUrl, Url.Action(Request.RequestContext.RouteData.Values["action"].ToString(), Request.RequestContext.RouteData.Values["controller"].ToString()).TrimStart('/'));
                    trail.Add(item.Value.ToString(), url);
                }
            }

            ViewBag.Trail = trail;

            return PartialView("Breadcrumb", ViewBag);
        }
    }
}
