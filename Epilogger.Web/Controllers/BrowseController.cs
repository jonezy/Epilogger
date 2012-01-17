using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epilogger.Web.Models;
using Epilogger.Data;
using AutoMapper;

namespace Epilogger.Web.Controllers
{
    public class BrowseController : BaseController
    {
        EpiloggerDB db;
        EventService ES = new EventService();
        TweetService TS = new TweetService();
        ImageService IS = new ImageService();
        //CheckInService CS = new CheckInService();
        //ExternalLinkService LS = new ExternalLinkService();
        //BlogService BS = new BlogService();
        CategoryService CatS = new CategoryService();
        UserService US = new UserService();
        //VenueService venueService = new VenueService();

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (db == null) db = new EpiloggerDB();
            if (ES == null) ES = new EventService();
            if (TS == null) TS = new TweetService();
            if (IS == null) IS = new ImageService();
            //if (CS == null) CS = new CheckInService();
            //if (LS == null) LS = new ExternalLinkService();
            //if (BS == null) BS = new BlogService();
            if (CatS == null) CatS = new CategoryService();
            if (US == null) US = new UserService();
            //if (venueService == null) venueService = new VenueService();
            base.Initialize(requestContext);
        }


        //public ActionResult Index()
        //{
        //    return RedirectToAction("Index", new { filter = "overview" });
        //}
        public ActionResult Index(string filter, int? page)
        {

            int currentPage = page.HasValue ? page.Value - 1 : 0;

            BrowseEventsDisplayViewModel model = new BrowseEventsDisplayViewModel();

            if (CurrentUserID == Guid.Empty)
            {
                model.Authorized = false;
            }
            else
            {
                model.Authorized = true;
            }

            List<Event> events = new List<Event>();
            //IEnumerable<Event> hottestevents = ES.GetHottestEvents(5);
            Epilogger.Data.Event randomEvent = ES.GetRandomEvent();
            model.RandomEvent = ES.RandomUpcomingEvent();
            //Fillthe events of the time selected
            switch (filter)
            {
                case "upcoming":
                    events = ES.UpcomingEventsPaged(currentPage, 10);
                    break;
                case "past":
                    events = ES.PastEventsPaged(currentPage, 10);
                    model.TotalRecords = ES.PastEventCount();
                    break;
                case "now":
                    events = ES.GoingOnNowEventsPaged(currentPage, 10);
                    model.TotalRecords = ES.GoingOnNowEventsCount();
                    break;
                case "subscribed":
                    events = BuildEventSubscriptions(currentPage, 10);
                    model.TotalRecords = CurrentUser.UserFollowsEvents.Count();
                    break;
                case "myevents":
                    events = ES.FindByUserIDPaged(CurrentUserID, currentPage, 10);
                    model.TotalRecords = ES.FindCountByUserID(CurrentUserID);
                    break;

                case "random":

                    return RedirectToAction("details", new { id = randomEvent.EventSlug });
                default:
                    filter = "overview";
                    model.UpcomingEvents = ES.UpcomingEvents();
                    model.EventCategories = CatS.AllCategories();

                    if (model.Authorized)
                    {
                        try
                        {
                            events = US.GetUserSubscribedAndCreatedEvents(CurrentUserID, 8).ToList();
                        }
                        catch { }
                    }
                    else
                    {
                        events = ES.TodaysEvents();
                    }

                    break;
            }

            model.BrowsePageFilter = filter;
            model.CurrentPageIndex = currentPage;
            model.Events = Mapper.Map<List<Event>, List<DashboardEventViewModel>>(events);

            //For the Overview page, the hottest events
            model.HottestEvents = new List<HotestEventsModel>();
            foreach (Epilogger.Data.Event item in ES.GetHottestEvents(4))
            {
                HotestEventsModel HE = new HotestEventsModel();
                HE.Event = item;
                HE.RandomHottestImages = IS.GetRandomImagesByEventID(item.ID, 10);
                HE.TweetCount = TS.FindTweetCountByEventID(item.ID, DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2200-12-31 00:00:00"));
                HE.PhotoCount = IS.FindImageCountByEventID(item.ID, DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2200-12-31 00:00:00"));
                model.HottestEvents.Add(HE);
            }

            return View(model);
        }

        private List<Event> BuildEventSubscriptions(int currentPage, int recordsPerPage)
        {
            List<Event> events = new List<Event>();
            List<UserFollowsEvent> subscribedEvents = CurrentUser.UserFollowsEvents.ToList();
            foreach (var item in subscribedEvents)
            {
                events.Add(item.Events.FirstOrDefault());
            }

            return events.Skip(currentPage * recordsPerPage).Take(recordsPerPage).OrderByDescending(c => c.StartDateTime).ToList();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult GetBrowseOverviewTabData(int Tab)
        {
            List<Event> events = new List<Event>();

            switch (Tab)
            {
                case 1: //My Events
                    events = US.GetUserSubscribedAndCreatedEvents(CurrentUserID, 8).ToList();
                    break;
                case 2: //Today
                    events = ES.TodaysEvents();
                    break;
            }
            List<DashboardEventViewModel> TheEvents = new List<DashboardEventViewModel>();
            TheEvents = Mapper.Map<List<Event>, List<DashboardEventViewModel>>(events);

            //PartialViewResult TabContent = PartialView("_BrowseEventTabContentTemplate.cshtml", TheEvents);
            //return TabContent;

            return PartialView("_BrowseEventTabContentTemplate", TheEvents);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------










    }
}
