using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Mvc;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Core.Filters;
using Epilogger.Web.Models;
using Twitterizer;


namespace Epilogger.Web.Controllers
{
    public partial class WidgetController : BaseController
    {
        EventService _es = new EventService();
        TweetService _ts = new TweetService();
        ImageService _is = new ImageService();
        CheckInService _cs = new CheckInService();
        WidgetCustomSettingsService _ws = new WidgetCustomSettingsService();
        UserTwitterActionService _userTwitterActionService = new UserTwitterActionService();

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

        private int GetHeightOffset(int height, int width, bool fullLayout)
        {
            var heightOffset = 0;
            
            //Portrait
            if (width < 300)
            {
                //251px-299px
                heightOffset = fullLayout ? 118 : 226;
                //Chrome
                //heightOffset = fullLayout ? 116 : 224;
            }
            else
            {
                //Over 300px
                heightOffset = fullLayout ? 127 : 243;
                if (width >= 500)
                {
                    heightOffset = 127;
                }

                //Chrome
                //heightOffset = fullLayout ? 125 : 241;
            }

            if (width < 250)
            {
                //Under 250px
                heightOffset = fullLayout ? 118 : 208;
                
                //Chrome
                //heightOffset = fullLayout ? 116 : 206;
                
            }


            //Landscape
            if (height < width)
            {
                //Offsets for Landscape layout
                heightOffset = 127;
                //Chrome
                //heightOffset = 125;
            }

            return heightOffset;

        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (_es == null) _es = new EventService();
            if (_ts == null) _ts = new TweetService();
            if (_is == null) _is = new ImageService();
            if (_cs == null) _cs = new CheckInService();
            if (_ws == null) _ws = new WidgetCustomSettingsService();
            base.Initialize(requestContext);
        }


        //
        // GET: /Widget/
        [CacheFilter]
        [CompressFilter]
        public virtual ActionResult Index(string id, string width, string height)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetViewModel>(requestedEvent);

            model.CustomSettings = _ws.FindByEventID(requestedEvent.ID);
            model.TweetCount = _ts.FindTweetCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
            model.Tweets = _ts.FindByEventIDOrderDescTakeX(requestedEvent.ID, 12, this.FromDateTime(), this.ToDateTime());
            model.ImageCount = _is.FindImageCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
            model.CheckInCount = _cs.FindCheckInCountByEventID(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());
            model.CheckIns = _cs.FindByEventIDOrderDescTake16(requestedEvent.ID, this.FromDateTime(), this.ToDateTime());

            //model.width = width==null ? 300 : int.Parse(width);
            //model.height = height == null ? 500 : int.Parse(height);
            
            model.Width = width==null ? 100 : int.Parse(width);
            model.Height = height == null ? 100 : int.Parse(height);

            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());


