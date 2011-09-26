﻿using System.Collections.Generic;

using System;
using System.Linq;

using Epilogger.Data;

namespace Epilogger.Web {
    public class EventService : ServiceBase<Event> {
        protected override string CacheKey {
            get { return "Epilogger.Web.Events"; }
        }

        public List<Event> AllEvents() {
            return base.GetData();
        }

        public List<Event> UpcomingEvents()
        {
            return GetData(e => e.StartDateTime > DateTime.UtcNow);
            //db.Events.Where(e => e.StartDateTime > DateTime.UtcNow).ToList();
        }

        public List<Event> PastEvents()
        {
            return db.Events.Where(e => e.EndDateTime < DateTime.UtcNow).ToList();
        }

        public List<Event> GoingOnNowEvents()
        {
            return db.Events.Where(e => e.StartDateTime <= DateTime.UtcNow && e.EndDateTime >= DateTime.UtcNow).ToList();
        }

        

        public Event GetRandomEvent()
        {
            int lowerbound = 1;
            int upperbound = this.GetHighestEventID();

            System.Random r = new System.Random();
            int RandomNumber = r.Next(lowerbound, upperbound);
            
            do
            {
                Event EE = this.FindByID(RandomNumber);
                if (EE == null)
                {
                    RandomNumber = Helpers.RandomInt(r, lowerbound, upperbound);
                }
                else
                {
                    return EE;
                }
            } while (true);

        }

        public int GetHighestEventID()
        {
            return db.Events.OrderByDescending(e => e.ID).First().ID;
        }



        public IEnumerable<Event> GetHottestEvents(int ItemsToReturn)
        {
            return db.Events.Where(e => e.StartDateTime > DateTime.UtcNow.AddMonths(-1) && e.StartDateTime < DateTime.UtcNow).OrderByDescending(e => e.Tweets.Where(f => f.EventID == e.ID).Count() + (e.Images.Where(f => f.EventID == e.ID).Count() * 4)).Take(ItemsToReturn);
        }


        public Event FindByID(int EventID)
        {
            return GetData().Where(e => e.ID == EventID).FirstOrDefault();
        }

        public List<Event> FindByUserID(Guid userID) {
            return GetData().Where(e => e.UserID == userID).OrderByDescending(e=>e.StartDateTime).ToList();
        }


        public List<UserRatesEvent> FindEventRatingsByID(int id, DateTime F, DateTime T)
        {
            return db.UserRatesEvents.Where(r => r.EventID == id && r.RatingDateTime >= F && r.RatingDateTime <= T).ToList();
        }


        public object Save(Event entity)
        {
            if (entity.ID > 0)
                return base.GetRepository<Event>().Update(entity);

            return base.GetRepository<Event>().Add(entity);
        }

    }
}