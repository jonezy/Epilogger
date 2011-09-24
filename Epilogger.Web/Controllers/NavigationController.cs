using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Epilogger.Web.Models;

namespace Epilogger.Web.Controllers {
    public class NavigationController : BaseController {
        [ChildActionOnly]
        public ActionResult GlobalNavigation() {
            GlobalNavigationModel model = new GlobalNavigationModel { UserLoggedIn = CurrentUserID != Guid.Empty ? true : false };
            if (CurrentUser != null) {
                model.Username = CurrentUser.Username;
            }

            return PartialView("GlobalNavigation", model);
        }

        public ActionResult RenderBreadCrumb() {
            Dictionary<string, string> trail = new Dictionary<string, string>();
            trail.Add("home", App.BaseUrl);
            if (Request.Url.AbsoluteUri != App.BaseUrl) {
                int count = 1;
                foreach (var item in Request.RequestContext.RouteData.Values) {
                    if (item.Value.ToString().ToLower() != "index" && item.Key.ToString().ToLower() != "id") {
                        string url = string.Empty;
                        if (count == 1) {
                            url = string.Format("{0}{1}", App.BaseUrl, Url.Action("index", Request.RequestContext.RouteData.Values["controller"].ToString()).TrimStart('/'));
                        } else if (count == 2) {
                            url = string.Format("{0}{1}", App.BaseUrl, Url.Action(Request.RequestContext.RouteData.Values["action"].ToString(), Request.RequestContext.RouteData.Values["controller"].ToString(), Request.RequestContext.RouteData.Values).TrimStart('/'));
                        }
                        
                        trail.Add(item.Value.ToString(), url);

                        count++;
                    }
                }
            }

            ViewBag.Trail = trail;

            return PartialView("Breadcrumb", ViewBag);
        }
    }
}
