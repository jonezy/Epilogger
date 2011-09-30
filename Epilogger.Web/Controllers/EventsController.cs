using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Core.Stats;
using Epilogger.Web.Models;

using RichmondDay.Helpers;
using System.Net;
using System.IO;
using System.Xml;

namespace Epilogger.Web.Controllers {
    public class EventsController : BaseController {
        EpiloggerDB db;
        EventService ES = new EventService();
        TweetService TS = new TweetService();
        ImageService IS = new ImageService();
        CheckInService CS = new CheckInService();
        ExternalLinkService LS = new ExternalLinkService();
        BlogService BS = new BlogService();
        CategoryService CatS = new CategoryService();
        UserService US = new UserService();

        DateTime _FromDateTime = DateTime.Parse("2000-01-01 00:00:00");
        private DateTime FromDateTime() {
            try {
                if (Request.QueryString["f"] != null) {
                    _FromDateTime = DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["f"])).FromUserTimeZoneToUtc();
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
                    _ToDateTime = DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["t"])).FromUserTimeZoneToUtc();
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

            base.Initialize(requestContext);
        }

        public ActionResult Index(string filter) {


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
            IEnumerable<Event> hottestevents = ES.GetHottestEvents(10);

            //Fillthe events of the time selected
            switch (filter)
            {
                case "upcoming":
                    events = ES.UpcomingEvents();
                    break;
                case "past":
                    events = ES.PastEvents();
                    break;
                case "now":
                    events = ES.GoingOnNowEvents().OrderBy(ne=>ne.EndDateTime.GetValueOrDefault()).ToList();
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

            model.Events = Mapper.Map<List<Event>, List<DashboardEventViewModel>>(events);
            //Not use if this is needed yet.
            //model.Events = events;

            
            //For the Overview page, the hottest events


            model.HottestEvents = new List<HotestEventsModel>();
            foreach (Epilogger.Data.Event item in ES.GetHottestEvents(10))
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


        public ActionResult Details(int id) {
            EventDisplayViewModel Model = Mapper.Map<Event, EventDisplayViewModel>(ES.FindByID(id));
            Model.TweetCount = TS.FindTweetCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.Tweets = TS.FindByEventIDOrderDescTake6(id, this.FromDateTime(), this.ToDateTime());
            Model.ImageCount = IS.FindImageCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.Images = IS.FindByEventIDOrderDescTake9(id, this.FromDateTime(), this.ToDateTime());
            Model.CheckInCount = CS.FindCheckInCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.CheckIns = CS.FindByEventIDOrderDescTake5(id, this.FromDateTime(), this.ToDateTime());
            Model.ExternalLinks = LS.FindByEventIDOrderDescTake3(id, this.FromDateTime(), this.ToDateTime());
            Model.EventRatings = ES.FindEventRatingsByID(id, this.FromDateTime(), this.ToDateTime());
            Model.HasUserRated = false;
            Model.CurrentUserID = CurrentUserID;

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
            Model.BlogPosts = BS.FindByEventID(id);

            return View(Model);
        }

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

        public ActionResult AllPhotos(int id, int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;

            AllPhotosDisplayViewModel Model = Mapper.Map<Event, AllPhotosDisplayViewModel>(ES.FindByID(id));
            Model.PhotoCount = IS.FindImageCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.CurrentPageIndex = currentPage;
            Model.ShowTopPhotos = false;
            Model.Images = IS.GetPagedPhotos(id, currentPage + 1, 30, this.FromDateTime(), this.ToDateTime());

            if (currentPage + 1 == 1) {
                Model.ShowTopPhotos = true;
                Model.Images = IS.GetPagedPhotos(id, currentPage + 1, 30, this.FromDateTime(), this.ToDateTime());
            }

            return View(Model);
        }

        public ActionResult AllTweets(int id, int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;

            TopTweetersStats topTweetersStats = new TopTweetersStats();
            AllTweetsDisplayViewModel Model = Mapper.Map<Event, AllTweetsDisplayViewModel>(ES.FindByID(id));
            Model.TweetCount = TS.FindTweetCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.UniqueTweeterCount = TS.FindUniqueTweetCountByEventID(id, this.FromDateTime(), this.ToDateTime());
            Model.CurrentPageIndex = currentPage;
            Model.TopTweeters = topTweetersStats.Calculate(TS.GetTop10TweetersByEventID(id, this.FromDateTime(), this.ToDateTime())).ToList();
            Model.ShowTopTweets = false;
            Model.Tweets = Mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetDisplayViewModel>>(TS.GetPagedTweets(id, currentPage + 1, 100, this.FromDateTime(), this.ToDateTime()));

            if (currentPage + 1 == 1) {
                Model.ShowTopTweets = true;
                Model.Tweets = Mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetDisplayViewModel>>(TS.GetPagedTweets(id, currentPage + 1, 100, this.FromDateTime(), this.ToDateTime()));
            }

            return View(Model);
        }

        [RequiresAuthentication(AccessDeniedMessage = "You must be logged in to view the details of that event")]
        public ActionResult Create() {
            CreateEventViewModel Model = Mapper.Map<Event, CreateEventViewModel>(new Event());
            Model.TimeZoneOffset = Helpers.GetUserTimeZoneOffset();

            DateTime roundTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            if (DateTime.Now.Minute > 30) {
                roundTime.AddHours(1);
            }

            Model.StartDateTime = roundTime;
            //Model.EndDateTime = roundTime.AddHours(3);
            Model.CollectionStartDateTime = roundTime.AddDays(-2);
            Model.CollectionEndDateTime = roundTime.AddDays(3);

            return View(Model);
        }

        [HttpPost]
        public ActionResult Create(CreateEventViewModel model) {
            if (ModelState.IsValid) {
                try {
                    model.UserID = CurrentUserID;
                    model.CreatedDateTime = DateTime.UtcNow;
                    model.EndDateTime = null;
                    model.CollectionEndDateTime = null;

                    // get yer dates.
                    // 7 is start date, 8 is end
                    DateTime startDate;
                    DateTime endDate;
                    DateTime.TryParse(Request.Form[7], out startDate); // start date
                    DateTime.TryParse(Request.Form[8], out endDate); // end date (could be null)

                    model.CollectionStartDateTime = startDate.FromUserTimeZoneToUtc().AddDays(-2);
                    if (endDate != DateTime.MinValue) {
                        model.CollectionEndDateTime = endDate.FromUserTimeZoneToUtc().AddDays(3);
                    }

                    model.StartDateTime = startDate.FromUserTimeZoneToUtc();
                    if(endDate != DateTime.MinValue) {
                        model.EndDateTime = endDate.FromUserTimeZoneToUtc();
                    }

                    Event EPLevent = Mapper.Map<CreateEventViewModel, Event>(model);
                    ES.Save(EPLevent);

                    this.StoreSuccess("Your Event was created successfully!  Dont forget to share it with your friends and attendees!");
                    
                    return RedirectToAction("details", new { id = EPLevent.ID });
                } catch (Exception ex) {
                    this.StoreError(string.Format("There was an error: {0}", ex.Message));
                    Event EPLevent = Mapper.Map<CreateEventViewModel, Event>(model);
                    model = Mapper.Map<Event, CreateEventViewModel>(EPLevent);
                    return View(model);
                }
            }
            Event tempEvent = Mapper.Map<CreateEventViewModel, Event>(model);
            model = Mapper.Map<Event, CreateEventViewModel>(tempEvent);
            return View(model);
        }

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

        public ActionResult GetImageComments(int id) {
            return PartialView("_ImageComments", TS.FindByImageID(id));
        }

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

        public bool UpdateSubscription(int EventID, bool HasSubscribed) {
            //Save the Selectiob
            if (HasSubscribed) {
                HasSubscribed = false;
            } else {
                HasSubscribed = true;
            }


            return HasSubscribed;
        }

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
                    ratesEvent.UserRating = "+";
                } else {
                    ratesEvent.UserRating = "-";
                }

                service.SaveUserRatesEvent(ratesEvent);
                this.StoreSuccess("Rating saved!");

            }

            return RedirectToAction("details", new { id = id });
        }

        public ActionResult AllContent(int id) {
            AllContentViewModel model = Mapper.Map<Event, AllContentViewModel>(ES.FindByID(id));
            return View(model);
        }

        public ActionResult AllBlogPosts(int id) {
            AllBlogPostsViewModel model = Mapper.Map<Event, AllBlogPostsViewModel>(ES.FindByID(id));
            return View(model);
        }

        public ActionResult AllCheckins(int id, int? page) {
            int currentPage = page.HasValue ? page.Value - 1 : 0;

            Event currentEvent = ES.FindByID(id);
            List<CheckinDisplayViewModel> checkins = Mapper.Map<List<CheckIn>, List<CheckinDisplayViewModel>>(CS.FindByEventIDPaged(id, currentPage, 10, this.FromDateTime(), this.ToDateTime()).ToList());

            AllCheckinsViewModel model = new AllCheckinsViewModel(checkins, currentPage, 10);
            model.ID = currentEvent.ID.ToString();
            model.Name = currentEvent.Name;
            model.TotalRecords = CS.FindCheckInCountByEventID(id, this.FromDateTime(), this.ToDateTime());

            return View(model);
        }

        public ActionResult AllLinks(int id, int? page) {

            int currentPage = page.HasValue ? page.Value - 1 : 0;
            AllLinksViewModel model = Mapper.Map<Event, AllLinksViewModel>(ES.FindByID(id));

            
            model.Links = LS.FindByEventIDPaged(id, currentPage, 10, this.FromDateTime(), this.ToDateTime());
            model.CurrentPageIndex = currentPage;
            model.TotalRecords = LS.FindCountByEventID(id, this.FromDateTime(), this.ToDateTime());

            return View(model);
        }

        public ActionResult Edit(int id) {
            Event currentEvent = ES.FindByID(id);
            CreateEventViewModel model = Mapper.Map<Event, CreateEventViewModel>(currentEvent);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection fc, CreateEventViewModel model) {
            if (ModelState.IsValid) {
                Event currentEvent = ES.FindByID(model.ID);
                try {
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

                    DateTime.TryParse(Request.Form[7], out startDate); // start date
                    DateTime.TryParse(Request.Form[8], out endDate); // end date (could be null)
                    DateTime.TryParse(Request.Form[10], out collectionStart); 
                    DateTime.TryParse(Request.Form[11], out collectionEnd);

                    currentEvent.StartDateTime = startDate.FromUserTimeZoneToUtc();
                    if (endDate != DateTime.MinValue) {
                        currentEvent.EndDateTime = endDate.FromUserTimeZoneToUtc(); // 7 is timezone offset
                    }

                    currentEvent.CollectionStartDateTime = collectionStart.FromUserTimeZoneToUtc();
                    if (collectionEnd != DateTime.MinValue) {
                        currentEvent.CollectionEndDateTime = collectionEnd.FromUserTimeZoneToUtc();
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

        public ActionResult VenueSearch() {
            return View(new VenueSearchModel());
        }

        [HttpPost]
        public ActionResult VenueSearch(FormCollection fc) {
            string url = string.Format("http://maps.google.com/maps/geo?output=csv&q={0}", "toronto");
            string results = GetResults(url, null, "Get");
            var parts = results.Split(',');
            
            Double longitude = Convert.ToDouble(parts[2]);
            Double latitude = Convert.ToDouble(parts[3]);

            string clientId = "GRBSH3HPYZYHIACLAL1GHGYHVHVWLJ0GGUUB1OLV41GV5EF1";
            string clientSecret = "FFCUYMPWPVTCP5AVNDS2VCA1JPTTR4FKCE35ZQUV3TKON5MH";
            string latLong = string.Format("{0},{1}",longitude, latitude);
            string version = DateTime.Today.ToString("yyyyMMdd");
            string query = "";

            string searchRequest = string.Format("https://api.foursquare.com/v2/venues/search?ll={0}&query={1}&client_id={2}&client_secret={3}&v={4}",
                latLong,
                query,
                clientId,
                clientSecret,
                version);

            var client = new FoursquareVenueClient();
            var venues = client.Execute(searchRequest);
            List<FoursquareVenue> foundVenues = new List<FoursquareVenue>();
            foreach (var item in venues.response) {
                foundVenues.Add(new FoursquareVenue { Name = item.name });
            }

            VenueSearchModel model = new VenueSearchModel();
            model.Venues = foundVenues;

            return View(model);
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
    }

}
