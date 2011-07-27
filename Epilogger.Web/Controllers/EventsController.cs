using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Models;

namespace Epilogger.Web.Controllers {
    public class EventsController : Controller {
        EpiloggerDB db;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (db == null) db = new EpiloggerDB();

            base.Initialize(requestContext);
        }

        public ActionResult Index() {
            return View(Mapper.Map<List<Event>,List<EventDisplayViewModel>>(db.Events.OrderByDescending(e => e.StartDateTime).ToList()));
        }

        public ActionResult Details(int id) {
            return View(Mapper.Map<Event, EventDisplayViewModel>(db.Events.Where(e=>e.ID == id).FirstOrDefault()));
        }

        public ActionResult EventBySlug(string eventSlug) {
            Event foundEvent = null;
            foreach (var e in db.Events) {
                if(e.Name.CreateUrlSlug() == eventSlug) {
                    foundEvent = e;
                    break;
                }
            }

            return View("Details", Mapper.Map<Event, EventDisplayViewModel>(foundEvent));
        }
    }
}
