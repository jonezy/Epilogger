using Epilogger.Data;

namespace Epilogger.Web {
    public class VenueService : ServiceBase<Venue> {
        protected override string CacheKey {
            get { return "Epilogger.Venues"; }
        }


        public object Save(Venue entity) {
            if (entity.ID > 0)
                return base.GetRepository<Venue>().Update(entity);

            return base.GetRepository<Venue>().Add(entity);
        }
    }
}