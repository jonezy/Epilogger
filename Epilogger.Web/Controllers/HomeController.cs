using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Core.Filters;
using Epilogger.Web.Models;
using Epilogger.Web.Core.Stats;

namespace Epilogger.Web.Controllers {

    public partial class HomeController : BaseController
    {
        readonly UserLogService _ls = new UserLogService();
        readonly ClickLogService _cs = new ClickLogService();
        readonly EventService _es = new EventService();
        readonly ImageService _is = new ImageService();

        [CacheFilter(Duration = 10)]
        [CompressFilter]
        public virtual ActionResult Index()
        {

            var activity = _es.GetHomepageActivity();
            var model = new HomepageViewModel(
                activity.Take(8).ToList(),
                0,
                activity.Count()
                );

            foreach (var item in model.Activity.Where(item => item.ActivityType == ActivityType.PHOTOS_VIDEOS))
            {
                item.Image = _is.FindByID(Convert.ToInt32(item.ActivityContent));
            }


            //Featured Events
            var featured = _es.GetFeaturedEvents();
            if (featured != null)
                model.FeaturedEvents = new HomepageFeaturedEventsViewModel()
                                           {
                                               Event = featured, 
                                               TopImages = _is.GetNewestPhotosByEventId(featured.ID, 5)
                                           };
            

            return View(model);
        }









        //model.HomepageTotal = new HomepageTotals().HomepageTotal;
        //Do the redis stuff to get the Trending Events
        //var eplRedis = new Common.Redis();
        //var trendingEventId = eplRedis.GetTrendingEvents();
        //var trendingEvents = new List<Event>();
        //foreach (var e in trendingEventId.Take(10).Select(eventId => ES.FindByID((int.Parse(eventId)))))
        //{
        //    if (e !=null)
        //    {
        //        e.Name = e.Name.TruncateAtWord(30);
        //        trendingEvents.Add(e);
        //    }
        //}
        //model.TrendingEvents = trendingEvents;

        //Convert the Image ID in the ActivityContent to the Image HTML from the template. Facilite common HTML and single place for image HTML.

        //var sc = new StatusMessagesService();
        //model.StatusMessages = sc.GetLast10Messages();

        //public ActionResult BetaSignUp() {
        //    IEnumerable<HomepageActivityModel> activity = ES.GetHomepageActivity();
        //    BetaSignUpViewModel bmodel = new BetaSignUpViewModel(
        //        activity.Take(6).ToList(),
        //        0,
        //        activity.Count()
        //    );
        //    foreach (HomepageActivityModel item in bmodel.Activity) {
        //        if (item.ActivityType == ActivityType.PHOTOS_VIDEOS) {
        //            item.Image = IS.FindByID(Convert.ToInt32(item.ActivityContent));
        //        }
        //    }

        //    bmodel.EmailAddress = "youremail@awesome.com";

        //    return View(bmodel);
        //}

        //[HttpPost]
        //public ActionResult BetaSignUp(BetaSignUpViewModel model) {

        //    if (!ModelState.IsValid) {
        //        return View(model);
        //    }

        //    IEnumerable<HomepageActivityModel> activity = ES.GetHomepageActivity();
        //    model.Activity = activity.Take(6).ToList();
        //    model.CurrentPageIndex = 0;

        //    foreach (HomepageActivityModel item in model.Activity) {
        //        if (item.ActivityType == ActivityType.PHOTOS_VIDEOS) {
        //            item.Image = IS.FindByID(Convert.ToInt32(item.ActivityContent));
        //        }
        //    }

        //    //save the email address
        //    model.regDateTime = DateTime.UtcNow;
        //    model.Submitted = true;
        //    model.ipAddress = Request.ServerVariables["REMOTE_ADDR"];

        //    BetaSignup bsu = new BetaSignup();

        //    bsu = Mapper.Map<BetaSignUpViewModel, BetaSignup>(model);

        //    BetaService betaS = new BetaService();
        //    betaS.Save(bsu);

        //    return View(model);
        //}

        public virtual ActionResult StatusMessages()
        {
            var model = new StatusMessage {MSGDateTime = DateTime.UtcNow};
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult StatusMessages(StatusMessage model)
        {

            var sc = new StatusMessagesService();
            sc.Save(model);

            return View();
        }

        [HttpPost]
        public virtual ActionResult LogClick(Epilogger.Data.UserClickAction clickactions)
        {

            clickactions.ActionDateTime = DateTime.UtcNow;
            clickactions.UserID = CurrentUserID;
            clickactions.IPAddress = HttpContext.Request.UserHostAddress;

            _ls.Save(clickactions);

            return Json(new { success = true });
        }

        [HttpPost]
        public virtual ActionResult ClickMap(Epilogger.Data.userClickTracking clickactions)
        {

            clickactions.UserID = CurrentUserID;
            clickactions.ClickDateTime = DateTime.UtcNow;

            _cs.Save(clickactions);

            return Json(new { success = true });
        }

        public virtual ActionResult _GetClickMap(Epilogger.Data.userClickTracking clickactions)
        {
            return PartialView(_cs.GetLast200ClicksByLocation(clickactions.location));
        }

        public virtual ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Search(SearchEventViewModel model)
        {

            IEnumerable<Epilogger.Data.Event> evs = _es.GetEventsBySearchTerm(model.SearchTerm);

            model.Events = Mapper.Map<IEnumerable<Epilogger.Data.Event>, List<DashboardEventViewModel>>(evs);

            return View(model);
        }

        public virtual ActionResult About()
        {
            return View();
        }

        public virtual ActionResult Contact()
        {
            return View();
        }

        public virtual ActionResult Terms()
        {
            return View();
        }

        public virtual ActionResult Privacy()
        {
            return View();
        }

        public virtual ActionResult SocialBar(int eventid, int photoid)
        {
            var es = new EventService();
            var imgs = new ImageService();
            var ts = new TweetService();

            var model = new PhotoDetailsViewModel();
            var theEvent = es.FindByID(eventid);
            if (theEvent != null)
            {
                model.EventId = eventid;
                model.EventSlug = theEvent.EventSlug;
                model.Image = imgs.FindByID(photoid);
                model.Tweets = ts.FindByImageID(photoid, eventid);
                model.Event = theEvent;
                model.HashTag =
                    theEvent.SearchTerms.Split(new string[] { " OR " }, StringSplitOptions.None)[0].Contains("#")
                        ? theEvent.SearchTerms.Split(new string[] { " OR " }, StringSplitOptions.None)[0]
                        : "#" + theEvent.SearchTerms.Split(new string[] { " OR " }, StringSplitOptions.None)[0];
            }

            var apiClient = new Epilogr.APISoapClient();
            model.ShortURL = apiClient.CreateUrl("http://epilogger.com/events/PhotoDetails/" + eventid + "/" + photoid).ShortenedUrl;


            return PartialView(model);
        }

        public virtual ActionResult WebStatus()
        {
            return View();
        }

        public virtual ActionResult UpgradeBenefits()
        {
            return View();
        }
        
    }
}
