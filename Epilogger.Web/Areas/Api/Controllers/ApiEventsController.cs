﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AutoMapper;
using DevOne.Security.Cryptography.BCrypt;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models;
using Epilogger.Web.Areas.Api.Models.Classes;
using Epilogger.Web.Areas.Api.Models.GeckoClasses;
using Epilogger.Web.Core.Filters;
using Newtonsoft.Json;
using dotless.Core.Parser.Tree;


namespace Epilogger.Web.Areas.Api.Controllers
{
    public partial class ApiEventsController : Controller
    {

        

        #region Initialize Managers

        readonly IEventManager _eventManager;
        readonly ICategoryManager _categoryManager;
        readonly ITweetManager _tweetManager;
        readonly IImageManager _imageManager;
        readonly ICheckInManager _checkinManager;
        readonly IUserManager _userManager;
        readonly IVenueManager _venueManager;
        readonly IStatManager _statManager;

        public ApiEventsController()
        {
            _eventManager = new EventManager();
            _categoryManager = new CategoryManager();
            _tweetManager = new TweetManager();
            _imageManager = new ImageManager();
            _checkinManager = new CheckInManager();
            _userManager = new UserManager();
            _venueManager = new VenueManager();
            _statManager = new StatManager();
        }
        #endregion



        #region Events
        

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        //Events

