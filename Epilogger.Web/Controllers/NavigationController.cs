using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Epilogger.Web.Models;
using Epilogger.Data;

namespace Epilogger.Web.Controllers {
    public class NavigationController : BaseController {
        EventService eventService;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (eventService == null) eventService = new EventService();
            base.Initialize(requestContext);
        }
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
                    string routeValue = item.Value.ToString().ToLower();
                    string routeKey = item.Key.ToString().ToLower();
                    
                    if (routeValue != "index" && (routeValue != "details")) {
                        string label = item.Value.ToString();
                        // if this is an event id let's grab the name for the label
                        if (routeKey == "id") {
                            int eventId = int.Parse(routeValue);
                            Event currentEvent = eventService.FindByID(eventId);
                            label = currentEvent.Name;
                        }

                        string url = count == 1 ? 
                            string.Format("{0}{1}", App.BaseUrl, Url.Action("index", Request.RequestContext.RouteData.Values["controller"].ToString()).TrimStart('/')) :
                            string.Format("{0}{1}", App.BaseUrl, Url.Action(Request.RequestContext.RouteData.Values["action"].ToString(), Request.RequestContext.RouteData.Values["controller"].ToString(), Request.RequestContext.RouteData.Values).TrimStart('/'));

                        trail.Add(label, url);

                        count++;
                    }
                }
            }

            ViewBag.Trail = trail;

            return PartialView("Breadcrumb", ViewBag);
        }
    }
}
