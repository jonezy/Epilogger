using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Core.Filters;
using Epilogger.Web.Core.Stats;
using Epilogger.Web.Models;
using Epilogger.Common;

using RichmondDay.Helpers;
using System.Net;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using Twitterizer;

namespace Epilogger.Web.Controllers {
    public partial class EventsController : BaseController
    {
        private const string ClientId = "GRBSH3HPYZYHIACLAL1GHGYHVHVWLJ0GGUUB1OLV41GV5EF1";
        private const string ClientSecret = "FFCUYMPWPVTCP5AVNDS2VCA1JPTTR4FKCE35ZQUV3TKON5MH";
        readonly string _version = DateTime.Today.ToString("yyyyMMdd");

        EpiloggerDB _db;
        EventService _es = new EventService();
        TweetService _ts = new TweetService();
        ImageService _is = new ImageService();
        CheckInService _cs = new CheckInService();
        ExternalLinkService _ls = new ExternalLinkService();
        BlogService _bs = new BlogService();
        CategoryService _catS = new CategoryService();
        UserService _us = new UserService();
        VenueService _venueService = new VenueService();
        UserTwitterActionService _userTwitterActionService = new UserTwitterActionService();

        DateTime _fromDateTime = DateTime.Parse("2000-01-01 00:00:00");
        private DateTime FromDateTime() {
            try {
                if (Request.QueryString["f"] != null) {
                    //_FromDateTime = DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["f"])).FromUserTimeZoneToUtc();
                    _fromDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["f"])));
                }
                return _fromDateTime;
            } catch (Exception) {
                return _fromDateTime;
            }
        }

        DateTime _toDateTime = DateTime.Parse("2200-12-31 00:00:00");
        private DateTime ToDateTime() {
            try {
                if (Request.QueryString["t"] != null) {
                    //_ToDateTime = DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["t"])).FromUserTimeZoneToUtc();
                    _toDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["t"])));
                }
                return _toDateTime;
            } catch (Exception) {
                return _toDateTime;
            }

        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (_db == null) _db = new EpiloggerDB();
            if (_es == null) _es = new EventService();
            if (_ts == null) _ts = new TweetService();
            if (_is == null) _is = new ImageService();
            if (_cs == null) _cs = new CheckInService();
            if (_ls == null) _ls = new ExternalLinkService();
            if (_bs == null) _bs = new BlogService();
            if (_catS == null) _catS = new CategoryService();
            if (_us == null) _us = new UserService();
            if (_venueService == null) _venueService = new VenueService();
            if (_userTwitterActionService == null) _userTwitterActionService = new UserTwitterActionService();
            base.Initialize(requestContext);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        
        [CompressFilter]
        public virtual ActionResult Category(string categoryName)
        {
            var model = new BrowseCategoriesDisplayViewModel();
            var events = new List<Event>();
            
            //TODO - Implement paging!
            if (categoryName != string.Empty)
            {
                events = categoryName.ToLower() == "all" ? _es.Get50Events() : _es.GetEventsByCategorySlug(categoryName);
            }

            model.Events = Mapper.Map<List<Event>, List<DashboardEventViewModel>>(events).OrderByDescending(f => f.StartDateTime);
            model.EventCategories = _catS.AllCategories();
            model.CategoryName = _catS.GetCategoryBySlug(categoryName).CategoryName;


            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        //public ActionResult Details(int id) {
        [CompressFilter]
        public virtual ActionResult Details(string id)
        {

            var requestedEvent = _es.FindBySlug(id);

            if (requestedEvent != null)
            {
                var model = Mapper.Map<Event, EventDisplayViewModel>(requestedEvent);
                model.TweetCount = _ts.FindTweetCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.Tweets = _ts.FindByEventIDOrderDescTake6(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.ImageCount = _is.FindImageCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.Images = _is.FindByEventIDOrderDescTake9(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.CheckInCount = _cs.FindCheckInCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.CheckIns = _cs.FindByEventIDOrderDescTake16(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.ExternalLinks = _ls.FindByEventIDOrderDescTake3(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.BlogPosts = _bs.FindByEventIDTake5(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.EventRatings = _es.FindEventRatingsByID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.HasUserRated = false;
                model.CurrentUserID = CurrentUserID;
                model.CurrentUserRole = CurrentUserRole;
                model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);
                model.TheUser = CurrentUser;
                if (requestedEvent.UserID != null)
                    model.CreatedEventUser = _us.GetUserByID((Guid) requestedEvent.UserID);

                model.CanDelete = false;
                if ((requestedEvent.UserID == CurrentUserID) || CurrentUserRole == UserRoleType.Administrator)
                {
                    model.CanDelete = true;
                }

                //@(((Model.EventRatings.Sum(i => i.UserRating) / Model.EventRatings.Count()) / 5) * 100)


                if (Request.QueryString["f"] != null) {
                    model.FromDateTime = this.FromDateTime();
                } else {
                    model.FromDateTime = null;
                }
                if (Request.QueryString["t"] != null) {
                    model.ToDateTime = this.ToDateTime();
                } else {
                    model.ToDateTime = null;
                }


                //If there is a user logged in
                if (CurrentUserID != Guid.Empty) {
                    model.HasSubscribed = CurrentUser.UserFollowsEvents.FirstOrDefault(ufe => ufe.EventID == requestedEvent.ID) != null ? true : false;
                    if (model.EventRatings.Any(i => i.UserID == CurrentUserID)) {
                        model.HasUserRated = true;
                    } else {
                        model.HasUserRated = false;
                    }
                }

                return View(model);
            }
            ModelState.AddModelError(string.Empty, "The event you're trying to visit doesn't exist.");
            return RedirectToAction("index", "browse");

        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        
        private EventToolbarViewModel BuildToolbarViewModel(Event requestedEvent) {
            var toolbarModel = new EventToolbarViewModel
                                   {
                                       EventID = requestedEvent.ID,
                                       EventSlug = requestedEvent.EventSlug,
                                       CreatedByID = requestedEvent.UserID.Value,
                                       CurrentUserID = CurrentUserID,
                                       CurrentUserRole = CurrentUserRole
                                   };
            if (CurrentUserID != Guid.Empty)
            {
                toolbarModel.HasSubscribed = CurrentUser.UserFollowsEvents.FirstOrDefault(ufe => ufe.EventID == requestedEvent.ID) != null ? true : false;
            }

            return toolbarModel;
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public virtual ActionResult Details(string id, FormCollection collection)
        {


            if (collection["ResetDates"] == "1")
            {
                return RedirectToAction("Details", new { id = id });
            }        
            else 
            {
                var fromDateTime = DateTime.Parse(collection["FromDateTime"]);
                var toDateTime = DateTime.Parse(collection["ToDateTime"]);

                string encodedFrom = Helpers.base64Encode(String.Format("{0:yyyy-MM-dd HH:mm:ss}", fromDateTime));
                string encodedTo = Helpers.base64Encode(String.Format("{0:yyyy-MM-dd HH:mm:ss}", toDateTime));


                return RedirectToAction("Details", new { id = id, f = encodedFrom, t = encodedTo });
            }
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        
        [CompressFilter]
        public virtual ActionResult AllPhotos(string id, int? page)
        {
            int currentPage = page.HasValue ? page.Value - 1 : 0;
            var requestedEvent = _es.FindBySlug(id);

            if (requestedEvent != null)
	        {
                var model = Mapper.Map<Event, AllPhotosDisplayViewModel>(requestedEvent);
                model.PhotoCount = _is.FindImageCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.CurrentPageIndex = currentPage;
                model.ShowTopPhotos = false;
                model.Images = _is.GetPagedPhotos(requestedEvent.ID, currentPage + 1, 30, this.FromDateTime(), this.ToDateTime());
                model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

                model.CanDelete = false;
                if ((requestedEvent.UserID == CurrentUserID) || CurrentUserRole == UserRoleType.Administrator)
                {
                    model.CanDelete = true;
                }

                if (currentPage + 1 == 1) {
                    model.ShowTopPhotos = true;
                    model.TopImages = _is.GetTopPhotosByEventID(requestedEvent.ID, 10, this.FromDateTime(), this.ToDateTime());
                }

                return View(model);
            }
            ModelState.AddModelError(string.Empty, "The event you're trying to visit doesn't exist.");
            return RedirectToAction("index", "Browse");
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        [CompressFilter]
        public virtual ActionResult AllTweets(string id, int? page)
        {
            
            int currentPage = page.HasValue ? page.Value - 1 : 0;

            var requestedEvent = _es.FindBySlug(id);
            if (requestedEvent != null)
            {
                var topTweetersStats = new TopTweetersStats();
                var model = Mapper.Map<Event, AllTweetsDisplayViewModel>(requestedEvent);

                model.TweetCount = _ts.FindTweetCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.UniqueTweeterCount = _ts.FindUniqueTweetCountByEventID(requestedEvent.ID, this.FromDateTime(),
                                                                             this.ToDateTime());

                model.CurrentPageIndex = currentPage;
                model.TopTweeters =
                    topTweetersStats.Calculate(_ts.GetTop10TweetersByEventID(requestedEvent.ID, this.FromDateTime(),
                                                                             this.ToDateTime())).ToList();
                model.ShowTopTweets = false;
                //model.Tweets =
                //    Mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetDisplayViewModel>>(
                //        _ts.GetPagedTweets(requestedEvent.ID, currentPage + 1, 100, this.FromDateTime(),
                //                           this.ToDateTime()));
                
                model.Tweets = _ts.GetPagedTweets(requestedEvent.ID, currentPage + 1, 100, FromDateTime(), ToDateTime());
                model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

                model.CanDelete = false;
                if ((requestedEvent.UserID == CurrentUserID) || CurrentUserRole == UserRoleType.Administrator)
                {
                    model.CanDelete = true;
                }

                //model.CanDelete = 

                if (currentPage + 1 == 1)
                {
                    model.ShowTopTweets = true;
                    //model.Tweets =
                    //    Mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetDisplayViewModel>>(
                    //        _ts.GetPagedTweets(requestedEvent.ID, currentPage + 1, 100, this.FromDateTime(),
                    //                           this.ToDateTime()));
                    model.Tweets = _ts.GetPagedTweets(requestedEvent.ID, currentPage + 1, 100, FromDateTime(), ToDateTime());
                }

                return View(model);
            }

            ModelState.AddModelError(string.Empty, "The event you're trying to visit doesn't exist.");
            return RedirectToAction("index", "Browse");
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        [RequiresAuthentication(ValidUserRole = UserRoleType.RegularUser, AccessDeniedMessage = "You must be logged in to your epilogger account to create an event")]
        public virtual ActionResult Create()
        {
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
        [RequiresAuthentication(ValidUserRole = UserRoleType.RegularUser, AccessDeniedMessage = "You must be logged in to your epilogger account to create an event")]
        [HttpPost]
        public virtual ActionResult Create(CreateEventViewModel model)
        {

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
                var venue = new Venue
                                {
                                    FoursquareVenueID = foursquareVenue.response.id,
                                    Address = locationNode.address,
                                    Name = foursquareVenue.response.name,
                                    City = locationNode.city,
                                    State = locationNode.state,
                                    Zip = locationNode.postalCode,
                                    CrossStreet = locationNode.crossStreet,
                                    Geolat = locationNode.lat,
                                    Geolong = locationNode.lng
                                };

                // save the venue
                _venueService.Save(venue);
                model.VenueID = venue.ID;
                model.Venue = venue;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.UserID = CurrentUserID;
                    model.CreatedDateTime = DateTime.UtcNow;
                                        
                    var epLevent = Mapper.Map<CreateEventViewModel, Event>(model);

                    _es.Save(epLevent);

                    //Initiate a first collect on the event
                    var tsmp = new MQ.MSGProducer("Epilogger", "TwitterSearch");
                    var tsMSG = new MQ.Messages.TwitterSearchMSG
                    {
                        EventID = model.ID,
                        SearchTerms = model.SearchTerms,
                        SearchFromLatestTweet = false,
                        SearchSince = null,
                        SearchUntil = null
                    };
                    tsmp.SendMessage(tsMSG);
                    tsmp.Dispose();



                    //The event has been created and there is no error. 
                    //Let's tweet out that mother.
                    try
                    {
                        var apiClient = new Epilogr.APISoapClient();
                        var shortUrlContainer = apiClient.CreateUrl("http://epilogger.com/events/" + epLevent.EventSlug);

                        
                        var theTerm = string.Empty;
                        if (epLevent.SearchTerms.Contains("OR"))
                        {
                            var terms = Regex.Split(epLevent.SearchTerms, "OR");
                            if (!terms[0].Contains("#"))
                            {
                                theTerm = "#" + terms[0];
                            }
                            else
                            {
                                theTerm = terms[0];
                            }

                        }
                        else
                        {
                            if (!epLevent.SearchTerms.Contains("#"))
                            {
                                theTerm = "#" + epLevent.SearchTerms;
                            }
                        }


                        var beAPartString = string.Empty;
                        var compareToStart = DateTime.Compare(DateTime.UtcNow, epLevent.StartDateTime);
                        if (epLevent.EndDateTime == null)
                        {
                            beAPartString = "See what's going on";
                        }
                        else
                        {
                            var compareToEnd = DateTime.Compare(DateTime.UtcNow, epLevent.EndDateTime ?? DateTime.MinValue);

                            if (compareToStart < 0)
                            {
                                beAPartString = "Be a part of the lead up";
                            }
                            else if (compareToStart > 0 && compareToEnd < 0)
                            {
                                beAPartString = "See what's going on";
                            }
                            else if (compareToEnd > 0)
                            {
                                beAPartString = "See what happened";
                            }
                        }
                        
                        var eventCreatedTweet =
                            string.Format(
                                "{0} has been added to Epilogger! {1} at {2} {3}", epLevent.Name, beAPartString, shortUrlContainer.ShortenedUrl, theTerm);

                        var mp = new MQ.MSGProducer("Epilogger", "TweetSender");
                        var myMSG = new MQ.Messages.TweetSenderMsg { TweetText = eventCreatedTweet };
                        mp.SendMessage(myMSG);
                        mp.Dispose();
                    }
                    catch (Exception)
                    {
                    }
                    
                    this.StoreSuccess("Your Event was created successfully!  Dont forget to share it with your friends and attendees!");

                    return RedirectToAction("details", new { id = epLevent.EventSlug });
                }
                catch (Exception ex)
                {
                    this.StoreError(string.Format("There was an error: {0}", ex.Message));
                    var epLevent = Mapper.Map<CreateEventViewModel, Event>(model);
                    model = Mapper.Map<Event, CreateEventViewModel>(epLevent);
                    return View(model);
                }
            }

            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult EventBySlug(string eventSlug)
        {
            Event foundEvent = null;
            foreach (var e in _db.Events) {
                if (e.Name.CreateUrlSlug() == eventSlug) {
                    foundEvent = e;
                    break;
                }
            }

            return View("Details", Mapper.Map<Event, EventDisplayViewModel>(foundEvent));
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult GetImageComments(int eventId, int imageid)
        {
            return PartialView("_ImageComments", _ts.FindByImageID(imageid, eventId));
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult GetImageCommentsPage1(ImageCommentViewModel model)
        {
            return PartialView("ImageCommentsPaged", model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult GetImageCommentsPaged(ImageCommentViewModel model)
        {
            
            model.Tweets = _ts.FindByImageIDPaged(model.ImageId, model.EventId, model.page, 4);
            model.ModifyDisplayClass = "";

            return PartialView("ImageCommentsPaged", model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult ImageCommentControl(int eventId, int imageId, int? page, string commentLocation)
        {

            var model = new ImageCommentViewModel()
            {
                CanDelete = false,
                ModifyDisplayClass = "pp_FirstComment",
                StyleFirstTweet = true,
                ShowControls = false,
                Tweets = _ts.FindByImageIDPaged(imageId, eventId, 1, 4),
                EventId = eventId,
                ImageId = imageId,
                page = page ?? 1,
                TotalTweetCount = _ts.FindCountByImageID(imageId, eventId)
            };

            return PartialView("ImageCommentControl", model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public virtual ActionResult GetLastTweetsJSON(int Count, string pageLoadTime, int EventID)
        {
            Dictionary<String, Object> dict = new Dictionary<String, Object>();

            if (pageLoadTime.Length > 0) {
                if (pageLoadTime != "undefined") {

                    pageLoadTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Parse(pageLoadTime));

                    EpiloggerDB db = _ts.Thedb();
                    IEnumerable<Tweet> theTweets = _ts.Thedb().Tweets.Where(t => t.EventID == EventID & t.CreatedDate > DateTime.Parse(pageLoadTime)).OrderByDescending(t => t.CreatedDate).Take(Count);

                    var htmlString = new StringBuilder();
                    var lasttweettime = string.Empty;
                    var TheFirst = true;
                    var RecordCount = 0;

                    foreach (var theT in theTweets) {
                        if (TheFirst) {
                            lasttweettime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", theT.CreatedDate);
                            TheFirst = false;
                        }


                        //Instead of hard coding the HTML for the tweets, let's use the template.
                        var firstOrDefault = theT.Events.FirstOrDefault(t => t.ID == EventID);
                        var canDelete = firstOrDefault != null && ((firstOrDefault.UserID == CurrentUserID) || CurrentUserRole == UserRoleType.Administrator);

                        htmlString.Append(RenderRazorViewToString("TweetTemplate", new TweetTemplateViewModel() { CanDelete = canDelete, Tweet = theT, ShowControls = true, EventId = EventID }));

                        RecordCount++;
                    }


                    //Return the Dictionary as it's IEnumerable and it creates the correct JSON doc.
                    dict = new Dictionary<String, Object>();

                    dict.Add("numberofnewtweets", RecordCount);
                    dict.Add("lasttweettime", lasttweettime);
                    dict.Add("tweetcount", string.Format("{0:#,###}", db.Tweets.Count(t => t.EventID == EventID)));
                    dict.Add("html", htmlString.ToString());
                }
            }

            return Json(dict);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public virtual ActionResult GetLastPhotosJSON(int Count, string pageLoadTime, int EventID)
        {
            var dict = new Dictionary<String, Object>();

            if (pageLoadTime.Length > 0) {
                if (pageLoadTime != "undefined") {
                    pageLoadTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Parse(pageLoadTime));

                    var db = _ts.Thedb();
                    var theImages = db.Images.Where(t => t.EventID == EventID && t.DateTime > DateTime.Parse(pageLoadTime).AddSeconds(1)).OrderByDescending(t => t.DateTime).Take(Count);

                    var html = new StringBuilder();
                    var lastphototime = string.Empty;
                    var theFirst = true;
                    var recordCount = 0;

                    foreach (var theI in theImages) {
                        if (theFirst) {
                            lastphototime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", theI.DateTime);
                            theFirst = false;
                        }

                        //Instead of hard coding the HTML for the images, let's use the template.
                        var firstOrDefault = theI.Events.FirstOrDefault(t => t.ID == EventID);
                        var canDelete = firstOrDefault != null && ((firstOrDefault.UserID == CurrentUserID) || CurrentUserRole == UserRoleType.Administrator);

                        html.Append(RenderRazorViewToString("_ImageTemplate", new ImageTemplateViewModel { CanDelete = canDelete, Image = theI }));
                        
                        recordCount++;
                    }

                    //Return the Dictionary as it's IEnumerable and it creates the correct JSON doc.
                    dict = new Dictionary<String, Object>
                               {
                                   {"numberofnewphotos", recordCount},
                                   {"lastphototime", lastphototime},
                                   {"photocount", string.Format("{0:#,###}", db.Images.Count(t => t.EventID == EventID))},
                                   {"html", html.ToString()}
                               };
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
        public virtual ActionResult Subscribe(FormCollection fc)
        {

            Event requestedEvent = _es.FindBySlug(fc["EventSlug"].ToString());

            if (requestedEvent != null)
            {
                if (CurrentUserID == Guid.Empty) {
                    this.StoreWarning("You must be logged in to your epilogger account to subscribe to an event");
                    return RedirectToAction("details", new { id = requestedEvent.EventSlug });
                }

                UserService service = new UserService();
                UserFollowsEvent followsEvent = new UserFollowsEvent();
                followsEvent.EventID = requestedEvent.ID;
                followsEvent.UserID = CurrentUserID;
                followsEvent.Timestamp = DateTime.Now;

                service.SaveUserFollowsEvent(followsEvent);

                this.StoreSuccess("You are now subscribed to this event!");
            }

            return RedirectToAction("details", new { id = requestedEvent.EventSlug });
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public virtual ActionResult UnSubscribe(FormCollection fc)
        {

            Event requestedEvent = _es.FindBySlug(fc["EventSlug"].ToString());

            if (requestedEvent != null) 
            {
                if (CurrentUserID == Guid.Empty) {
                    this.StoreWarning("You must be logged in to your epilogger account to subscribe to an event");
                    return RedirectToAction("details", new { id = requestedEvent.EventSlug });
                }

                UserService service = new UserService();
                UserFollowsEvent followsEvent = new UserFollowsEvent();
                followsEvent.EventID = requestedEvent.ID;
                followsEvent.UserID = CurrentUserID;

                service.DeleteEventSubscription(CurrentUserID, requestedEvent.ID);

                this.StoreSuccess("You are no longer subscribed to this event!");
            }

            return RedirectToAction("details", new { id = requestedEvent.EventSlug });
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        //Depricated
        //See StarRating
        [HttpPost]
        public virtual ActionResult eventRating(FormCollection fc)
        {
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
        [CompressFilter]
        public virtual ActionResult Search(String id, string IEsearchterm)
        {
            var requestedEvent = _es.FindBySlug(id);

            if (requestedEvent != null)
	        {
                var s = new SearchInEventViewModel {ID = requestedEvent.ID, SearchTerm = IEsearchterm};
                var thisEvent = _es.FindByID(requestedEvent.ID);
                s.Name = thisEvent.Name;
                s.Eventslug = thisEvent.EventSlug;
            
                if (!string.IsNullOrEmpty(IEsearchterm)) {
                    s.SearchResults = _es.SearchInEvent(requestedEvent.ID, IEsearchterm, FromDateTime(), ToDateTime());
                }
                s.ToolbarViewModel = BuildToolbarViewModel(thisEvent);

                return View(s);
            }
            ModelState.AddModelError(string.Empty, "The event you're trying to visit doesn't exist.");
            return RedirectToAction("index", "Browse");
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public virtual ActionResult Search(FormCollection fc)
        {
            var searchTerm = fc["SearchTerm"];

            return RedirectToAction("Search", new { id = fc["EventSlug"], IEsearchterm = searchTerm });
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        [CompressFilter]
        public virtual ActionResult AllContent(int id)
        {
            AllContentViewModel model = Mapper.Map<Event, AllContentViewModel>(_es.FindByID(id));
            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        [CompressFilter]
        public virtual ActionResult AllBlogPosts(string id, int? page)
        {
            int currentPage = page.HasValue ? page.Value - 1 : 0;
            Event requestedEvent = _es.FindBySlug(id);

            if (requestedEvent != null)
	        {
                var blogPosts = Mapper.Map<List<BlogPost>, List<BlogPostDisplayViewModel>>(_bs.FindByEventID(requestedEvent.ID).ToList());

                var model = Mapper.Map<Event, AllBlogPostsViewModel>(requestedEvent);
                model.SetAllBlogPostsViewModel(blogPosts, currentPage, blogPosts.Count());
                model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

                return View(model);
            }
            ModelState.AddModelError(string.Empty, "The event you're trying to visit doesn't exist.");
            return RedirectToAction("index", "Browse");
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        [CompressFilter]
        public virtual ActionResult AllCheckins(string id, int? page)
        {
            int currentPage = page.HasValue ? page.Value - 1 : 0;

            var currentEvent = _es.FindBySlug(id);
            if (currentEvent != null)
            {
                var checkins = Mapper.Map<List<CheckIn>, List<CheckinDisplayViewModel>>(_cs.FindByEventIDPaged(currentEvent.ID, currentPage, 10, this.FromDateTime(), this.ToDateTime()).ToList());

                var model = new AllCheckinsViewModel(checkins, currentPage, 10);
                model.ID = currentEvent.ID.ToString();
                model.Name = currentEvent.Name;
                model.EventSlug = currentEvent.EventSlug;
                model.TotalRecords = _cs.FindCheckInCountByEventID(currentEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.ToolbarViewModel = BuildToolbarViewModel(currentEvent);

                return View(model);

            }
	        ModelState.AddModelError(string.Empty, "The event you're trying to visit doesn't exist.");
	        return RedirectToAction("index", "Browse");

        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        [CompressFilter]
        public virtual ActionResult AllLinks(string id, int? page)
        {

            int currentPage = page.HasValue ? page.Value - 1 : 0;
            var requestedEvent = _es.FindBySlug(id);
            if (requestedEvent != null)
	        {
                var model = Mapper.Map<Event, AllLinksViewModel>(requestedEvent);
                model.Links = _ls.FindByEventIDPaged(requestedEvent.ID, currentPage, 10, this.FromDateTime(), this.ToDateTime());
                model.CurrentPageIndex = currentPage;
                model.TotalRecords = _ls.FindCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
                model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

                return View(model);
            }
            ModelState.AddModelError(string.Empty, "The event you're trying to visit doesn't exist.");
            return RedirectToAction("index", "Browse");
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        [CompressFilter]
        public virtual ActionResult AllStats(String id)
        {

            //int currentPage = page.HasValue ? page.Value - 1 : 0;
            var requestedEvent = _es.FindBySlug(id);
            if (requestedEvent != null)
            {

                var model = Mapper.Map<Event, AllStatsViewModel>(requestedEvent);

                if (Request.QueryString["f"] != null)
                {
                    model.FromDateTime = FromDateTime();
                }
                else
                {
                    model.FromDateTime = null;
                }
                if (Request.QueryString["t"] != null)
                {
                    model.ToDateTime = ToDateTime();
                }
                else
                {
                    model.ToDateTime = null;
                }
                model.MyUTCNow = DateTime.UtcNow;

                model.TweetCount = _ts.FindTweetCountByEventID(requestedEvent.ID, FromDateTime(), ToDateTime());
                model.ImageCount = _is.FindImageCountByEventID(requestedEvent.ID, FromDateTime(), ToDateTime());
                model.ExternalLinkCount = _ls.FindCountByEventID(requestedEvent.ID, FromDateTime(), ToDateTime());
                model.TopImages = model.TopImages = _is.GetTopPhotosAndTweetByEventID(requestedEvent.ID, 10, FromDateTime(), ToDateTime());
                model.TopLinks = _ls.GetTopURLsByEventID(requestedEvent.ID, 5, FromDateTime(), ToDateTime());

                var checkins = Mapper.Map<List<CheckIn>, List<CheckinDisplayViewModel>>(_cs.FindByEventID(requestedEvent.ID, FromDateTime(), ToDateTime()).ToList());
                model.AllCheckIns = checkins;

                var topTweetersStats = new TopTweetersStats();
                model.TopTweeters = model.TopTweeters = topTweetersStats.Calculate(_ts.GetTop10TweetersByEventID(requestedEvent.ID, FromDateTime(), ToDateTime())).ToList();

                model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

                //model.Links = LS.FindByEventIDPaged(id, currentPage, 10, this.FromDateTime(), this.ToDateTime());
                //model.CurrentPageIndex = currentPage;
                //model.TotalRecords = LS.FindCountByEventID(id, this.FromDateTime(), this.ToDateTime());


                return View(model);
            }
	        ModelState.AddModelError(string.Empty, "The event you're trying to visit doesn't exist.");
	        return RedirectToAction("index", "Browse");

        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        [CompressFilter]
        [RequiresAuthentication(ValidUserRole = UserRoleType.RegularUser, AccessDeniedMessage = "You must be logged in to your epilogger account to edit an event")]
        public virtual ActionResult Edit(string id)
        {
            Event currentEvent = _es.FindBySlug(id);
            CreateEventViewModel model = Mapper.Map<Event, CreateEventViewModel>(currentEvent);
            model.ToolbarViewModel = BuildToolbarViewModel(currentEvent);

            model.CurrentUserRole = CurrentUserRole;
            model.CurrentUserID = CurrentUser.ID;
            model.UserID = CurrentUser.ID;
            model.EventSlug = currentEvent.EventSlug;

            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        [RequiresAuthentication(ValidUserRole = UserRoleType.RegularUser, AccessDeniedMessage = "You must be logged in to your epilogger account to edit an event")]
        [HttpPost]
        public virtual ActionResult Edit(FormCollection fc, CreateEventViewModel model)
        {
            Event currentEvent = _es.FindBySlug(model.EventSlug);
            model.ID = currentEvent.ID;

            //if (ModelState.IsValid) {
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

                    DateTime.TryParse(Request.Form["start_date"] + " " + Request.Form["start_time"], out startDate); // start date
                    DateTime.TryParse(Request.Form["end_date"] + " " + Request.Form["end_time"], out endDate); // end date (could be null)
                    DateTime.TryParse(Request.Form["collection_start_date"] + " " + Request.Form["collection_start_time"], out collectionStart);
                    DateTime.TryParse(Request.Form["collection_end_date"] + " " + Request.Form["collection_end_time"], out collectionEnd);

                    //Adjust the timezone. this is becuase the EditTemplate is not returning the Time.
                    model.StartDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(startDate);
                    model.EndDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(endDate);
                    model.CollectionStartDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(collectionStart);
                    model.CollectionEndDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(collectionEnd);

                    currentEvent.StartDateTime = model.StartDateTime;
                    currentEvent.EndDateTime = model.EndDateTime;
                    currentEvent.CollectionStartDateTime = model.CollectionStartDateTime;
                    currentEvent.CollectionEndDateTime = model.CollectionEndDateTime;

                    ////Adjust the timezone. this is becuase the EditTemplate is not returning the Time.
                    //currentEvent.StartDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(startDate);
                    //if (endDate != DateTime.MinValue)
                    //{
                    //    currentEvent.EndDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(endDate);
                    //}

                    //currentEvent.CollectionStartDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(collectionStart); 
                    //if (collectionEnd != DateTime.MinValue) {
                    //    currentEvent.CollectionEndDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(collectionEnd);
                    //}

                    //currentEvent.VenueID = model.VenueID;

                    if (!string.IsNullOrEmpty(model.FoursquareVenueID))
                    {
                        if (!_venueService.DoesVenueExist(model.FoursquareVenueID))
                        {
                            // have to look up the foursquare venue and then create it and save it to the db.
                            dynamic foursquareVenue = LookupFoursquareVenue(model.FoursquareVenueID);
                            var locationNode = foursquareVenue.response.location;

                            // convert it to a Venue
                            var venue = new Venue
                                            {
                                                FoursquareVenueID = foursquareVenue.response.id,
                                                Address = locationNode.address,
                                                Name = foursquareVenue.response.name,
                                                City = locationNode.city,
                                                State = locationNode.state,
                                                CrossStreet = locationNode.crossStreet,
                                                Geolat = locationNode.lat,
                                                Geolong = locationNode.lng
                                            };

                            // save the venue
                            _venueService.Save(venue);
                            currentEvent.VenueID = venue.ID;
                        }
                        else
                        {
                            //Need to look up the VenueID of the new FourSquareID
                            var lookupVenue = _venueService.FindByFourSquareVenueID(model.FoursquareVenueID);
                            if (lookupVenue != null)
                            {
                                currentEvent.VenueID = lookupVenue.ID;
                            }
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

                    
                    _es.Save(currentEvent);
                    this.StoreSuccess("Your event was updated successfully!  Make sure you let all your friends know about the changes you just made!");
                    model = Mapper.Map<Event, CreateEventViewModel>(currentEvent);
                    model.EventSlug = currentEvent.EventSlug;
                } catch (Exception ex) {
                    this.StoreError(string.Format("There was an error: {0}", ex.Message));
                    model = Mapper.Map<Event, CreateEventViewModel>(currentEvent);
                    model.EventSlug = currentEvent.EventSlug;
                    return View(model);
                }
            //}
            
            return RedirectToAction("edit", new { id = model.EventSlug});
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult VenueSearch()
        {

            //TODO replace with IP geo coded data.
            var vsm = new VenueSearchModel { City = "Toronto", ProvinceState = "ON" };
            return PartialView(vsm);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public virtual PartialViewResult SearchVenues(FormCollection fc)
        {
            var location = new StringBuilder();
            if (fc["address"] != null) {
                location.AppendFormat("{0},",fc["address"]);
            }
            if (fc["state"] != null) {
                location.AppendFormat("{0},",fc["state"]);
            }
            if (fc["city"] != null) {
                location.AppendFormat("{0},",fc["city"]);
            }
            if (fc["zip"] != null) {
                location.AppendFormat("{0},", fc["zip"]);
            }
            
            string url = string.Format("http://maps.google.com/maps/geo?output=csv&q={0}", Url.Encode(location.ToString().TrimEnd(',')));
            string results = GetResults(url, null, "Get");
            var parts = results.Split(',');

            var longitude = Convert.ToDouble(parts[2]);
            var latitude = Convert.ToDouble(parts[3]);

            var venues = FoursquareVenueSearch(fc["name"] ?? "", longitude, latitude);

            var foundVenues = new List<FoursquareVenue>();
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
                ClientId,
                ClientSecret,
                _version);

            var client = new FoursquareVenueClient();
            var venues = client.Execute(searchRequest);

            return venues;
        }

        private dynamic LookupFoursquareVenue(string venueId) {
            string searchRequest = string.Format("https://api.foursquare.com/v2/venues/{0}?client_id={1}&client_secret={2}&v={3}",
                venueId,
                ClientId,
                ClientSecret,
                _version);

            var client = new FoursquareVenueClient();
            var venue = client.Execute(searchRequest);

            return venue;
        }

        string GetResults(string url, string postData, string method) {
            var webRequest = WebRequest.Create(url);
            webRequest.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(method)) {
                webRequest.Method = method;

                if (!string.IsNullOrEmpty(postData)) {
                    // posting data to a url
                    var byteSend = Encoding.ASCII.GetBytes(postData);
                    webRequest.ContentLength = byteSend.Length;

                    using (var streamOut = webRequest.GetRequestStream())
                        streamOut.Write(byteSend, 0, byteSend.Length);
                }
            } else
                webRequest.Method = "GET";

            var webResponse = webRequest.GetResponse();
            using (var streamReader = new StreamReader(webResponse.GetResponseStream(), encoding: Encoding.UTF8))
                if (streamReader.Peek() > -1) return streamReader.ReadToEnd();

            return "";
        } 
        #endregion

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        [CompressFilter]
        public virtual ActionResult AddBlogPost(int id)
        {
            var model = new AddBlogPostViewModel {BlogURL = "http://", EventID = id};
            return PartialView(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost, ValidateInput(false)]
        public bool AddBlogPost(int id, AddBlogPostViewModel model) {
            try {
                var blogPost = Mapper.Map<AddBlogPostViewModel, BlogPost>(model);
                blogPost.EventID = id;
                blogPost.UserID = CurrentUserID;
                blogPost.DateTime = DateTime.UtcNow;
                _bs.Save(blogPost);

                return true;
            } catch (Exception) {
                return false;
            }
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult AddLink()
        {
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
            } catch (Exception) {
                return false;
            }
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult StarRatings()
        {
            return PartialView("_StarRatingTemplate");
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public virtual ActionResult StarRatings(FormCollection fc)
        {
            
            int userRating;
            int.TryParse(fc["UserRating"], out userRating);

            var requestedEvent = _es.FindBySlug(fc["EventSlug"]);
            if (requestedEvent != null)
            {
                if (CurrentUserID == Guid.Empty)
                {
                    this.StoreWarning("You must be logged in to your epilogger account to subscribe to an event");
                    return RedirectToAction("details", new { id = requestedEvent.EventSlug });
                }

                var service = new UserService();
                var ratesEvent = new UserRatesEvent
                                {
                                    EventID = requestedEvent.ID,
                                    UserID = CurrentUserID,
                                    RatingDateTime = DateTime.UtcNow,
                                    UserRating = userRating
                                };


                service.SaveUserRatesEvent(ratesEvent);
                this.StoreSuccess("Rating saved!");

            }

            return RedirectToAction("details", new { id = requestedEvent.EventSlug });
        }


//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public bool Delete(int eventID)
        {
            var mp = new MQ.MSGProducer("Epilogger", "System");
            var deleteMSG = new MQ.Messages.SystemProcessingMSG
                                {EventID = eventID, Task = MQ.Messages.SystemMessageType.Delete};
            mp.SendMessage(deleteMSG);
            mp.Dispose();

            this.StoreSuccess("Your event has been added to the Delete queue! Due to the large volume of data, your event may take a few minutes to be removed from the system.");

            return true;
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public bool DeleteTweetAjax(string eventid, long tweetID)
        {
            var requestedEvent = _es.FindBySlug(eventid);

            //This is called by Ajax to delete a tweet. Double check the user is allowed to do this.
            if ((requestedEvent.UserID == CurrentUserID) || CurrentUserRole == UserRoleType.Administrator)
            {
                _ts.MarkTweetAsDeleted(tweetID);
            }

            return true;

        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public bool DeleteImageAjax(string eventid, long imageID)
        {
            var requestedEvent = _es.FindBySlug(eventid);

            //This is called by Ajax to delete a tweet. Double check the user is allowed to do this.
            if ((requestedEvent.UserID == CurrentUserID) || CurrentUserRole == UserRoleType.Administrator)
            {
                _is.MarkImageAsDeleted(imageID);
            }

            return true;

        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------


        public virtual ActionResult UploadPhotos(int id)
        {
            var model = new UploadPhotosViewModel {EventID = id};
            return PartialView(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult TweetTemplate(TweetTemplateViewModel model)
        {
            model.Replied = _userTwitterActionService.HasUserRepliedToTweet(CurrentUserID, model.Tweet.TwitterID);
            model.Retweeted = _userTwitterActionService.HasUserRetweetedTweet(CurrentUserID, model.Tweet.TwitterID);
            model.Favorited = _userTwitterActionService.HasUserFavoritedTweet(CurrentUserID, model.Tweet.TwitterID);

            return PartialView(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult TweetReply(int eventId, long tweetId)
        {
            
            var model = new TweetReplyViewModel
                            {
                                Tweet = _ts.FindByTwitterID(tweetId),
                                Event = _es.FindByID(eventId),
                                IsTwitterAuthed = CurrentUserTwitterAuthorization != null
                            };

            var apiClient = new Epilogr.APISoapClient();
            model.ShortEventURL = apiClient.CreateUrl("http://epilogger.com/events/" + model.Event.EventSlug).ShortenedUrl;

            return PartialView(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost] //Called by Ajax
        public bool TweetReply(FormCollection c)
        {
            try
            {
                var tokens = new OAuthTokens
                {
                    ConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"],
                    AccessToken = CurrentUserTwitterAuthorization.Token,
                    AccessTokenSecret = CurrentUserTwitterAuthorization.TokenSecret
                };

                var ts = TwitterStatus.Update(tokens, c["ReplyNewTweet"], new StatusUpdateOptions { InReplyToStatusId = decimal.Parse(c["TwitterID"]) });
                
                //Record the Reply
                var uta = new UserTwitterAction
                              {
                                  TweetId = (long) ts.ResponseObject.Id,
                                  TwitterAction = "Reply",
                                  UserId = CurrentUser.ID,
                                  DateTime = DateTime.UtcNow
                              };
                _userTwitterActionService.Save(uta);

                return ts.Result == RequestResult.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult TweetRetweet(int eventId, long tweetId)
        {
            var model = new TweetReplyViewModel
                            {
                Tweet = _ts.FindByTwitterID(tweetId),
                Event = _es.FindByID(eventId),
                IsTwitterAuthed = CurrentUserTwitterAuthorization != null
            };

            var apiClient = new Epilogr.APISoapClient();
            model.ShortEventURL = apiClient.CreateUrl("http://epilogger.com/events/" + model.Event.EventSlug).ShortenedUrl;

            return PartialView(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost] //Called by Ajax
        public bool TweetRetweet(FormCollection c)
        {
            try
            {
                var tokens = new OAuthTokens
                                 {
                    ConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"],
                    AccessToken = CurrentUserTwitterAuthorization.Token,
                    AccessTokenSecret = CurrentUserTwitterAuthorization.TokenSecret
                };

                var classicRT = bool.Parse(c["ClassicRT"]);

                var ts = classicRT ? TwitterStatus.Update(tokens, c["RetweetText"]) : TwitterStatus.Retweet(tokens, decimal.Parse(c["TwitterID"]));

                //Record the Retweet
                var uta = new UserTwitterAction
                {
                    TweetId = (long)ts.ResponseObject.Id,
                    TwitterAction = "Retweet",
                    UserId = CurrentUser.ID,
                    DateTime = DateTime.UtcNow
                };
                _userTwitterActionService.Save(uta);

                return ts.Result == RequestResult.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost] //Called by Ajax
        public string TweetFavorite(FormCollection c)
        {
            try
            {
                
                if (CurrentUserTwitterAuthorization == null)
                {
                    return "Auth";
                }

                var tokens = new OAuthTokens
                                 {
                    ConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"],
                    AccessToken = CurrentUserTwitterAuthorization.Token,
                    AccessTokenSecret = CurrentUserTwitterAuthorization.TokenSecret
                };

                var ts = TwitterFavorite.Create(tokens, decimal.Parse(c["TwitterID"]));

                //Record the Reply
                var uta = new UserTwitterAction
                {
                    TweetId = (long)ts.ResponseObject.Id,
                    TwitterAction = "Favorite",
                    UserId = CurrentUser.ID,
                    DateTime = DateTime.UtcNow
                };
                _userTwitterActionService.Save(uta);

                return "True";
            }
            catch (Exception)
            {
                return "False";
            }
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult NeedTwitterAuth()
        {
            return PartialView();
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult PhotoDetails(int eventid, int photoid)
        {
            var model = new PhotoDetailsViewModel();
            var theEvent = _es.FindByID(eventid);
            if (theEvent != null)
            {
                model.EventId = eventid;
                model.EventSlug = theEvent.EventSlug;
                model.Image = _is.FindByID(photoid);
                model.Tweets = _ts.FindByImageID(photoid, eventid);
                model.Event = theEvent;
                model.HashTag =
                    theEvent.SearchTerms.Split(new string[] {" OR "}, StringSplitOptions.None)[0].Contains("#")
                        ? theEvent.SearchTerms.Split(new string[] {" OR "}, StringSplitOptions.None)[0]
                        : "#" + theEvent.SearchTerms.Split(new string[] {" OR "}, StringSplitOptions.None)[0];
            }

            var apiClient = new Epilogr.APISoapClient();
            model.ShortURL = apiClient.CreateUrl("http://epilogger.com/events/PhotoDetails/" + eventid + "/" + photoid).ShortenedUrl;

            return View(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult TweetBox(int eventid, int photoid)
        {
            var imageComment = _ts.FindByImageID(photoid, eventid).FirstOrDefault();

            Debug.Assert(imageComment != null, "imageComment != null");
            var model = new TweetReplyViewModel
            {
                Tweet = _ts.FindByTwitterID(imageComment.TwitterID),
                Event = _es.FindByID(eventid),
                IsTwitterAuthed = CurrentUserTwitterAuthorization != null
            };
            
            var apiClient = new Epilogr.APISoapClient();
            model.ShortEventURL = apiClient.CreateUrl("http://epilogger.com/events/PhotoDetails/" + eventid + "/" + photoid).ShortenedUrl;

            return PartialView(model);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------


        [HttpPost] //Called by Ajax
        public bool Tweetbox(FormCollection c)
        {
            try
            {
                var tokens = new OAuthTokens
                {
                    ConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"],
                    AccessToken = CurrentUserTwitterAuthorization.Token,
                    AccessTokenSecret = CurrentUserTwitterAuthorization.TokenSecret
                };

                var ts = TwitterStatus.Update(tokens, c[0], new StatusUpdateOptions { InReplyToStatusId = decimal.Parse(c["TwitterID"]) });

                var uta = new UserTwitterAction
                {
                    TweetId = (long)ts.ResponseObject.Id,
                    TwitterAction = "Reply",
                    UserId = CurrentUser.ID
                };
                _userTwitterActionService.Save(uta);

                return ts.Result == RequestResult.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult ConceptMap(string id)
        {

            var requestedEvent = _es.FindBySlug(id);
            if (requestedEvent != null)
            {
                var model = Mapper.Map<Event, AllLinksViewModel>(requestedEvent);
                model.ToolbarViewModel = BuildToolbarViewModel(requestedEvent);

                return View(model);
            }
            ModelState.AddModelError(string.Empty, "The event you're trying to visit doesn't exist.");
            return RedirectToAction("index", "Browse");
            
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        
        public virtual ActionResult Live4X3(string id)
        {

            var requestedEvent = _es.FindBySlug(id);
            if (requestedEvent != null)
            {
                var topTweetersStats = new TopTweetersStats();
                var model = new LiveModeViewModel
                                {
                                    EventId = requestedEvent.ID,
                                    //Tweets = _ts.FindForLiveModeFirstLoad(requestedEvent.ID, 6),
                                    Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, 5, FromDateTime(), ToDateTime()).ToList(),
                                    EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime()),
                                    TopTweeters = topTweetersStats.Calculate(_ts.GetTop10TweetersByEventID(requestedEvent.ID, FromDateTime(), ToDateTime())).ToList(),
                                    Hashtag = requestedEvent.SearchTerms.Split(new string[] { " OR " }, StringSplitOptions.None)[0].Contains("#")
                                                ? requestedEvent.SearchTerms.Split(new string[] { " OR " }, StringSplitOptions.None)[0]
                                                : "#" + requestedEvent.SearchTerms.Split(new string[] { " OR " }, StringSplitOptions.None)[0]
                                };

                return View(model);
            }
            ModelState.AddModelError(string.Empty, "The event you're trying to visit doesn't exist.");
            return RedirectToAction("index", "Browse");

        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        public virtual ActionResult LiveGetLastTweetsJson(int count, string pageLoadTime, int eventID)
        {
            var dict = new Dictionary<String, Object>();

            if (pageLoadTime.Length > 0)
            {
                if (pageLoadTime != "undefined")
                {

                    pageLoadTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Parse(pageLoadTime));

                    var db = _ts.Thedb();
                    
                    //var theTweets = _ts.FindForLiveModeAjax(eventID, DateTime.Parse(pageLoadTime), count);
                    
                    var theTweets = _ts.FindForLiveModeAjaxDesc(eventID, count);
                    theTweets.Sort((x, y) => y.CreatedDate.Value.CompareTo(x.CreatedDate.Value));
                    

                    var lasttweettime = string.Empty;

                    var tweets = new List<String>();
                    var last = theTweets.Last();
                    foreach (var theT in theTweets)
                    {
                        if (theT.Equals(last))
                        {
                            lasttweettime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", theT.CreatedDate);
                        }

                        //Instead of hard coding the HTML for the tweets, let's use the template.
                        var firstOrDefault = theT.Events.FirstOrDefault(t => t.ID == eventID);
                        var canDelete = firstOrDefault != null && ((firstOrDefault.UserID == CurrentUserID) || CurrentUserRole == UserRoleType.Administrator);

                        //htmlString.Append(RenderRazorViewToString("TweetTemplate", new TweetTemplateViewModel() { CanDelete = canDelete, Tweet = theT, ShowControls = true, EventId = eventID }));
                        tweets.Add(RenderRazorViewToString("_LiveTweetTemplate", new TweetTemplateViewModel() { ModifyDisplayClass = "newupdates", CanDelete = canDelete, Tweet = theT, ShowControls = true, EventId = eventID }));

                    }


                    //Return the Dictionary as it's IEnumerable and it creates the correct JSON doc.
                    //dict = new Dictionary<String, Object>();

                    dict.Add("numberofnewtweets", tweets.Count);
                    dict.Add("lasttweettime", lasttweettime);
                    dict.Add("tweetcount", string.Format("{0:#,###}", db.Tweets.Count(t => t.EventID == eventID)));
                    dict.Add("tweetsInhtml", tweets);
                }
            }

            return Json(dict);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------
    }

}
