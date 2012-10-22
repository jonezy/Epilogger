using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epilogger.Web.Models;

namespace Epilogger.Web.Controllers
{
    public partial class ErrorController : Controller
    {
        //
        // GET: /Error/

        public virtual ActionResult NotFound()
        {
            return View();
        }
        public virtual ActionResult ServerError()
        {
            // if (godMode)
            //{
            //    var exception = Server.GetLastError();
            //    ViewBag.ErrorException = exception;
            //}
            return View();
        }
        public virtual ActionResult SearchRedirect(SearchEventViewModel model)
        {
            return RedirectToAction("Home/Search", model);
        }
    }
}
