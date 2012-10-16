using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epilogger.Web.Models;

namespace Epilogger.Web.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult ServerError()
        {
            // if (godMode)
            //{
            //    var exception = Server.GetLastError();
            //    ViewBag.ErrorException = exception;
            //}
            return View();
        }
        public ActionResult SearchRedirect(SearchEventViewModel model)
        {
            return RedirectToAction("Home/Search", model);
        }
    }
}
