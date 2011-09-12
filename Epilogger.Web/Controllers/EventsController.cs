using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Models;
using RichmondDay.Helpers;
using System.Text;

namespace Epilogger.Web.Controllers {
    public class EventsController : BaseController {
        EpiloggerDB db;
        EventService ES = new EventService();
        TweetService TS = new TweetService();
        ImageService IS = new ImageService();
        CheckInService CS = new CheckInService();
        ExternalLinkService LS = new ExternalLinkService();
        BlogService BS = new BlogService();
        

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

        //[RequiresAuthentication(AccessDeniedMessage = "You must be logged in to view the list of events")]
        public ActionResult Index() {
            List<Event> events = ES.AllEvents();
            List<EventDisplayViewModel> model = Mapper.Map<List<Event>, List<EventDisplayViewModel>>(events);

            return View(model);
        }

        [LogErrors(FriendlyErrorMessage = "There was a problem saving your event")]
        public ActionResult Error() {
            throw new Exception("This is a test exception");
        }

        //[RequiresAuthentication(AccessDeniedMessage = "You must be logged in to view the details of that event")]
        public ActionResult Details(int id) {

            EventDisplayViewModel Model = Mapper.Map<Event, EventDisplayViewModel>(ES.FindByID(id));

            Model.TweetCount = TS.FindTweetCountByEventID(id);
            Model.Tweets = TS.FindByEventIDOrderDescTake6(id);
                        
            //Not optimized
            Model.Images = IS.FindByEventID(id);
            Model.CheckIns = CS.FindByEventID(id);
            Model.ExternalLinks = LS.FindByEventID(id);
            Model.BlogPosts = BS.FindByEventID(id);
            
            return View(Model);
        }


        public ActionResult AllPhotos(int id)
        {

            AllPhotosDisplayViewModel Model = Mapper.Map<Event, AllPhotosDisplayViewModel>(ES.FindByID(id));

            Model.PhotoCount = IS.FindImageCountByEventID(id);

            Model.Images = IS.GetPagedPhotos(id, 1, 10);

            return View(Model);
        }



        [RequiresAuthentication(AccessDeniedMessage = "You must be logged in to view the details of that event")]
        public ActionResult CreateEvent() {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(CreateEventViewModel model) {

            try {
                model.UserID = Guid.Parse(CookieHelpers.GetCookieValue("lc", "uid").ToString());

                Event EPLevent = Mapper.Map<CreateEventViewModel, Event>(model);
                ES.Save(EPLevent);
                this.StoreSuccess("Your Event was created");
            } catch (Exception ex) {
                this.StoreError(string.Format("There was an error: {0}", ex.Message));
            }

            return View();
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

        public ActionResult GetImageComments(int id)
        {
            return PartialView("_ImageComments", TS.FindByImageID(id));
        }



        [HttpPost]
        public ActionResult GetLastTweetsJSON(int Count, string pageLoadTime, int EventID)
        {
            Dictionary<String, Object> dict = new Dictionary<String, Object>();

            if (pageLoadTime.Length > 0)
            {
                if (pageLoadTime != "undefined")
                {

                    pageLoadTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Parse(pageLoadTime));

                    EpiloggerDB db = TS.Thedb();
                    IEnumerable<Tweet> TheTweets = TS.Thedb().Tweets.Where(t => t.EventID == EventID & t.CreatedDate > DateTime.Parse(pageLoadTime)).OrderByDescending(t => t.CreatedDate).Take(Count);
                    //IEnumerable<Tweet> TheTweets = TS.Thedb().Tweets.Where(t => t.EventID == EventID).OrderByDescending(t => t.CreatedDate).Take(1);


                    StringBuilder HTML = new StringBuilder();
                    string lasttweettime = string.Empty;
                    bool TheFirst = true;
                    int RecordCount = 0;

                    foreach (Tweet TheT in TheTweets)
                    {
                        if (TheFirst)
                        {
                            lasttweettime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", TheT.CreatedDate);
                            TheFirst = false;
                        }

                        //TODO - This will need to be replaced with something better

                        //<li>
                        //    <img src="@EPLTweet.ProfileImageURL" class="fleft" alt="" />
                        //    <small><a href="http://www.twitter.com/@EPLTweet.FromUserScreenName">@EPLTweet.FromUserScreenName</a></small>
                        //    <p>@Html.Raw(EPLTweet.TextAsHTML)</p>
                        //</li>


                        string ProfilePicture = "<img src='" + TheT.ProfileImageURL + "' class='fleft' alt='' />";
                        string FromLine = "<small><a href='http://www.twitter.com/" + TheT.FromUserScreenName + "' target='_blank'>" + TheT.FromUserScreenName + "</a></small>";
                        HTML.Append("<li id='Tweet-" + TheT.TwitterID + "' class='tweet newupdates'>" + ProfilePicture + FromLine + "<p>" + TheT.TextAsHTML + "</p></li>");

                        RecordCount++;
                    }


                    //Return the Dictionary as it's IEnumerable and it creates the correct JSON doc.
                    dict = new Dictionary<String, Object>();

                    dict.Add("numberofnewtweets", RecordCount);
                    dict.Add("lasttweettime", lasttweettime);
                    dict.Add("tweetcount", string.Format("{0:#,###}", db.Tweets.Where(t => t.EventID == EventID).Count()));
                    dict.Add("html", HTML.ToString());
                }
            }

            return Json(dict);
        }

        [HttpPost]
        public ActionResult GetLastPhotosJSON(int Count, string pageLoadTime, int EventID)
        {
            Dictionary<String, Object> dict = new Dictionary<String, Object>();

            if (pageLoadTime.Length > 0)
            {
                if (pageLoadTime != "undefined")
                {

                    pageLoadTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Parse(pageLoadTime));

                    EpiloggerDB db = TS.Thedb();
                    IEnumerable<Image> TheImages = db.Images.Where(t => t.EventID == EventID & t.DateTime > DateTime.Parse(pageLoadTime)).OrderByDescending(t => t.DateTime).Take(Count);
                    //IEnumerable<Image> TheImages = db.Images.Where(t => t.EventID == EventID).OrderByDescending(t => t.DateTime).Take(1);


                    StringBuilder HTML = new StringBuilder();
                    string lastphototime = string.Empty;
                    bool TheFirst = true;
                    int RecordCount = 0;

                    foreach (Image TheI in TheImages)
                    {
                        if (TheFirst)
                        {
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
    }
}
