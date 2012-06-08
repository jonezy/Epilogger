﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models;
using Newtonsoft.Json;
using dotless.Core.Parser.Tree;

namespace Epilogger.Web.Areas.Api.Controllers
{
    public partial class ApiEventsController : Controller
    {

        #region Initialize Managers

        readonly IEventManager _eventManager;
        ICategoryManager _categoryManager;

        public ApiEventsController()
        {
            _eventManager = new EventManager();
            _categoryManager = new CategoryManager();
        }
        #endregion

        #region User Authentication
        //User Authentication

#endregion

        #region Events
        

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        //Events

        [HttpGet]
        public virtual JsonResult EventList(int? page, int? count)
        {
            var model = this._eventManager.GetEvents(page, count);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [RestHttpVerbFilter]
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


        #endregion

        #region Categories

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [HmacAuthorization]
        public virtual JsonResult Categories()
        {
            var model = _categoryManager.GetCategories();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

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


    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class HmacAuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Verify we have been provided with the required ClientID and HMAC Hash (data)
            var clientId = filterContext.HttpContext.Request.QueryString["clientid"];
            if (String.IsNullOrEmpty(clientId))
            {
                throw new MissingFieldException(String.Format("The required parameter clientid was missing, please include clientid when making API requests."));
            }

            var hmacValue = filterContext.HttpContext.Request.QueryString["signature"];
            if (String.IsNullOrEmpty(hmacValue))
            {
                throw new MissingFieldException(String.Format("The required parameter signature was missing, please include signature when making API requests."));
            }

            var timeStamp = filterContext.HttpContext.Request.QueryString["timestamp"];
            if (String.IsNullOrEmpty(timeStamp))
            {
                throw new MissingFieldException(String.Format("The required parameter timeStamp was missing, please include timeStamp when making API requests."));
            }
            
            //All parameters have are here, let's make sure they match.
            



            //if (string.IsNullOrEmpty(hashToken))
            //{
            //    throw new MissingFieldException(string.Format("The hidden form field named value {0} was missing. This is created by the Html.AntiModelInjection methods. Ensure the name used on your [ValidateAntiModelInjectionAttribute(\"!HERE!\")] matches the field name used in Html.AntiModelInjection method. If this attribute is used on a controller method that is meant for HttpGet, then the form value would not yet exist. This attribute is meant to be used on controller methods accessed via HttpPost.", encryptedPropertyName));
            //}


            ////Plain text must be available to compare.
            //if (string.IsNullOrEmpty(formValue))
            //{
            //    throw new MissingFieldException(string.Format("The form value {0} was missing. If this attribute is used on a controller method that is meant for HttpGet, then the form value would not yet exist. This attribute is meant to be used on controller methods accessed via HttpPost.", _propertyName));
            //}

            filterContext.HttpContext.Trace.Write("(Logging Filter)Action Executing: " + filterContext.ActionDescriptor.ActionName);

            base.OnActionExecuting(filterContext);
        }

        //This is used to verify the request

        //private static string getHMACMD5Signature(string methodPath, string additionalParameters, string expires)
        //{
        //    //build the uri to sign, exclude the domain part, start with '/'
        //    StringBuilder uristring = new StringBuilder(); //will hold the final uri
        //    uristring.Append(methodPath.StartsWith("/") ? "" : "/"); //start with a slash
        //    uristring.Append(methodPath); //this is the address of the resource
        //    uristring.Append("?AccessKey=" + HttpUtility.UrlEncode(accessKey)); //url encoded parameters
        //    uristring.Append("&Expires=" + HttpUtility.UrlEncode(expires));
        //    uristring.Append(additionalParameters);

        //    //calculate hmac signature
        //    byte[] secretBytes = Encoding.ASCII.GetBytes(secretKey);
        //    HMACMD5 hmac = new HMACMD5(secretBytes);
        //    byte[] dataBytes = Encoding.ASCII.GetBytes(uristring.ToString());
        //    byte[] computedHash = hmac.ComputeHash(dataBytes);
        //    string computedHashString = Convert.ToBase64String(computedHash);

        //    return computedHashString;
        //}
    }


}
