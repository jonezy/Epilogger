using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models;
using Newtonsoft.Json;
using dotless.Core.Parser.Tree;

namespace Epilogger.Web.Areas.Api.Controllers
{
    public partial class ApiEventsController : Controller
    {
        readonly IEventManager _eventManager;
        ICategoryManager _categoryManager;

        public ApiEventsController()
        {
            _eventManager = new EventManager();
            _categoryManager = new CategoryManager();
        }

        [HttpGet]
        public virtual JsonResult EventList(int? page, int? count)
        {
            var model = this._eventManager.GetEvents(page, count);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

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


        [HttpGet]
        public virtual JsonResult Categories()
        {
            var model = _categoryManager.GetCategories();
            return Json(model, JsonRequestBehavior.AllowGet);
        }






        //NOT IMPLIMENTED
        //Creates a bunch of events. (Not Implemented)
        [HttpPost]
        public virtual JsonResult EventList(List<Event> items)
        {
            var model = this._eventManager.CreateEvents(items);
            return Json(model);
        }



    }


    public class RestHttpVerbFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpMethod = filterContext.HttpContext.Request.HttpMethod;
            filterContext.ActionParameters["httpVerb"] = httpMethod;
            base.OnActionExecuting(filterContext);
        }
    }
}
