using System.Web.Mvc;
using System.Collections;
using Epilogger.Data;
using System.Collections.Generic;
using Epilogger.Web.Models;
using AutoMapper;
using System.Linq;
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

            DashboardIndexViewModel model = new DashboardIndexViewModel(activity.OrderByDescending(a => a.Date).ToList(),currentPage);

            return View(model);
        }

        public ActionResult Profile() {
            DashboardProfileViewModel model = Mapper.Map<User, DashboardProfileViewModel>(CurrentUser);

            return View(model);
        }

    }
}