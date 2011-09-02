using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Models;
using RichmondDay.Helpers;

namespace Epilogger.Web.Controllers {
    public class EventsController : BaseController {
        EpiloggerDB db;
        EventService ES = new EventService();
        TweetService TS = new TweetService();
        

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (db == null) db = new EpiloggerDB();
            if (ES == null) ES = new EventService();
            if (TS == null) TS = new TweetService();

            base.Initialize(requestContext);
        }

        [RequiresAuthentication(AccessDeniedMessage = "You must be logged in to view the list of events")]
        public ActionResult Index() {
            List<Event> events = ES.AllEvents();
            List<EventDisplayViewModel> model = Mapper.Map<List<Event>, List<EventDisplayViewModel>>(events);

            return View(model);
        }

        [LogErrors(FriendlyErrorMessage = "There was a problem saving your event")]
        public ActionResult Error() {
            throw new Exception("This is a test exception");
        }

        [RequiresAuthentication(AccessDeniedMessage = "You must be logged in to view the details of that event")]
        public ActionResult Details(int id) {

            EventDisplayViewModel Model = Mapper.Map<Event, EventDisplayViewModel>(ES.FindByID(id));
            
            Model.Tweets = TS.FindByEventID(id);
            
            return View(Model);
        }

        [RequiresAuthentication(AccessDeniedMessage = "You must be logged in to view the details of that event")]
        public ActionResult CreateEvent() {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(CreateEventViewModel model) {

            try {
                model.UserID = Guid.Parse(CookieHelpers.GetCookieValue("lc", "uid").ToString());

                Event EPLevent = Mapper.Map<CreateEventViewModel, Event>(model);
                ES.Save(EPLevent);
                this.StoreSuccess("Your Event was created");
            } catch (Exception ex) {
                this.StoreError(string.Format("There was an error: {0}", ex.Message));
            }

            return View();
        }

        public ActionResult EventBySlug(string eventSlug) {
            Event foundEvent = null;
            foreach (var e in db.Events) {
                if (e.Name.CreateUrlSlug() == eventSlug) {
                    foundEvent = e;
                    break;
                }
            }

            return View("Details", Mapper.Map<Event, EventDisplayViewModel>(foundEvent));
        }
    }
}
