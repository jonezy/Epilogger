using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Models;
using System.Dynamic;

namespace Epilogger.Web.Controllers {
    public class EventsController : Controller {
        EpiloggerDB db;
        EventService service = new EventService();

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (db == null) db = new EpiloggerDB();
            if (service == null) service = new EventService();

            base.Initialize(requestContext);
        }

        public ActionResult Index() {
            List<Event> events = service.AllEvents();
            List<EventDisplayViewModel> model = Mapper.Map<List<Event>, List<EventDisplayViewModel>>(events);

            return View(model);
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
