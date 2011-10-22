﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Epilogger.Data;
using Epilogger.Web.Models;

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
            model.CurrentUserRole = CurrentUserRole;

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
                    
                    if (routeValue != "index") {
                        // this handles the majority of breadcrumb cases
                        string label = item.Value.ToString();
                        string url = count == 1 ? 
                            string.Format("{0}{1}", App.BaseUrl, Url.Action("index", Request.RequestContext.RouteData.Values["controller"].ToString()).TrimStart('/')) :
                            string.Format("{0}{1}", App.BaseUrl, Url.Action(Request.RequestContext.RouteData.Values["action"].ToString(), Request.RequestContext.RouteData.Values["controller"].ToString(), Request.RequestContext.RouteData.Values).TrimStart('/'));

                        // this unfucks the ordering in the events section
                        if(Request.RequestContext.RouteData.Values["controller"].ToString() == "events") {
                            trail.Remove("events");
                            trail.Add("events", url);
                        }

                        // this handles the event section, just overrides the values set above.
                        if (routeKey == "id" && routeValue != "events") {
                            int eventId;
                            int.TryParse(routeValue, out eventId);
                            if (eventId > 0) {
                                Event currentEvent = eventService.FindByID(eventId);
                                label = currentEvent.Name;
                                url = string.Format("{0}{1}", App.BaseUrl, Url.Action("details", "events", Request.RequestContext.RouteData.Values).TrimStart('/'));
                            }
                        }

                        if(!trail.ContainsKey(label) && !label.Equals("details") )
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
