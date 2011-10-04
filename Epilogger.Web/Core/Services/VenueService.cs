using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epilogger.Data;

namespace Epilogger.Web {
    public class VenueService : ServiceBase<Venue> {
        protected override string CacheKey {
            get { return "Epilogger.Venues"; }
        }


        //public object Save(Venue entity) {
        //    if (entity. > 0)
        //        return base.GetRepository<Venue>().Update(entity);

        //    return base.GetRepository<Venue>().Add(entity);
        //}
    }
}