using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Core.Filters;
using Epilogger.Web.Models;


namespace Epilogger.Web.Controllers
{
    public class WidgetController : Controller
    {
        EventService _es = new EventService();
        TweetService _ts = new TweetService();
        ImageService _is = new ImageService();
        CheckInService _cs = new CheckInService();

        DateTime _fromDateTime = DateTime.Parse("2000-01-01 00:00:00");
        private DateTime FromDateTime()
        {
            try
            {
                if (Request.QueryString["f"] != null)
                {
                    //_FromDateTime = DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["f"])).FromUserTimeZoneToUtc();
                    _fromDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["f"])));
                }
                return _fromDateTime;
            }
            catch (Exception)
            {
                return _fromDateTime;
            }
        }

        DateTime _toDateTime = DateTime.Parse("2200-12-31 00:00:00");
        private DateTime ToDateTime()
        {
            try
            {
                if (Request.QueryString["t"] != null)
                {
                    //_ToDateTime = DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["t"])).FromUserTimeZoneToUtc();
                    _toDateTime = Timezone.Framework.TimeZoneManager.ToUtcTime(DateTime.Parse(Epilogger.Web.Helpers.base64Decode(Request.QueryString["t"])));
                }
                return _toDateTime;
            }
            catch (Exception)
            {
                return _toDateTime;
            }

        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (_es == null) _es = new EventService();
            if (_ts == null) _ts = new TweetService();
            if (_is == null) _is = new ImageService();
            if (_cs == null) _cs = new CheckInService();
            base.Initialize(requestContext);
        }


        //
        // GET: /Widget/
        [CacheFilter]
        [CompressFilter]
        public ActionResult Index(string id, string width, string height)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetViewModel>(requestedEvent);
            model.TweetCount = _ts.FindTweetCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
            model.Tweets = _ts.FindByEventIDOrderDescTake6(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
            model.ImageCount = _is.FindImageCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
            model.CheckInCount = _cs.FindCheckInCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
            model.CheckIns = _cs.FindByEventIDOrderDescTake16(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());

            model.width = width==null ? 300 : int.Parse(width);
            model.height = height == null ? 500 : int.Parse(height);

            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());

            if (model.height == 500 && model.width == 300)
            {
                model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, 14, FromDateTime(), ToDateTime());
            }
            else if (model.height == 330 && model.width == 550)
            {
                model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, 16, FromDateTime(), ToDateTime());
            }
            else
                model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, 22, FromDateTime(),
                                                               ToDateTime());

            return View(model);
        }

        //
        // GET: /Widget/
        [CacheFilter]
        [CompressFilter]
        public ActionResult PhotoDetails(string id, int PhotoID, string width, string height, int returnto)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetPhotosDetailsViewModel>(requestedEvent);

            model.PhotoID = PhotoID;
            model.Width = width == null ? 300 : int.Parse(width);
            model.Height = height == null ? 500 : int.Parse(height);
            model.returnto = returnto;
            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());

            //Get the photos
            model.Image = _is.FindByID(model.PhotoID);
            

            //Get the Comments
            model.Tweet = _ts.FindByImageID(model.PhotoID, requestedEvent.ID).First();

            return View(model);
        }


        //
        // GET: /Widget/
        [CacheFilter]
        [CompressFilter]
        public ActionResult Photos(string id, string width, string height)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetPhotosViewModel>(requestedEvent);

            model.Width = width == null ? 300 : int.Parse(width);
            model.Height = height == null ? 500 : int.Parse(height);
            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());

            //Get the photos
            if (model.Height==500 && model.Width==300)
            {
                model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, 15, this.FromDateTime(), this.ToDateTime());    
            }
            else
            {
                model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, 22, this.FromDateTime(), this.ToDateTime());
            }
            
            
            return View(model);
        }

        //
        // GET: /Widget/
        [CacheFilter]
        [CompressFilter]
        public ActionResult Tweets(string id, string width, string height)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetTweetsViewModel>(requestedEvent);

            model.Width = width == null ? 300 : int.Parse(width);
            model.Height = height == null ? 500 : int.Parse(height);
            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());

            //Get the tweets
            model.Tweets = _ts.FindByEventIDOrderDescTakeX(requestedEvent.ID, 20, this.FromDateTime(), this.ToDateTime());

            return View(model);
        }

        //
        // GET: /Widget/
        [CacheFilter]
        [CompressFilter]
        public ActionResult Checkins(string id, string width, string height)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetCheckinsViewModel>(requestedEvent);

            model.Width = width == null ? 300 : int.Parse(width);
            model.Height = height == null ? 500 : int.Parse(height);
            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());

            //Get the tweets
            model.Checkins = _cs.FindByEventID(requestedEvent.ID);

            return View(model);
        }

    }
}
