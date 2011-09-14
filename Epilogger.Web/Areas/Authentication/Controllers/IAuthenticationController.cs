using System.Web.Mvc;

namespace Epilogger.Web.Areas.Authentication.Controllers {
    public interface IAuthenticationController {
        ActionResult ConnectRequest();
        ActionResult ConnectAccount();
        ActionResult Disconnect();
    }
}