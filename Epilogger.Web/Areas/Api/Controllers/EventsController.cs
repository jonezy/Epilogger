using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models;
using dotless.Core.Parser.Tree;

namespace Epilogger.Web.Areas.Api.Controllers
{
    public class EventsController : Controller
    {
        
        IEventManager eventManager;

        public EventsController()
        {
            this.eventManager = new EventManager();
        }

        [HttpGet]
        public JsonResult EventList(int? page, int? count)
        {
            var model = this.eventManager.GetEvents(page, count);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EventList(List<Event> items)
        {
            var model = this.eventManager.CreateEvents(items);
            return Json(model);
        }

        [RestHttpVerbFilter]
        public JsonResult Event(int? id, Event item, string httpVerb)
        {
            switch(httpVerb)
            {
                case "POST":
                    return Json(this.eventManager.Create(item));
                case "PUT":
                    return Json(this.eventManager.Update(item));
                case "GET":
                    return Json(this.eventManager.GetById(id.GetValueOrDefault()), 
                        JsonRequestBehavior.AllowGet);
                case "DELETE":
                    return Json(this.eventManager.Delete(id.GetValueOrDefault()));
            }
            return Json(new { Error = true, Message = "Unknown HTTP verb" });
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
