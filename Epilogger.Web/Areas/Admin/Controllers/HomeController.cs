using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epilogger.Data;
using Epilogger.Web.Areas.Admin.Models;
using Epilogger.Web.Controllers;

namespace Epilogger.Web.Areas.Admin.Controllers {
    [RequiresAuthentication(ValidUserRole = UserRoleType.Administrator, AccessDeniedMessage = "You must be an administrator to access this part of the site.")]
    public partial class HomeController : BaseController
    {
        EpiloggerDB _db;
        EventService _es = new EventService();
        UserService _us = new UserService();

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (_db == null) _db = new EpiloggerDB();
            if (_es == null) _es = new EventService();
            if (_us == null) _us = new UserService();
            base.Initialize(requestContext);
        }


        //
        // GET: /Admin/Home/
        public virtual ActionResult Index()
        {
            var model = new AdminDashboardViewModel
                            {
                                TotalEvents = _es.Count(),
                                TotalUsers = _us.GetActiveUserCount(),
                                EventsToday = _db.Events.Count(e => e.CreatedDateTime >= DateTime.UtcNow.AddDays(-1)),
                                EventsYesterday =
                                    _db.Events.Count(
                                        e =>
                                        e.CreatedDateTime >= DateTime.UtcNow.AddDays(-2) &&
                                        e.CreatedDateTime <= DateTime.UtcNow.AddDays(-1)),
                                AvgNumberOfEventsPerDay = 0,
                                EventsThisWeek = _db.Events.Count(e => e.CreatedDateTime >= DateTime.UtcNow.AddDays(-7)),
                                EventsLastWeek =
                                    _db.Events.Count(
                                        e =>
                                        e.CreatedDateTime >= DateTime.UtcNow.AddDays(-14) &&
                                        e.CreatedDateTime <= DateTime.UtcNow.AddDays(-7))
                            };



            return View(model);
        }

    }
}
