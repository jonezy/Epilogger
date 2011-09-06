using System.Web.Mvc;

namespace Epilogger.Web.Controllers {
    [RequiresAuthentication(AccessDeniedMessage = "You must be logged in to view your dashboard")]
    public class DashboardController : BaseController {

        public ActionResult Index() {
            return View();
        }

    }
}
