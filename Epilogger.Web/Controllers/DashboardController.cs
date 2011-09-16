using System.Collections;
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
        UserService userService;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (tweetService == null) tweetService = new TweetService();
            if (eventService == null) eventService = new EventService();
            if (userService == null) userService = new UserService();

            base.Initialize(requestContext);
        }

        public ActionResult Index(int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;
            List<DashboardActivityModel> activity = new List<DashboardActivityModel>();

            if (CurrentUserTwitterAuthorization != null) {
                IEnumerable<Tweet> tweets = tweetService.FindByUserScreenName(CurrentUserTwitterAuthorization.ServiceUsername);
                IEnumerable<Event> events = eventService.FindByUserID(CurrentUserID);
                List<Image> images = new List<Image>();
                List<UserRatesEvent> eventrating = userService.GetUserEventRatings(CurrentUserID);

                foreach (var item in events) {
                    IEnumerable<ImageMetaDatum> usersEventImages = item.ImageMetaData.Where(imd => imd.TwitterName == CurrentUserTwitterAuthorization.ServiceUsername).Distinct();
                    foreach (var image in usersEventImages) {
                        images.Add(image.Images.First());
                    }
                }

                foreach (var item in CurrentUser.UserFollowsEvents) {
                    activity.Add(new DashboardActivityModel() {
                        ActivityType = ActivityType.FOLLOW_EVENT,
                        Date = item.Timestamp,
                        ActivityContent = item.Events.FirstOrDefault().Name,
                        Event = Mapper.Map<Event, DashboardEventViewModel>(item.Events.FirstOrDefault())
                    });
                }

                //tweets
                foreach (var item in tweets) {
                    activity.Add(new DashboardActivityModel() {
                        ActivityType = ActivityType.TWEET,
                        Date = item.CreatedDate.Value,
                        ActivityContent = item.TextAsHTML,
                        Event = Mapper.Map<Event, DashboardEventViewModel>(item.Events.FirstOrDefault())
                    });
                }

                // events
                foreach (var item in events) {
                    activity.Add(new DashboardActivityModel() {
                        ActivityType = ActivityType.EVENT_CREATION,
                        Date = item.StartDateTime.Value,
                        ActivityContent = item.Description,
                        Event = Mapper.Map<Event, DashboardEventViewModel>(item)
                    });
                }

                // photo's & video's
                foreach (var item in images) {
                    activity.Add(new DashboardActivityModel() {
                        ActivityType = ActivityType.PHOTOS_VIDEOS,
                        Date = item.DateTime,
                        ActivityContent = item.Fullsize,
                        Event = Mapper.Map<Event, DashboardEventViewModel>(item.Events.FirstOrDefault())
                    });
                }

                // event ratings
                foreach (var item in eventrating) {
                    activity.Add(new DashboardActivityModel() {
                        ActivityType = ActivityType.EVENT_RATING,
                        Date = item.RatingDateTime,
                        ActivityContent = string.Format("{0}'d {1}", item.UserRating, item.Events.FirstOrDefault().Name),
                        Event = Mapper.Map<Event, DashboardEventViewModel>(item.Events.FirstOrDefault())
                    });
                }
            }


            DashboardIndexViewModel model = new DashboardIndexViewModel(
                activity.OrderByDescending(a => a.Date).Skip(currentPage * 12).Take(12).ToList(),
                currentPage,
                activity.Count
            );

            return View(model);
        }

        public ActionResult Profile() {
            List<Event> events = BuildEventsAndSubscriptions();
            DashboardProfileViewModel model = Mapper.Map<User, DashboardProfileViewModel>(CurrentUser);
            model.Events = Mapper.Map<List<Event>, List<DashboardEventViewModel>>(events.Take(12).ToList());

            return View(model);
        }

        public ActionResult Events(int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;
            List<Event> events = BuildEventsAndSubscriptions();

            DashboardEventsViewModel model = new DashboardEventsViewModel() {
                CurrentPageIndex = currentPage,
                TotalRecords = events.Count(),
                Events = Mapper.Map<List<Event>, List<DashboardEventViewModel>>(events.Skip(currentPage * 12).Take(12).ToList())
            };

            return View(model);
        }

        private List<Event> BuildEventsAndSubscriptions() {
            List<Event> events = eventService.FindByUserID(CurrentUserID);
            List<UserFollowsEvent> subscribedEvents = CurrentUser.UserFollowsEvents.ToList();
            foreach (var item in subscribedEvents) {
                events.Add(item.Events.FirstOrDefault());
            }

            return events.OrderByDescending(e => e.StartDateTime).ToList();
        }

        public ActionResult Account() {
            DashboardAccountViewModel model = new DashboardAccountViewModel() {
                EmailAddress = CurrentUser.EmailAddress,
                CreatedDate = CurrentUser.CreatedDate
            };

            return View(model);
        }
    }
}