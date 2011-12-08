using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Core.Stats;
using Epilogger.Web.Models;
using Epilogger.Common;

using RichmondDay.Helpers;
using System.Net;
using System.IO;
using System.Xml;

namespace Epilogger.Web.Controllers {
    public class EventsController : BaseController {
        string clientId = "GRBSH3HPYZYHIACLAL1GHGYHVHVWLJ0GGUUB1OLV41GV5EF1";
        string clientSecret = "FFCUYMPWPVTCP5AVNDS2VCA1JPTTR4FKCE35ZQUV3TKON5MH";
        string version = DateTime.Today.ToString("yyyyMMdd");

        EpiloggerDB db;
        EventService ES = new EventService();
        TweetService TS = new TweetService();
        ImageService IS = new ImageService();
        CheckInService CS = new CheckInService();
        ExternalLinkService LS = new ExternalLinkService();
        BlogService BS = new BlogService();
        CategoryService CatS = new CategoryService();
        UserService US = new UserService();
        VenueService venueService = new VenueService();

        DateTime _FromDateTime = DateTime.Parse("2000-01-01 00:00:00");
        private DateTime FromDateTime() {
            try {
                if (Request.QueryString["f"] != null) {
                    //_FromDateTime = DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["f"])).FromUserTimeZoneToUtc();
                    _FromDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["f"])));
                }
                return _FromDateTime;
            } catch (Exception) {
                return _FromDateTime;
            }
        }

        DateTime _ToDateTime = DateTime.Parse("2200-12-31 00:00:00");
        private DateTime ToDateTime() {
            try {
                if (Request.QueryString["t"] != null) {
                    //_ToDateTime = DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["t"])).FromUserTimeZoneToUtc();
                    _ToDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["t"])));
                }
                return _ToDateTime;
            } catch (Exception) {
                return _ToDateTime;
            }

        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (db == null) db = new EpiloggerDB();
            if (ES == null) ES = new EventService();
            if (TS == null) TS = new TweetService();
            if (IS == null) IS = new ImageService();
            if (CS == null) CS = new CheckInService();
            if (LS == null) LS = new ExternalLinkService();
            if (BS == null) BS = new BlogService();
            if (CatS == null) CatS = new CategoryService();
            if (US == null) US = new UserService();
            if (venueService == null) venueService = new VenueService();
            base.Initialize(requestContext);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult Category(string CategoryName)
        {
            BrowseCategoriesDisplayViewModel model = new BrowseCategoriesDisplayViewModel();
            List<Event> events = new List<Event>();
            
            if (CategoryName != string.Empty)
            {
                events = ES.GetEventsByCategorySlug(CategoryName);
            }
            
            List<DashboardEventViewModel> TheEvents = new List<DashboardEventViewModel>();
            model.Events = Mapper.Map<List<Event>, List<DashboardEventViewModel>>(events).OrderByDescending(f => f.StartDateTime);
            model.EventCategories = CatS.AllCategories();
            model.CategoryName = CatS.GetCategoryBySlug(CategoryName).CategoryName;


            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        
        public ActionResult Index(string filter, int? page) {

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
                    Epilogger.Data.Event e = ES.GetRandomEvent();
                    return RedirectToAction("details", new { id = e.ID });
                default:
                    filter = "overview";
                    model.UpcomingEvents = ES.UpcomingEvents();
                    model.EventCategories = CatS.AllCategories();

                    if (model.Authorized)
                    {
                        events = US.GetUserSubscribedAndCreatedEvents(CurrentUserID).Take(8).ToList();
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
                    events = US.GetUserSubscribedAndCreatedEvents(CurrentUserID).Take(8).ToList();
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

        public ActionResult Details(int id) {
            Event requestedEvent = ES.FindByID(id);
            EventDisplayViewModel Model = Mapper.Map<Event, EventDisplayViewModel>(requestedEvent);
            Model.TweetCount = TS.FindTweetCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.Tweets = TS.FindByEventIDOrderDescTake6(id, this.FromDateTime(), this.ToDateTime());
            Model.ImageCount = IS.FindImageCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.Images = IS.FindByEventIDOrderDescTake9(id, this.FromDateTime(), this.ToDateTime());
            Model.CheckInCount = CS.FindCheckInCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.CheckIns = CS.FindByEventIDOrderDescTake5(id, this.FromDateTime(), this.ToDateTime());
            Model.ExternalLinks = LS.FindByEventIDOrderDescTake3(id, this.FromDateTime(), this.ToDateTime());
            Model.BlogPosts = BS.FindByEventIDTake5(id, this.FromDateTime(), this.ToDateTime());
            Model.EventRatings = ES.FindEventRatingsByID(id, this.FromDateTime(), this.ToDateTime());
            Model.HasUserRated = false;
            Model.CurrentUserID = CurrentUserID;
            Model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

            //@(((Model.EventRatings.Sum(i => i.UserRating) / Model.EventRatings.Count()) / 5) * 100)


            if (Request.QueryString["f"] != null) {
                Model.FromDateTime = this.FromDateTime();
            } else {
                Model.FromDateTime = null;
            }
            if (Request.QueryString["t"] != null) {
                Model.ToDateTime = this.ToDateTime();
            } else {
                Model.ToDateTime = null;
            }


            //If there is a user logged in
            if (CurrentUserID != Guid.Empty) {
                Model.HasSubscribed = CurrentUser.UserFollowsEvents.Where(ufe => ufe.EventID == id).FirstOrDefault() != null ? true : false;
                if (Model.EventRatings.Where(i => i.UserID == CurrentUserID).Count() > 0) {
                    Model.HasUserRated = true;
                } else {
                    Model.HasUserRated = false;
                }
            }


            //Not optimized
            

            return View(Model);
        }

        private EventToolbarViewModel BuildToolbarViewModel(Event requestedEvent) {
            EventToolbarViewModel toolbarModel = new EventToolbarViewModel();
            toolbarModel.EventID = requestedEvent.ID;
            toolbarModel.CreatedByID = requestedEvent.UserID.Value;
            toolbarModel.CurrentUserID = CurrentUserID;
            toolbarModel.CurrentUserRole = CurrentUserRole;

            return toolbarModel;
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult Details(int id, FormCollection collection) {


            if (collection["ResetDates"] == "1")
            {
                return RedirectToAction("Details", new { id = id });
            }        
            else 
            {
                DateTime FromDateTime;
                DateTime ToDateTime;

                FromDateTime = DateTime.Parse(collection["FromDateTime"]);
                ToDateTime = DateTime.Parse(collection["ToDateTime"]);

                string encodedFrom = Epilogger.Web.Helpers.base64Encode(String.Format("{0:yyyy-MM-dd HH:mm:ss}", FromDateTime));
                string encodedTo = Epilogger.Web.Helpers.base64Encode(String.Format("{0:yyyy-MM-dd HH:mm:ss}", ToDateTime));


                return RedirectToAction("Details", new { id = id, f = encodedFrom, t = encodedTo });
            }
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult AllPhotos(int id, int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;
            Event requestedEvent = ES.FindByID(id);
            AllPhotosDisplayViewModel Model = Mapper.Map<Event, AllPhotosDisplayViewModel>(requestedEvent);
            Model.PhotoCount = IS.FindImageCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.CurrentPageIndex = currentPage;
            Model.ShowTopPhotos = false;
            Model.Images = IS.GetPagedPhotos(id, currentPage + 1, 30, this.FromDateTime(), this.ToDateTime());
            Model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

            if (currentPage + 1 == 1) {
                Model.ShowTopPhotos = true;
                Model.TopImages = IS.GetTopPhotosByEventID(id, 10, this.FromDateTime(), this.ToDateTime());
            }

            return View(Model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult AllTweets(int id, int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;

            Event requestedEvent = ES.FindByID(id);
            TopTweetersStats topTweetersStats = new TopTweetersStats();
            AllTweetsDisplayViewModel Model = Mapper.Map<Event, AllTweetsDisplayViewModel>(requestedEvent);
            Model.TweetCount = TS.FindTweetCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.UniqueTweeterCount = TS.FindUniqueTweetCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.CurrentPageIndex = currentPage;
            Model.TopTweeters = topTweetersStats.Calculate(TS.GetTop10TweetersByEventID(id, this.FromDateTime(), this.ToDateTime())).ToList();
            Model.ShowTopTweets = false;
            Model.Tweets = Mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetDisplayViewModel>>(TS.GetPagedTweets(id, currentPage + 1, 100, this.FromDateTime(), this.ToDateTime()));
            Model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

            if (currentPage + 1 == 1) {
                Model.ShowTopTweets = true;
                Model.Tweets = Mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetDisplayViewModel>>(TS.GetPagedTweets(id, currentPage + 1, 100, this.FromDateTime(), this.ToDateTime()));
            }

            return View(Model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult Create() {
            CreateEventViewModel Model = Mapper.Map<Event, CreateEventViewModel>(new Event());
            //Model.TimeZoneOffset = Helpers.GetUserTimeZoneOffset();

            DateTime roundTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, 0, 0);
            if (DateTime.UtcNow.Minute > 30)
            {
                roundTime = roundTime.AddHours(1);
            }

            Model.StartDateTime = roundTime;
            Model.EndDateTime = roundTime.AddHours(3);
            Model.CollectionStartDateTime = roundTime.AddDays(-2);
            Model.CollectionEndDateTime = roundTime.AddDays(3);
            Model.WebsiteURL = "http://";

            return View(Model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult Create(CreateEventViewModel model) {

            DateTime startDate;
            DateTime endDate;
            DateTime collectionStart;
            DateTime collectionEnd;

            DateTime.TryParse(Request.Form["start_date"] + " " + Request.Form["start_time"], out startDate); // start date
            DateTime.TryParse(Request.Form["end_date"] + " " + Request.Form["end_time"], out endDate); // end date (could be null)
            DateTime.TryParse(Request.Form["collection_start_date"] + " " + Request.Form["collection_start_time"], out collectionStart);
            DateTime.TryParse(Request.Form["collection_end_date"] + " " + Request.Form["collection_end_time"], out collectionEnd);

            //Adjust the timezone. this is becuase the EditTemplate is not returning the Time.
            model.StartDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(startDate);
            model.EndDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(endDate);
            model.CollectionStartDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(collectionStart);
            model.CollectionEndDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(collectionEnd);
            
            if (!string.IsNullOrEmpty(model.FoursquareVenueID))
            {
                // have to look up the foursquare venue and then create it and save it to the db.
                dynamic foursquareVenue = LookupFoursquareVenue(model.FoursquareVenueID);
                var locationNode = foursquareVenue.response.location;

                // convert it to a Venue
                Venue venue = new Venue();
                venue.FoursquareVenueID = foursquareVenue.response.id;
                venue.Address = locationNode.address;
                venue.Name = foursquareVenue.response.name;
                venue.City = locationNode.city;
                venue.State = locationNode.state;
                venue.Zip = locationNode.postalCode;
                venue.CrossStreet = locationNode.crossStreet;
                venue.Geolat = locationNode.lat;
                venue.Geolong = locationNode.lng;

                // save the venue
                venueService.Save(venue);
                model.VenueID = venue.ID;
                model.Venue = venue;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.UserID = CurrentUserID;
                    model.CreatedDateTime = DateTime.UtcNow;
                    model.EndDateTime = null;
                    model.CollectionEndDateTime = null;
                    
                    Event EPLevent = Mapper.Map<CreateEventViewModel, Event>(model);
                    ES.Save(EPLevent);

                    this.StoreSuccess("Your Event was created successfully!  Dont forget to share it with your friends and attendees!");

                    return RedirectToAction("details", new { id = EPLevent.ID });
                }
                catch (Exception ex)
                {
                    this.StoreError(string.Format("There was an error: {0}", ex.Message));
                    Event EPLevent = Mapper.Map<CreateEventViewModel, Event>(model);
                    model = Mapper.Map<Event, CreateEventViewModel>(EPLevent);
                    return View(model);
                }
            }

            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult EventBySlug(string eventSlug) {
            Event foundEvent = null;
            foreach (var e in db.Events) {
                if (e.Name.CreateUrlSlug() == eventSlug) {
                    foundEvent = e;
                    break;
                }
            }

            return View("Details", Mapper.Map<Event, EventDisplayViewModel>(foundEvent));
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult GetImageComments(int id) {
            return PartialView("_ImageComments", TS.FindByImageID(id));
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult GetLastTweetsJSON(int Count, string pageLoadTime, int EventID) {
            Dictionary<String, Object> dict = new Dictionary<String, Object>();

            if (pageLoadTime.Length > 0) {
                if (pageLoadTime != "undefined") {

                    pageLoadTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Parse(pageLoadTime));

                    EpiloggerDB db = TS.Thedb();
                    IEnumerable<Tweet> TheTweets = TS.Thedb().Tweets.Where(t => t.EventID == EventID & t.CreatedDate > DateTime.Parse(pageLoadTime)).OrderByDescending(t => t.CreatedDate).Take(Count);

                    StringBuilder HTMLString = new StringBuilder();
                    string lasttweettime = string.Empty;
                    bool TheFirst = true;
                    int RecordCount = 0;

                    foreach (Tweet TheT in TheTweets) {
                        if (TheFirst) {
                            lasttweettime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", TheT.CreatedDate);
                            TheFirst = false;
                        }

                        StringBuilder tweet = new StringBuilder();
                        tweet.AppendFormat("<li id='{0}' class='tweet clearfix'>", TheT.TwitterID);
                        tweet.AppendFormat("<img src='{0}' class='fleft' alt='' height='48' width='48'  />", TheT.ProfileImageURL);
                        tweet.Append("<div class='tweet-body'><strong>");
                        tweet.AppendFormat("<a href='http://www.twitter.com/{0}' target='_blank'>@{1}</a></strong>", TheT.FromUserScreenName, TheT.FromUserScreenName);
                        tweet.AppendFormat("<p>{0}</p>", TheT.TextAsHTML);
                        tweet.Append("</div>");
                        tweet.Append("</li>");

                        HTMLString.Append(tweet.ToString());
                        RecordCount++;
                    }


                    //Return the Dictionary as it's IEnumerable and it creates the correct JSON doc.
                    dict = new Dictionary<String, Object>();

                    dict.Add("numberofnewtweets", RecordCount);
                    dict.Add("lasttweettime", lasttweettime);
                    dict.Add("tweetcount", string.Format("{0:#,###}", db.Tweets.Where(t => t.EventID == EventID).Count()));
                    dict.Add("html", HTMLString.ToString());
                }
            }

            return Json(dict);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult GetLastPhotosJSON(int Count, string pageLoadTime, int EventID) {
            Dictionary<String, Object> dict = new Dictionary<String, Object>();

            if (pageLoadTime.Length > 0) {
                if (pageLoadTime != "undefined") {
                    pageLoadTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Parse(pageLoadTime));

                    EpiloggerDB db = TS.Thedb();
                    IEnumerable<Image> TheImages = db.Images.Where(t => t.EventID == EventID & t.DateTime > DateTime.Parse(pageLoadTime)).OrderByDescending(t => t.DateTime).Take(Count);

                    StringBuilder HTML = new StringBuilder();
                    string lastphototime = string.Empty;
                    bool TheFirst = true;
                    int RecordCount = 0;

                    foreach (Image TheI in TheImages) {
                        if (TheFirst) {
                            lastphototime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", TheI.DateTime);
                            TheFirst = false;
                        }

                        string TheImage = "<a href='" + TheI.Fullsize + "' rel='prettyPhoto[latestphotos]' title='" + TheI.ID + "' id='" + TheI.ID + "'><img src='" + TheI.Fullsize + "' width='200' border='0' alt='' /></a>";
                        string CommentCount = "<a href='#' class='commentbubble'>" + TheI.ImageMetaData.Count() + "</a>";

                        HTML.Append("<div class='withcomment newPhotoupdates' style='display:none;' id='photo-'" + TheI.ID + "'>" + TheImage + CommentCount + "</div>");


                        RecordCount++;
                    }

                    //Return the Dictionary as it's IEnumerable and it creates the correct JSON doc.
                    dict = new Dictionary<String, Object>();

                    dict.Add("numberofnewphotos", RecordCount);
                    dict.Add("lastphototime", lastphototime);
                    dict.Add("photocount", string.Format("{0:#,###}", db.Images.Where(t => t.EventID == EventID).Count()));
                    dict.Add("html", HTML.ToString());
                }
            }

            return Json(dict);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public bool UpdateSubscription(int EventID, bool HasSubscribed) {
            //Save the Selectiob
            if (HasSubscribed) {
                HasSubscribed = false;
            } else {
                HasSubscribed = true;
            }


            return HasSubscribed;
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult Subscribe(FormCollection fc) {
            int id;
            int.TryParse(fc["ID"].ToString(), out id);
            if (id > 0) {
                if (CurrentUserID == Guid.Empty) {
                    this.StoreWarning("You must be logged in to your epilogger account to subscribe to an event");
                    return RedirectToAction("details", new { id = id });
                }

                UserService service = new UserService();
                UserFollowsEvent followsEvent = new UserFollowsEvent();
                followsEvent.EventID = id;
                followsEvent.UserID = CurrentUserID;
                followsEvent.Timestamp = DateTime.Now;

                service.SaveUserFollowsEvent(followsEvent);

                this.StoreSuccess("You are now subscribed to this event!");
            }

            return RedirectToAction("details", new { id = id });
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult UnSubscribe(FormCollection fc) {
            int id;
            int.TryParse(fc["ID"].ToString(), out id);

            if (id > 0) {
                if (CurrentUserID == Guid.Empty) {
                    this.StoreWarning("You must be logged in to your epilogger account to subscribe to an event");
                    return RedirectToAction("details", new { id = id });
                }

                UserService service = new UserService();
                UserFollowsEvent followsEvent = new UserFollowsEvent();
                followsEvent.EventID = id;
                followsEvent.UserID = CurrentUserID;

                service.DeleteEventSubscription(CurrentUserID, id);

                this.StoreSuccess("You are no longer subscribed to this event!");
            }

            return RedirectToAction("details", new { id = id });
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        //Depricated
        //See StarRating
        [HttpPost]
        public ActionResult eventRating(FormCollection fc) {
            int id;
            string ThumbsUp;
            int.TryParse(fc["ID"].ToString(), out id);
            ThumbsUp = fc["ThumbsUp"].ToString();

            if (id > 0) {
                if (CurrentUserID == Guid.Empty) {
                    this.StoreWarning("You must be logged in to your epilogger account to subscribe to an event");
                    return RedirectToAction("details", new { id = id });
                }

                UserService service = new UserService();
                UserRatesEvent ratesEvent = new UserRatesEvent();

                ratesEvent.EventID = id;
                ratesEvent.UserID = CurrentUserID;
                ratesEvent.RatingDateTime = DateTime.UtcNow;

                if (ThumbsUp == "1") {
                    //ratesEvent.UserRating = "+";
                } else {
                    //ratesEvent.UserRating = "-";
                }

                service.SaveUserRatesEvent(ratesEvent);
                this.StoreSuccess("Rating saved!");

            }

            return RedirectToAction("details", new { id = id });
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult Search(int id, string IEsearchterm)
        {

            SearchInEventViewModel s = new SearchInEventViewModel();
            s.ID = id;
            s.SearchTerm = IEsearchterm;
            Event ThisEvent = new Event();
            ThisEvent = ES.FindByID(id);
            s.Name = ThisEvent.Name;
            if (!string.IsNullOrEmpty(IEsearchterm)) {
                s.SearchResults = ES.SearchInEvent(id, IEsearchterm, this.FromDateTime(), this.ToDateTime());
            }
            s.ToolbarViewModel = BuildToolbarViewModel(ThisEvent);

            return View(s);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult Search(FormCollection fc)
        {
            int id;
            string SearchTerm;
            int.TryParse(fc["ID"].ToString(), out id);
            SearchTerm = fc["SearchTerm"].ToString();

            return RedirectToAction("Search", new { id = id, IEsearchterm = SearchTerm });

            //SearchInEventViewModel s = new SearchInEventViewModel();
            //s.ID = id;
            //s.SearchTerm = SearchTerm;
            //Event ThisEvent = new Event();
            //ThisEvent = ES.FindByID(id);
            //s.Name = ThisEvent.Name;
            //s.SearchResults = ES.SearchInEvent(id, SearchTerm, this.FromDateTime(), this.ToDateTime());

            //s.ToolbarViewModel = BuildToolbarViewModel(ThisEvent);

            //return View(s);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult AllContent(int id) {
            AllContentViewModel model = Mapper.Map<Event, AllContentViewModel>(ES.FindByID(id));
            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult AllBlogPosts(int id, int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;
            Event requestedEvent = ES.FindByID(id);
            List<BlogPostDisplayViewModel> blogPosts = Mapper.Map<List<BlogPost>, List<BlogPostDisplayViewModel>>(BS.FindByEventID(id).ToList());

            AllBlogPostsViewModel model = Mapper.Map<Event, AllBlogPostsViewModel>(requestedEvent);
            model.SetAllBlogPostsViewModel(blogPosts, currentPage, blogPosts.Count());
            model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult AllCheckins(int id, int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;

            Event currentEvent = ES.FindByID(id);
            List<CheckinDisplayViewModel> checkins = Mapper.Map<List<CheckIn>, List<CheckinDisplayViewModel>>(CS.FindByEventIDPaged(id, currentPage, 10, this.FromDateTime(), this.ToDateTime()).ToList());

            AllCheckinsViewModel model = new AllCheckinsViewModel(checkins, currentPage, 10);
            model.ID = currentEvent.ID.ToString();
            model.Name = currentEvent.Name;
            model.TotalRecords = CS.FindCheckInCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            model.ToolbarViewModel = BuildToolbarViewModel(currentEvent);

            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult AllLinks(int id, int? page) {

            int currentPage = page.HasValue ? page.Value - 1 : 0;
            Event requestedEvent = ES.FindByID(id);
            
            AllLinksViewModel model = Mapper.Map<Event, AllLinksViewModel>(requestedEvent);
            model.Links = LS.FindByEventIDPaged(id, currentPage, 10, this.FromDateTime(), this.ToDateTime());
            model.CurrentPageIndex = currentPage;
            model.TotalRecords = LS.FindCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult AllStats(int id)
        {

            //int currentPage = page.HasValue ? page.Value - 1 : 0;
            Event requestedEvent = ES.FindByID(id);
            AllStatsViewModel Model = Mapper.Map<Event, AllStatsViewModel>(requestedEvent);

            if (Request.QueryString["f"] != null)
            {
                Model.FromDateTime = this.FromDateTime();
            }
            else
            {
                Model.FromDateTime = null;
            }
            if (Request.QueryString["t"] != null)
            {
                Model.ToDateTime = this.ToDateTime();
            }
            else
            {
                Model.ToDateTime = null;
            }
            Model.MyUTCNow = DateTime.UtcNow;

            Model.TweetCount = TS.FindTweetCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.ImageCount = IS.FindImageCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.ExternalLinkCount = LS.FindCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.TopImages = Model.TopImages = IS.GetTopPhotosAndTweetByEventID(id, 10, this.FromDateTime(), this.ToDateTime());
            Model.TopLinks = LS.GetTopURLsByEventID(id, 5, this.FromDateTime(), this.ToDateTime());

            List<CheckinDisplayViewModel> checkins = Mapper.Map<List<CheckIn>, List<CheckinDisplayViewModel>>(CS.FindByEventID(id, this.FromDateTime(), this.ToDateTime()).ToList());
            Model.AllCheckIns = checkins;

            TopTweetersStats topTweetersStats = new TopTweetersStats();
            Model.TopTweeters = Model.TopTweeters = topTweetersStats.Calculate(TS.GetTop10TweetersByEventID(id, this.FromDateTime(), this.ToDateTime())).ToList();

            Model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

            //model.Links = LS.FindByEventIDPaged(id, currentPage, 10, this.FromDateTime(), this.ToDateTime());
            //model.CurrentPageIndex = currentPage;
            //model.TotalRecords = LS.FindCountByEventID(id, this.FromDateTime(), this.ToDateTime());


            return View(Model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult Edit(int id) {
            Event currentEvent = ES.FindByID(id);
            CreateEventViewModel model = Mapper.Map<Event, CreateEventViewModel>(currentEvent);
            model.ToolbarViewModel = BuildToolbarViewModel(currentEvent);

            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult Edit(FormCollection fc, CreateEventViewModel model) {
            if (ModelState.IsValid) {
                Event currentEvent = ES.FindByID(model.ID);
                try {
                    currentEvent.CategoryID = model.CategoryID;
                    currentEvent.SubTitle = model.Subtitle;
                    currentEvent.Name = model.Name;
                    currentEvent.SearchTerms = model.SearchTerms;
                    currentEvent.Description = model.Description;
                    currentEvent.TwitterAccount = model.TwitterAccount;
                    currentEvent.FacebookPageURL = model.FacebookPageURL;
                    currentEvent.WebsiteURL = !string.IsNullOrEmpty(model.WebsiteURL) && !model.WebsiteURL.StartsWith("http://") ? 
                        model.WebsiteURL.Insert(0, "http://") : 
                        model.WebsiteURL;
                    currentEvent.Cost = model.Cost;
                    currentEvent.TimeZoneOffset = model.TimeZoneOffset;

                    DateTime startDate;
                    DateTime endDate;
                    DateTime collectionStart;
                    DateTime collectionEnd;

                    DateTime.TryParse(Request.Form["StartDateTime"], out startDate); // start date
                    DateTime.TryParse(Request.Form["EndDateTime"], out endDate); // end date (could be null)
                    DateTime.TryParse(Request.Form["CollectionStartDateTime"], out collectionStart);
                    DateTime.TryParse(Request.Form["CollectionEndDateTime"], out collectionEnd);

                    //Adjust the timezone. this is becuase the EditTemplate is not returning the Time.
                    currentEvent.StartDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(startDate);
                    if (endDate != DateTime.MinValue)
                    {
                        currentEvent.EndDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(endDate);
                    }

                    currentEvent.CollectionStartDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(collectionStart); 
                    if (collectionEnd != DateTime.MinValue) {
                        currentEvent.CollectionEndDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(collectionEnd);
                    }

                    currentEvent.VenueID = model.VenueID;

                    if (!string.IsNullOrEmpty(model.FoursquareVenueID))
                    {
                        if (!venueService.DoesVenueExist(currentEvent.VenueID ?? 0))
                        {
                            // have to look up the foursquare venue and then create it and save it to the db.
                            dynamic foursquareVenue = LookupFoursquareVenue(model.FoursquareVenueID);
                            var locationNode = foursquareVenue.response.location;

                            // convert it to a Venue
                            Venue venue = new Venue();
                            venue.FoursquareVenueID = foursquareVenue.response.id;
                            venue.Address = locationNode.address;
                            venue.Name = foursquareVenue.response.name;
                            venue.City = locationNode.city;
                            venue.State = locationNode.state;
                            venue.CrossStreet = locationNode.crossStreet;
                            venue.Geolat = locationNode.lat;
                            venue.Geolong = locationNode.lng;

                            // save the venue
                            venueService.Save(venue);
                            currentEvent.VenueID = venue.ID;
                        }
                    }

                    //Quick Hack
                    if (model.EndDateTime == DateTime.MinValue)
                    {
                        model.EndDateTime = null;
                    }
                    if (model.CollectionEndDateTime == DateTime.MinValue)
                    {
                        model.CollectionEndDateTime = null;
                    }

                    
                    ES.Save(currentEvent);
                    this.StoreSuccess("Your event was updated successfully!  Make sure you let all your friends know about the changes you just made!");
                    model = Mapper.Map<Event, CreateEventViewModel>(currentEvent);
                } catch (Exception ex) {
                    this.StoreError(string.Format("There was an error: {0}", ex.Message));
                    model = Mapper.Map<Event, CreateEventViewModel>(currentEvent);
                    return View(model);
                }
            }
            
            return RedirectToAction("edit", new { id = model.ID});
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult VenueSearch() {

            //TODO replace with IP geo coded data.
            VenueSearchModel VSM = new VenueSearchModel();
            VSM.City = "Toronto";
            VSM.ProvinceState = "ON";

            return PartialView(VSM);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public PartialViewResult SearchVenues(FormCollection fc) {
            StringBuilder location = new StringBuilder();
            if (fc["address"] != null) {
                location.AppendFormat("{0},",fc["address"].ToString());
            }
            if (fc["state"] != null) {
                location.AppendFormat("{0},",fc["state"].ToString());
            }
            if (fc["city"] != null) {
                location.AppendFormat("{0},",fc["city"].ToString());
            }
            if (fc["zip"] != null) {
                location.AppendFormat("{0},", fc["zip"].ToString());
            }
            
            string url = string.Format("http://maps.google.com/maps/geo?output=csv&q={0}", Url.Encode(location.ToString().TrimEnd(',')));
            string results = GetResults(url, null, "Get");
            var parts = results.Split(',');

            Double longitude = Convert.ToDouble(parts[2]);
            Double latitude = Convert.ToDouble(parts[3]);

            var venues = FoursquareVenueSearch(fc["name"] as string ?? "", longitude, latitude);

            List<FoursquareVenue> foundVenues = new List<FoursquareVenue>();
            foreach (var item in venues.response) {
                foundVenues.Add(new FoursquareVenue { id = item.id, Name = item.name, Address = item.location.address, City = item.location.city, State = item.location.state, Zip = item.location.postalCode });
            }

            return PartialView("_VenueSearchResults", foundVenues);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        #region venue search helpers
        private dynamic FoursquareVenueSearch(string venueName, Double longitude, Double latitude) {
            string searchRequest = string.Format("https://api.foursquare.com/v2/venues/search?ll={0}&query={1}&client_id={2}&client_secret={3}&v={4}",
                string.Format("{0},{1}", longitude, latitude),
                venueName,
                clientId,
                clientSecret,
                version);

            var client = new FoursquareVenueClient();
            var venues = client.Execute(searchRequest);

            return venues;
        }

        private dynamic LookupFoursquareVenue(string venueId) {
            string searchRequest = string.Format("https://api.foursquare.com/v2/venues/{0}?client_id={1}&client_secret={2}&v={3}",
                venueId,
                clientId,
                clientSecret,
                version);

            var client = new FoursquareVenueClient();
            var venue = client.Execute(searchRequest);

            return venue;
        }

        string GetResults(string url, string postData, string method) {
            string returnValue = string.Empty;
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(method)) {
                webRequest.Method = method;

                if (!string.IsNullOrEmpty(postData)) {
                    // posting data to a url
                    byte[] byteSend = Encoding.ASCII.GetBytes(postData);
                    webRequest.ContentLength = byteSend.Length;

                    using (Stream streamOut = webRequest.GetRequestStream())
                        streamOut.Write(byteSend, 0, byteSend.Length);
                }
            } else
                webRequest.Method = "GET";

            WebResponse webResponse = webRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                if (streamReader.Peek() > -1) return streamReader.ReadToEnd();

            return "";
        } 
        #endregion

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult AddBlogPost(int id) {
            AddBlogPostViewModel model = new AddBlogPostViewModel();
            model.BlogURL = "http://";
            model.EventID = id;

            return PartialView(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost, ValidateInput(false)]
        public bool AddBlogPost(int id, AddBlogPostViewModel model) {
            try {
                BlogPost blogPost = Mapper.Map<AddBlogPostViewModel, BlogPost>(model);
                blogPost.EventID = id;
                blogPost.UserID = CurrentUserID;
                blogPost.DateTime = DateTime.UtcNow;
                BS.Save(blogPost);

                return true;
            } catch (Exception ex) {
                return false;
            }
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult AddLink() {
            return PartialView();
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost, ValidateInput(false)]
        public bool AddLink(int id, AddLinkViewModel model) {
            try {
                URL externalLink = Mapper.Map<AddLinkViewModel, URL>(model);

                externalLink.DateTime = DateTime.UtcNow;
                externalLink.Deleted = false;
                externalLink.EventID = id;
                externalLink.Type = "User Submitted";

                //Need to save with no twitterID, BUT the db has a FK relationship. Need to break it and fix the rest of EPL.
                //Removing the link for now.


                //LS.Save(externalLink);

                return true;
            } catch (Exception ex) {
                return false;
            }
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public ActionResult StarRatings()
        {
            return PartialView("_StarRatingTemplate");
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult StarRatings(FormCollection fc)
        {
            int id;
            int UserRating;
            int.TryParse(fc["ID"].ToString(), out id);
            int.TryParse(fc["UserRating"].ToString(), out UserRating);

            if (id > 0)
            {
                if (CurrentUserID == Guid.Empty)
                {
                    this.StoreWarning("You must be logged in to your epilogger account to subscribe to an event");
                    return RedirectToAction("details", new { id = id });
                }

                UserService service = new UserService();
                UserRatesEvent ratesEvent = new UserRatesEvent();

                ratesEvent.EventID = id;
                ratesEvent.UserID = CurrentUserID;
                ratesEvent.RatingDateTime = DateTime.UtcNow;
                ratesEvent.UserRating = UserRating;

                service.SaveUserRatesEvent(ratesEvent);
                this.StoreSuccess("Rating saved!");

            }

            return RedirectToAction("details", new { id = id });
        }


//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public bool Delete(int EventID)
        {
            try
            {
                MQ.MSGProducer MP = new MQ.MSGProducer("Epilogger", "System");
                MQ.Messages.SystemProcessingMSG DeleteMSG = new MQ.Messages.SystemProcessingMSG();
                DeleteMSG.EventID = EventID;
                DeleteMSG.Task = MQ.Messages.SystemMessageType.Delete;
                MP.SendMessage(DeleteMSG);
                MP.Dispose();

                this.StoreSuccess("Your event has been added to the Delete queue! Due to the large volumn of data, your event may take a few minutes to be removed from the system.");
            }
            catch (Exception)
            {    
                throw;
            }
            
            return true;
        }
    
    }

}
