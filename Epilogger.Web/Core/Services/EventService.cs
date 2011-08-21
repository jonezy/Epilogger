using System.Collections.Generic;

using Epilogger.Data;

namespace Epilogger.Web {
    public class EventService : ServiceBase<Event> {
        protected override string CacheKey {
            get { return "Epilogger.Web.Events"; }
        }

        public List<Event> AllEvents() {
            return base.GetData();
        }

        public object Save(Event entity)
        {
            if (entity.ID > 0)
                return base.GetRepository<Event>(db).Update(entity);

            return base.GetRepository<Event>(db).Add(entity);
        }

    }
}