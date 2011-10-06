using System.Web.Mvc;
using System;
using Epilogger.Web.Models;
using System.Collections.Generic;
using AutoMapper;

namespace Epilogger.Web.Controllers {
    public class HomeController : BaseController {
        UserLogService LS = new UserLogService();
        ClickLogService CS = new ClickLogService();
        EventService ES = new EventService();


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


        [HttpPost]
        public ActionResult ClickMap(Epilogger.Data.userClickTracking clickactions)
        {

            clickactions.UserID = CurrentUserID;
            clickactions.ClickDateTime = DateTime.UtcNow;

            CS.Save(clickactions);

            return Json(new { success = true });
        }

        public ActionResult _GetClickMap(Epilogger.Data.userClickTracking clickactions)
        {
            return PartialView(CS.GetLast200ClicksByLocation(clickactions.location));
        }


        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchEventViewModel model)
        {

            IEnumerable<Epilogger.Data.Event> Evs = ES.GetEventsBySearchTerm(model.SearchTerm);

            model.Events = Mapper.Map<IEnumerable<Epilogger.Data.Event>, List<DashboardEventViewModel>>(Evs);
            
            return View(model);
        }


    }
}
