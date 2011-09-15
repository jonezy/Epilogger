using System.Collections.Generic;

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

        public Event FindByID(int EventID)
        {
            return GetData().Where(e => e.ID == EventID).FirstOrDefault();
        }

        public List<Event> FindByUserID(Guid userID) {
            return GetData().Where(e => e.UserID == userID).OrderByDescending(e=>e.StartDateTime).ToList();
        }


        public object Save(Event entity)
        {
            if (entity.ID > 0)
                return base.GetRepository<Event>().Update(entity);

            return base.GetRepository<Event>().Add(entity);
        }

    }
}