using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epilogger.Web.Controllers;

namespace Epilogger.Web.Areas.Admin.Controllers {
    [RequiresAuthentication(ValidUserRole = UserRoleType.Administrator, AccessDeniedMessage = "You must be an administrator to access this part of the site.")]
    public partial class HomeController : BaseController
    {
        //
        // GET: /Admin/Home/

        public virtual ActionResult Index()
        {
            return View();
        }

    }
}