            //Return a different number of photos depending on the size of the widget.
            if (model.Width < model.Height)
            {
                //Portrait
                model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, model.Width <= 300 ? 3 : 6, FromDateTime(), ToDateTime());
            }
            else
            {
                //Landscape
                if (model.Width >= 500 && model.Height >= 300)
                {
                    model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, (int)((model.Height - 125) / 85)*2, FromDateTime(), ToDateTime());

                    if (model.Width >= 600)
                    {
                        model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, (int)((model.Height-125) / 85)*3, FromDateTime(), ToDateTime());
                    }
                }
            }

            if ( model.Width==100 && model.Height==100)
            {
                //Floating layout, load lots.
                model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, 25, FromDateTime(), ToDateTime());
            }

            model.HeightOffset = GetHeightOffset(model.Height, model.Width, false);
            
            return View(model);
        }

        //
        // GET: /Widget/
        [CacheFilter]
        [CompressFilter]
        public virtual ActionResult PhotoDetails(string id, int PhotoID, string width, string height, int returnto)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetPhotosDetailsViewModel>(requestedEvent);

            model.PhotoID = PhotoID;

            model.Width = width == null ? 100 : int.Parse(width);
            model.Height = height == null ? 100 : int.Parse(height);

            model.Returnto = returnto;
            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());

            //Get the photos
            model.Image = _is.FindByID(model.PhotoID);
            

            //Get the Comments
            model.Tweet = _ts.FindByImageID(model.PhotoID, requestedEvent.ID).FirstOrDefault() ?? new Tweet();

            model.CustomSettings = _ws.FindByEventID(requestedEvent.ID);
            model.HeightOffset = GetHeightOffset(model.Height, model.Width, true);

            return View(model);
        }



        //
        // GET: /Widget/
        [CacheFilter]
        [CompressFilter]
        public virtual ActionResult Photos(string id, string width, string height)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetPhotosViewModel>(requestedEvent);

            model.Width = width == null ? 100 : int.Parse(width);
            model.Height = height == null ? 100 : int.Parse(height);

            model.CustomSettings = _ws.FindByEventID(requestedEvent.ID);
            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());


            //Return a different number of photos depending on the size of the widget.
            if (model.Width < model.Height)
            {
                //Portrait
                model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, 24, FromDateTime(), ToDateTime());
            }
            else
            {
                //Landscape
                if (model.Width >= 500 && model.Height >= 300)
                {
                    model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, (int)((model.Height * 2 - 125) / 85) * 5, FromDateTime(), ToDateTime());

                    if (model.Width >= 600)
                    {
                        model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, (int)((model.Height * 2 - 125) / 85) * 8, FromDateTime(), ToDateTime());
                    }
                }
            }

            if (model.Width == 100 && model.Height == 100)
            {
                //Floating layout, load lots.
                model.Images = _is.FindByEventIDOrderDescTakeX(requestedEvent.ID, 25, FromDateTime(), ToDateTime());
            }


            model.HeightOffset = GetHeightOffset(model.Height, model.Width, true);
            
            return View(model);
        }

        //
        // GET: /Widget/
        [CacheFilter]
        [CompressFilter]
        public virtual ActionResult Tweets(string id, string width, string height)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetTweetsViewModel>(requestedEvent);

            //model.Width = width == null ? 300 : int.Parse(width);
            //model.Height = height == null ? 500 : int.Parse(height);
            model.Width = width == null ? 100 : int.Parse(width);
            model.Height = height == null ? 100 : int.Parse(height);

            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());

            //Get the tweets
            model.Tweets = _ts.FindByEventIDOrderDescTakeX(requestedEvent.ID, 20, this.FromDateTime(), this.ToDateTime());

            model.CustomSettings = _ws.FindByEventID(requestedEvent.ID);
            model.HeightOffset = GetHeightOffset(model.Height, model.Width, true);

            return View(model);
        }

        //
        // GET: /Widget/
        [CacheFilter]
        [CompressFilter]
        public virtual ActionResult Checkins(string id, string width, string height)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetCheckinsViewModel>(requestedEvent);
            
            model.Width = width == null ? 100 : int.Parse(width);
            model.Height = height == null ? 100 : int.Parse(height);

            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());

            //Get the tweets
            model.Checkins = _cs.FindByEventID(requestedEvent.ID);

            model.CustomSettings = _ws.FindByEventID(requestedEvent.ID);
            model.HeightOffset = GetHeightOffset(model.Height, model.Width, true);

            return View(model);
        }


        //
        // GET: /Widget/
        [CacheFilter]
        [CompressFilter]
        public virtual ActionResult TwitterReply(string id, long twitterid, string width, string height, string returnurl, int returnto)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetTweetReplyViewModel>(requestedEvent);

            model.Width = width == null ? 100 : int.Parse(width);
            model.Height = height == null ? 100 : int.Parse(height);
            model.CustomSettings = _ws.FindByEventID(requestedEvent.ID);
            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());
            model.HeightOffset = GetHeightOffset(model.Height, model.Width, true);
            model.ReturnUrl = returnurl;
            model.Returnto = returnto;

            model.Tweet = _ts.FindByTwitterID(twitterid);

            model.IsTwitterAuthed = CurrentUserTwitterAuthorization != null;

            var apiClient = new Epilogr.APISoapClient();
            model.ShortEventURL = apiClient.CreateUrl("http://epilogger.com/events/" + model.EventSlug).ShortenedUrl;

            return View(model);
        }

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
                    TweetId = (long)ts.ResponseObject.Id,
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





        [CacheFilter]
        [CompressFilter]
        public virtual ActionResult TwitterRetweet(string id, long twitterid, string width, string height, string returnurl, int returnto)
        {
            var requestedEvent = _es.FindBySlug(id);
            var model = Mapper.Map<Event, WidgetTweetReplyViewModel>(requestedEvent);

            model.Width = width == null ? 100 : int.Parse(width);
            model.Height = height == null ? 100 : int.Parse(height);
            model.CustomSettings = _ws.FindByEventID(requestedEvent.ID);
            model.EpiloggerCounts = new Core.Stats.WidgetTotals().GetWidgetTotals(requestedEvent.ID, FromDateTime(), ToDateTime());
            model.HeightOffset = GetHeightOffset(model.Height, model.Width, true);
            model.ReturnUrl = returnurl;
            model.Returnto = returnto;

            model.Tweet = _ts.FindByTwitterID(twitterid);

            model.IsTwitterAuthed = CurrentUserTwitterAuthorization != null;

            var apiClient = new Epilogr.APISoapClient();
            model.ShortEventURL = apiClient.CreateUrl("http://epilogger.com/events/" + model.EventSlug).ShortenedUrl;

            return View(model);
        }


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


        public virtual ActionResult TwitterAuth()
        {
            //Twitter OAuth has returned here. Log the user tokens, log the user in, and load this page which closed the window and refreshes the parent.

            var test = new Areas.Authentication.Controllers.TwitterController();
            test.ConnectAccountNoRedirect(Request.QueryString["oauth_token"]);
            
            return PartialView();
        }

        public void GetPhotoThumbnail(string photoUrl, int width, int height)
        {
            new WebImage(new System.Net.WebClient().DownloadData(photoUrl))
                    .Resize(width, height, true, true) // Resizing the image to 100x100 px on the fly...
                    .Crop(1, 1) // Cropping it to remove 1px border at top and left sides (bug in WebImage)
                    .Write();
        }

        
    }
}
