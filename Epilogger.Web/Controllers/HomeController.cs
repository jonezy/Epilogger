using System.Web.Mvc;
using System;

namespace Epilogger.Web.Controllers {
    public class HomeController : BaseController {
        UserLogService LS = new UserLogService();


        public ActionResult Index() {
            ViewBag.Message = "Welcome to Epilogger!";
            
            return View();
        }

        public ActionResult About() {
            return View();
        }


        [HttpPost]
        public ActionResult LogClick(Epilogger.Data.UserClickAction clickactions)
        {

            clickactions.ActionDateTime = DateTime.UtcNow;
            clickactions.UserID = CurrentUserID;
            
            LS.Save(clickactions);
            
            return Json(new { success = true });
        }
        



    }
}
