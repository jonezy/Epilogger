using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;
using SubSonic.Schema;
using System.Data;

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

        public Venue FindByID(int ID)
        {
            return GetData(v => v.ID == ID).FirstOrDefault();
        }

        public bool DoesVenueExist(int ID)
        {
            int v = db.Venues.Where(d => d.ID == ID).Count();
            if (v == 0)
            { 
                return false; 
            }
            else
            { 
                return true; 
            }
        }

        public bool DoesVenueExist(string FourSquareVenueID)
        {
            int v = db.Venues.Where(d => d.FoursquareVenueID == FourSquareVenueID).Count();
            if (v == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



    }
}