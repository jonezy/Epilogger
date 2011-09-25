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

namespace Epilogger.Web.Controllers {
    public class EventsController : BaseController {
        EpiloggerDB db;
        EventService ES = new EventService();
        TweetService TS = new TweetService();
        ImageService IS = new ImageService();
        CheckInService CS = new CheckInService();
        ExternalLinkService LS = new ExternalLinkService();
        BlogService BS = new BlogService();

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

            base.Initialize(requestContext);
        }

        public ActionResult Index() {
            List<Event> events = ES.AllEvents();
            List<EventDisplayViewModel> model = Mapper.Map<List<Event>, List<EventDisplayViewModel>>(events);

            return View(model);
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
                Model.FromDateTime = this.FromDateTime().ToUserTimeZone();
            } else {
                Model.FromDateTime = null;
            }
            if (Request.QueryString["t"] != null) {
                Model.ToDateTime = this.ToDateTime().ToUserTimeZone();
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
            DateTime FromDateTime;
            DateTime ToDateTime;

            FromDateTime = DateTime.Parse(collection["FromDateTime"]);
            ToDateTime = DateTime.Parse(collection["ToDateTime"]);

            string encodedFrom = Epilogger.Web.Helpers.base64Encode(String.Format("{0:yyyy-MM-dd HH:mm:ss}", FromDateTime));
            string encodedTo = Epilogger.Web.Helpers.base64Encode(String.Format("{0:yyyy-MM-dd HH:mm:ss}", ToDateTime));
            return RedirectToAction("Details", new { id = id, f = encodedFrom, t = encodedTo });
            //return Redirect("/Events/Details/" + id + "?f=" + encodedFrom + "&t=" + encodedTo);
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
                Model.Tweets = Mapper.Map<IEnumerable<Tweet>, IEnumerable<TweetDisplayViewModel>>(TS.GetPagedTweets(id, currentPage + 1, 10, this.FromDateTime(), this.ToDateTime()));
            }

