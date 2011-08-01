using System.Web.Mvc;

namespace Epilogger.Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Message = "Welcome to Epilogger!";
            
            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}
