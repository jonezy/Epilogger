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
            IEnumerable<DashboardActivityModel> activity = userService.GetUserDashboardActivity(CurrentUser.ID);
            DashboardIndexViewModel model = new DashboardIndexViewModel(
                activity.OrderByDescending(a => a.Date).Skip(currentPage * 12).Take(12).ToList(),
                currentPage,
                activity.Count()
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
            List<Event> events = eventService.FindByUserID(CurrentUserID);

            DashboardEventsViewModel model = new DashboardEventsViewModel() {
                CurrentPageIndex = currentPage,
                TotalRecords = events.Count(),
                Events = Mapper.Map<List<Event>, List<DashboardEventViewModel>>(events.Skip(currentPage * 12).Take(12).ToList())
            };

            return View(model);
        }

        public ActionResult AllEvents(int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;
            List<Event> events = eventService.FindByUserID(CurrentUserID);

            DashboardEventsViewModel model = new DashboardEventsViewModel() {
                CurrentPageIndex = currentPage,
                TotalRecords = events.Count(),
                Events = Mapper.Map<List<Event>, List<DashboardEventViewModel>>(events.Skip(currentPage * 12).Take(12).ToList())
            };

            return View(model);
        }

        public ActionResult Subscriptions(int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;
            List<Event> events = BuildEventSubscriptions();

            DashboardEventsViewModel model = new DashboardEventsViewModel() {
                CurrentPageIndex = currentPage,
                TotalRecords = events.Count(),
                Events = Mapper.Map<List<Event>, List<DashboardEventViewModel>>(events.Skip(currentPage * 12).Take(12).ToList())
            };

            return View(model);
        }

        public ActionResult Account() {
            DashboardAccountViewModel model = new DashboardAccountViewModel() {
                EmailAddress = CurrentUser.EmailAddress,
                CreatedDate = CurrentUser.CreatedDate
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

        private List<Event> BuildEventSubscriptions() {
            List<Event> events = new List<Event>();
            List<UserFollowsEvent> subscribedEvents = CurrentUser.UserFollowsEvents.ToList();
            foreach (var item in subscribedEvents) {
                events.Add(item.Events.FirstOrDefault());
            }

            return events.OrderByDescending(e => e.StartDateTime).ToList();
        }

    }
}