            return View(Model);
        }

        [RequiresAuthentication(AccessDeniedMessage = "You must be logged in to view the details of that event")]
        public ActionResult Create() {
            CreateEventViewModel Model = new CreateEventViewModel();
            Model.TimeZoneOffset = Helpers.GetUserTimeZoneOffset();

            DateTime roundTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            if (DateTime.Now.Minute > 30) {
                roundTime.AddHours(1);
            }

            Model.StartDateTime = roundTime;
            Model.EndDateTime = roundTime.AddHours(3);
            Model.CollectionStartDateTime = roundTime.AddDays(-2);
            Model.CollectionEndDateTime = roundTime.AddDays(3);

            //Model.IsActive = true;
            //Model.WebsiteURL = "http://";
            //this.Mode = "Create";


            return View(Model);
        }

        [HttpPost]
        public ActionResult Create(CreateEventViewModel model) {
            if (ModelState.IsValid) {
                try {
                    model.UserID = Guid.Parse(CookieHelpers.GetCookieValue("lc", "uid").ToString());
                    model.CreatedDateTime = DateTime.UtcNow;

                    if (model.CollectionStartDateTime == DateTime.MinValue) {
                        model.CollectionStartDateTime = DateTime.Now.FromUserTimeZoneToUtc();
                    } else {
                        model.CollectionStartDateTime = model.CollectionStartDateTime.FromUserTimeZoneToUtc();
                    }
                    if (model.CollectionEndDateTime == DateTime.MinValue) {
                        model.CollectionEndDateTime = model.EndDateTime.AddDays(14);
                    } else {
                        model.CollectionEndDateTime = model.CollectionEndDateTime.FromUserTimeZoneToUtc();
                    }

                    model.StartDateTime = model.StartDateTime.FromUserTimeZoneToUtc();
                    model.EndDateTime = model.EndDateTime.FromUserTimeZoneToUtc();

                    Event EPLevent = Mapper.Map<CreateEventViewModel, Event>(model);
                    ES.Save(EPLevent);
                    this.StoreSuccess("Your Event was created");
                    return RedirectToAction("details", new { id = EPLevent.ID });
                } catch (Exception ex) {
                    this.StoreError(string.Format("There was an error: {0}", ex.Message));
                    return View(model);
                }

            }
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
                    //IEnumerable<Tweet> TheTweets = TS.Thedb().Tweets.Where(t => t.EventID == EventID).OrderByDescending(t => t.CreatedDate).Take(1);


                    StringBuilder HTMLString = new StringBuilder();
                    string lasttweettime = string.Empty;
                    bool TheFirst = true;
                    int RecordCount = 0;

                    foreach (Tweet TheT in TheTweets) {
                        if (TheFirst) {
                            lasttweettime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", TheT.CreatedDate);
                            TheFirst = false;
                        }

                        //TODO - This will need to be replaced with something better

                        //<li>
                        //    <img src="@EPLTweet.ProfileImageURL" class="fleft" alt="" />
                        //    <small><a href="http://www.twitter.com/@EPLTweet.FromUserScreenName">@EPLTweet.FromUserScreenName</a></small>
                        //    <p>@Html.Raw(EPLTweet.TextAsHTML)</p>
                        //</li>

                        //HTMLString.Append(PartialView("_TweetTemplate", TheT).ToString());


                        string ProfilePicture = "<img src='" + TheT.ProfileImageURL + "' class='fleft' alt='' height='48' width='48'  />";
                        string FromLine = "<small><a href='http://www.twitter.com/" + TheT.FromUserScreenName + "' target='_blank'>" + TheT.FromUserScreenName + "</a></small>";
                        HTMLString.Append("<li id='Tweet-" + TheT.TwitterID + "' class='tweet newupdates'>" + ProfilePicture + FromLine + "<p>" + TheT.TextAsHTML + "</p></li>");

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
                    //IEnumerable<Image> TheImages = db.Images.Where(t => t.EventID == EventID).OrderByDescending(t => t.DateTime).Take(1);


                    StringBuilder HTML = new StringBuilder();
                    string lastphototime = string.Empty;
                    bool TheFirst = true;
                    int RecordCount = 0;

                    foreach (Image TheI in TheImages) {
                        if (TheFirst) {
                            lastphototime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", TheI.DateTime);
                            TheFirst = false;
                        }

                        //TODO - This will need to be replaced with something better

                        //<div class="withcomment" id="photo-@EPLImage.ID">
                        //    <a href="@EPLImage.Fullsize" rel="prettyPhoto[latestphotos]" title="@EPLImage.ID" id="@EPLImage.ID"><img src="@EPLImage.Thumb" height="154" width="180" border="0" alt="" /></a>
                        //    <a href="#" class="commentbubble">
                        //        @EPLImage.ImageMetaData.Count()
                        //    </a>
                        //</div>

                        string TheImage = "<a href='" + TheI.Fullsize + "' rel='prettyPhoto[latestphotos]' title='" + TheI.ID + "' id='" + TheI.ID + "'><img src='" + TheI.Thumb + "' height='154' width='180' border='0' alt='' /></a>";
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
            model.TotalRecords = currentEvent.CheckIns.Count();

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
        public ActionResult Edit(CreateEventViewModel model) {
            if (ModelState.IsValid) {
                try {
                    Event currentEvent = ES.FindByID(model.ID);
                    currentEvent.SubTitle = model.Subtitle;
                    currentEvent.Name = model.Name;
                    currentEvent.SearchTerms = model.SearchTerms;
                    currentEvent.Description = model.Description;
                    currentEvent.Cost = model.Cost;

                    if (model.CollectionStartDateTime == DateTime.MinValue) {
                        currentEvent.CollectionStartDateTime = DateTime.Now.FromUserTimeZoneToUtc();
                    } else {
                        currentEvent.CollectionStartDateTime = model.CollectionStartDateTime.FromUserTimeZoneToUtc();
                    }
                    if (model.CollectionEndDateTime == DateTime.MinValue) {
                        currentEvent.CollectionEndDateTime = model.EndDateTime.AddDays(14);
                    } else {
                        currentEvent.CollectionEndDateTime = model.CollectionEndDateTime.FromUserTimeZoneToUtc();
                    }

                    currentEvent.StartDateTime = model.StartDateTime.FromUserTimeZoneToUtc();
                    currentEvent.EndDateTime = model.EndDateTime.FromUserTimeZoneToUtc();
                  
                    ES.Save(currentEvent);
                    this.StoreSuccess("Your Event was updated");
                    model = Mapper.Map<Event, CreateEventViewModel>(currentEvent);
                } catch (Exception ex) {
                    this.StoreError(string.Format("There was an error: {0}", ex.Message));
                    return View(model);
                }
            }
            
            return View(model);
        }
    }
}