        [HttpGet, HmacAuthorization]
        public virtual JsonResult EventList(int? page, int? count)
        {
            var model = this._eventManager.GetEvents(page, count);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [RestHttpVerbFilter, HmacAuthorization]
        public virtual JsonResult Event(int? id, Event item, string httpVerb)
        {
            switch(httpVerb)
            {
                //case "POST":
                //    return Json(this.eventManager.Create(item));
                //case "PUT":
                //    return Json(this.eventManager.Update(item));
                case "GET":
                    return Json(this._eventManager.GetById(id.GetValueOrDefault()), 
                        JsonRequestBehavior.AllowGet);
                //case "DELETE":
                //    return Json(this.eventManager.Delete(id.GetValueOrDefault()));
            }
            return Json(new { Error = true, Message = "Unknown HTTP verb" });
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet, HmacAuthorization]
        [CacheFilter(Duration = 10)]
        public virtual JsonResult TrendingEvents()
        {
            var model = _eventManager.GetTrendingEvents();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet, HmacAuthorization]
        public virtual JsonResult SearchEvents(string searchTerm)
        {
            var model = _eventManager.SearchEvents(searchTerm);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, HmacAuthorization]
        public virtual JsonResult FeaturedEvents()
        {
            var model = _eventManager.GetFeaturedEvents();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, HmacAuthorization]
        public virtual JsonResult SearchInEvent(int eventId, string searchTerm)
        {
            var model = _eventManager.SearchInEvent(eventId, searchTerm);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        //SearchInEvent(int EventID, string SearchTerm, DateTime FromDateTime, DateTime ToDateTime)
        


        #endregion

        #region TheFeed
        #endregion

        #region Tweets

            [HttpGet, HmacAuthorization]
            public virtual JsonResult Tweets(int eventId, int page, int count)
            {
                return Json(_tweetManager.GetTweetsByEventPages(eventId, page, count), JsonRequestBehavior.AllowGet);
            }

            [HttpGet, HmacAuthorization]
            public virtual JsonResult Top10Tweeters(int eventId)
            {
                return Json(_tweetManager.GetTop10Tweeters(eventId), JsonRequestBehavior.AllowGet);
            }

            [HttpGet, HmacAuthorization]
            public virtual JsonResult TweetsByImageID(int imageId, int eventId, int page, int count)
            {
                return Json(_tweetManager.GetTweetsByImageID(imageId, eventId, page, count), JsonRequestBehavior.AllowGet);
            }

            [HttpGet, HmacAuthorization]
            public virtual JsonResult TweetCountByImageID(int imageId, int eventId)
            {
                return Json(_tweetManager.GetTweetCountByImageID(imageId, eventId), JsonRequestBehavior.AllowGet);
            }


        #endregion

        #region Photos
            [HttpGet, HmacAuthorization]
            public virtual JsonResult ImageCountByEventID(int eventId)
            {
                return Json(_imageManager.FindImageCountByEventID(eventId), JsonRequestBehavior.AllowGet);
            }
            [HttpGet, HmacAuthorization]
            public virtual JsonResult ImagesByEventIDOrderDescTakeX(int eventID, int numberToReturn)
            {
                return Json(_imageManager.FindByEventIDOrderDescTakeX(eventID, numberToReturn), JsonRequestBehavior.AllowGet);
            }
            public virtual JsonResult TopImagesByEventID(int eventID, int numberToReturn)
            {
                return Json(_imageManager.GetTopPhotosByEventID(eventID, numberToReturn), JsonRequestBehavior.AllowGet);
            }
            public virtual JsonResult NewestPhotosByEventID(int eventID, int numberToReturn)
            {
                var model = _imageManager.GetNewestPhotosByEventID(eventID, numberToReturn);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            public virtual JsonResult TopPhotosAndTweetByEventID(int eventID, int numberToReturn)
            {
                return Json(_imageManager.GetTopPhotosAndTweetByEventID(eventID, numberToReturn), JsonRequestBehavior.AllowGet);
            }
            public virtual JsonResult ImagesByEventPaged(int eventID, int page, int count)
            {
                return Json(_imageManager.GetPagedPhotos(eventID, page, count), JsonRequestBehavior.AllowGet);
            }

        #endregion

        #region CheckIns

            public virtual JsonResult FindCheckInCountByEventID(int eventID)
            {
                return Json(_checkinManager.FindCheckInCountByEventID(eventID), JsonRequestBehavior.AllowGet);
            }

            public virtual JsonResult FindByEventIDPaged(int eventID, int page, int count)
            {
                return Json(_checkinManager.FindCheckInsByEventIDPaged(eventID, page, count), JsonRequestBehavior.AllowGet);
            }
        

        #endregion

        #region User
        //My Events

            //Login


            public virtual JsonResult GetUserByID(Guid userId)
            {
                return Json(_userManager.GetUserByID(userId), JsonRequestBehavior.AllowGet);
            }

            public virtual JsonResult GetUserByUsername(string userName)
            {
                return Json(_userManager.GetUserByUsername(userName), JsonRequestBehavior.AllowGet);
            }

            public virtual JsonResult GetUserByEmail(string email)
            {
                return Json(_userManager.GetUserByEmail(email), JsonRequestBehavior.AllowGet);
            }

        #endregion

        #region User Authorization

        [GetHeadersFilterAttribute, HmacAuthorization]
            public virtual JsonResult AuthEPLUser(NameValueCollection headers)
        {

            var userNamePass = Encoding.UTF8.GetString(Convert.FromBase64String(headers["Authorization"].Substring(6)));
            var splitUserPass = userNamePass.Split(new string[] { ":" }, StringSplitOptions.None);

            var user = _userManager.GetFullUserByUsername(splitUserPass[0]);
            if (!BCryptHelper.CheckPassword(splitUserPass[1], user.Password))
            {
                return Json(new { Error = true, Message = "There was a problem with either the username or password. Please correct and try again." }, JsonRequestBehavior.AllowGet);
            }

            return Json(Mapper.Map<User, ApiUser>(user), JsonRequestBehavior.AllowGet);
         
        }


        #endregion

        #region Categories

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

    [HttpGet, HmacAuthorization]
    public virtual JsonResult Categories()
    {
        var model = _categoryManager.GetCategories();
        return Json(model, JsonRequestBehavior.AllowGet);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------

    [HttpGet, HmacAuthorization]
    public virtual JsonResult EventsByCategoryID(int categoryId, int page, int count)
    {
        var model = _eventManager.EventsByCategoyIdPaged(categoryId, page, count);
        return Json(model, JsonRequestBehavior.AllowGet);
    }


    #endregion

        #region Venue

            public virtual JsonResult GetVenueByID(int id)
            {
                return Json(_venueManager.FindByID(id), JsonRequestBehavior.AllowGet);
            }

            public virtual JsonResult GetVenueByFoursquareVenueID(string foursquareVenueId)
            {
                return Json(_venueManager.FindByFourSquareVenueID(foursquareVenueId), JsonRequestBehavior.AllowGet);
            }


        #endregion

        #region Stats

            public virtual JsonResult GeckoGetUserGrowthDayOverDay()
            {
                var growth = _statManager.GetUserGrowth();
                var GeckoGrowth = new GeckoUserGrowth();
                var GeckoSettings = new GeckoSettings();
                GeckoSettings.axisy = new List<string>();
                GeckoSettings.axisx = new List<string>();
                GeckoGrowth.item = new List<string>();

                GeckoSettings.colour = "ee4490";
                GeckoSettings.axisy.Add(growth.Min(t => t.NumberOfUsers).ToString(CultureInfo.InvariantCulture));
                GeckoSettings.axisy.Add(growth.Max(t => t.NumberOfUsers).ToString(CultureInfo.InvariantCulture));

                foreach(UserGrowthStats i in growth)
                {
                    GeckoGrowth.item.Add(i.NumberOfUsers.ToString(CultureInfo.InvariantCulture));
                }

                GeckoGrowth.settings = GeckoSettings;

                return Json(GeckoGrowth, JsonRequestBehavior.AllowGet);
            }

            public virtual JsonResult GeckoGetUserGrowthLastWeek()
            {
                var growth = _statManager.GetUserGrowth(DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);
                var GeckoGrowth = new GeckoUserGrowth();
                var GeckoSettings = new GeckoSettings();
                GeckoSettings.axisy = new List<string>();
                GeckoSettings.axisx = new List<string>();
                GeckoGrowth.item = new List<string>();

                GeckoSettings.colour = "ee4490";
                GeckoSettings.axisy.Add(growth.Min(t => t.NumberOfUsers).ToString(CultureInfo.InvariantCulture));
                GeckoSettings.axisy.Add(growth.Max(t => t.NumberOfUsers).ToString(CultureInfo.InvariantCulture));
                GeckoSettings.axisx.Add(DateTime.UtcNow.AddDays(-7).ToString("MMMM d"));
                GeckoSettings.axisx.Add(DateTime.UtcNow.ToString("MMMM d"));


                foreach (UserGrowthStats i in growth)
                {
                    GeckoGrowth.item.Add(i.NumberOfUsers.ToString(CultureInfo.InvariantCulture));
                }

                GeckoGrowth.settings = GeckoSettings;

                return Json(GeckoGrowth, JsonRequestBehavior.AllowGet);
            }

            public virtual JsonResult GeckoGetUserGrowthLastMonth()
            {
                var growth = _statManager.GetUserGrowth(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow);
                var GeckoGrowth = new GeckoUserGrowth();
                var GeckoSettings = new GeckoSettings();
                GeckoSettings.axisy = new List<string>();
                GeckoSettings.axisx = new List<string>();
                GeckoGrowth.item = new List<string>();

                GeckoSettings.colour = "ee4490";
                GeckoSettings.axisy.Add(growth.Min(t => t.NumberOfUsers).ToString(CultureInfo.InvariantCulture));
                GeckoSettings.axisy.Add(growth.Max(t => t.NumberOfUsers).ToString(CultureInfo.InvariantCulture));
                GeckoSettings.axisx.Add(DateTime.UtcNow.AddMonths(-1).ToString("MMMM d"));
                GeckoSettings.axisx.Add(DateTime.UtcNow.ToString("MMMM d"));

                foreach (UserGrowthStats i in growth)
                {
                    GeckoGrowth.item.Add(i.NumberOfUsers.ToString(CultureInfo.InvariantCulture));
                }

                GeckoGrowth.settings = GeckoSettings;

                return Json(GeckoGrowth, JsonRequestBehavior.AllowGet);
            }


        #endregion



            //NOT IMPLIMENTED
        //Creates a bunch of events. (Not Implemented)
        [HttpPost]
        public virtual JsonResult EventList(List<Event> items)
        {
            var model = this._eventManager.CreateEvents(items);
            return Json(model);
        }


    }





//-----------------------------------------------------------------------------------------------------------------------------------------------------------
  



    public class RestHttpVerbFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpMethod = filterContext.HttpContext.Request.HttpMethod;
            filterContext.ActionParameters["httpVerb"] = httpMethod;
            base.OnActionExecuting(filterContext);
        }
    }

    public class GetHeadersFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpheaders = filterContext.HttpContext.Request.Headers;
            filterContext.ActionParameters["headers"] = httpheaders;
            base.OnActionExecuting(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class HmacAuthorizationAttribute : ActionFilterAttribute
    {
        APIApplicationService _as = new APIApplicationService();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                //Verify we have been provided with the required ClientID and HMAC Hash (data)
                var clientId = filterContext.HttpContext.Request.QueryString["clientid"];
                if (String.IsNullOrEmpty(clientId))
                {
                    throw new MissingFieldException(String.Format("The required parameter clientid was missing, please include clientid when making API requests."));
                }

                var signature = filterContext.HttpContext.Request.QueryString["signature"];
                if (String.IsNullOrEmpty(signature))
                {
                    throw new MissingFieldException(String.Format("The required parameter signature was missing, please include signature when making API requests."));
                }

                var timeStampString = filterContext.HttpContext.Request.QueryString["timestamp"];
                if (String.IsNullOrEmpty(timeStampString))
                {
                    throw new MissingFieldException(String.Format("The required parameter timeStamp was missing, please include timeStamp when making API requests."));
                }

                //Check time Stamp to ensure this request is fresh. 15secs
                var epochTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;

                if (epochTime - long.Parse(timeStampString) > 15)
                {
                    throw new AccessViolationException(String.Format("The timeStamp on this request is too old, please generate a fresh request."));
                }


                //Get the API application record from the DB.
                var apiApplication = _as.FindByClientId(clientId);
                if (apiApplication == null)
                {
                    throw new AccessViolationException(String.Format("This Client is not registered for API Access, please contact Epilogger if you would like access."));
                }

                //All parameters have are here, let's make sure they match.
                var hmacValidationString = getHMACMD5Signature(filterContext.HttpContext.Request.Url.LocalPath, "", timeStampString, clientId, apiApplication.ClientSecret);
                
                if (hmacValidationString != signature)
                {
                    throw new AccessViolationException(String.Format("The signature in this request does not match the correct signature."));
                }

                filterContext.HttpContext.Trace.Write("(Logging Filter)Action Executing: " + filterContext.ActionDescriptor.ActionName);

                base.OnActionExecuting(filterContext);
            }
            catch (Exception ex)
            {
                filterContext.Result = new EmptyResult();
                filterContext.HttpContext.Response.Write(ex.Message);
            }
            
        }

        //This is used to verify the request

        private static string getHMACMD5Signature(string methodPath, string additionalParameters, string expires, string accessKey, string secretKey)
        {
            //build the uri to sign, exclude the domain part, start with '/'
            var uristring = new StringBuilder(); //will hold the final uri
            uristring.Append(methodPath.StartsWith("/") ? "" : "/"); //start with a slash
            uristring.Append(methodPath); //this is the address of the resource
            uristring.Append("?clientid=" + HttpUtility.UrlEncode(accessKey)); //url encoded parameters
            uristring.Append("&timestamp=" + HttpUtility.UrlEncode(expires));
            uristring.Append(additionalParameters);

            //calculate hmac signature
            byte[] secretBytes = Encoding.ASCII.GetBytes(secretKey);
            var hmac = new HMACMD5(secretBytes);
            byte[] dataBytes = Encoding.ASCII.GetBytes(uristring.ToString());
            byte[] computedHash = hmac.ComputeHash(dataBytes);
            string computedHashString = Convert.ToBase64String(computedHash);

            return computedHashString;
        }
    }


}
