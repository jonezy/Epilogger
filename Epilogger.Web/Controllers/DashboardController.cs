﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Models;
namespace Epilogger.Web.Controllers {
    [RequiresAuthentication(AccessDeniedMessage = "You must be logged in to view your dashboard")]
    public class DashboardController : BaseController {
        TweetService tweetService;
        EventService eventService;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (tweetService == null) tweetService = new TweetService();
            if (eventService == null) eventService = new EventService();

            base.Initialize(requestContext);
        }

        public ActionResult Index(int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;

            IEnumerable<Tweet> tweets = tweetService.FindByUserScreenName(CurrentUserTwitterAuthorization.ServiceUsername);
            IEnumerable<Event> events = eventService.FindByUserID(CurrentUserID);
            List<Image> images = new List<Image>();
            foreach (var item in events) {
                foreach (var image in item.ImageMetaData.Where(imd => imd.TwitterName == CurrentUserTwitterAuthorization.ServiceUsername)) {
                    images.Add(image.Images.FirstOrDefault());
                }
            }

            List<DashboardActivityModel> activity = new List<DashboardActivityModel>();

            foreach (var item in tweets) {
                activity.Add(new DashboardActivityModel() {
                    ActivityType = ActivityType.TWEET,
                    Date = item.CreatedDate.Value,
                    ActivityContent = item.TextAsHTML,
                    Event = Mapper.Map<Event, DashboardEventViewModel>(item.Events.FirstOrDefault())
                });
	        }

            foreach (var item in events) {
                activity.Add(new DashboardActivityModel() {
                    ActivityType = ActivityType.EVENT_CREATION,
                    Date = item.StartDateTime.Value,
                    ActivityContent = item.Description,
                    Event = Mapper.Map<Event, DashboardEventViewModel>(item)
                });
            }

            foreach (var item in images) {
                activity.Add(new DashboardActivityModel() {
                    ActivityType = ActivityType.PHOTOS_VIDEOS,
                    Date = item.DateTime,
                    ActivityContent = item.Thumb,
                    Event = Mapper.Map<Event, DashboardEventViewModel>(item.Events.FirstOrDefault())
                });                
            }

            DashboardIndexViewModel model = new DashboardIndexViewModel(activity.OrderByDescending(a => a.Date).ToList(),currentPage);

            return View(model);
        }

        public ActionResult Profile() {
            DashboardProfileViewModel model = Mapper.Map<User, DashboardProfileViewModel>(CurrentUser);

            return View(model);
        }

    }
}