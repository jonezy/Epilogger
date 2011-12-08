using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;
using SubSonic.Repository;

namespace Epilogger.Web
{
    public class CheckInService : ServiceBase<CheckIn>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.CheckIn"; }
        }


        public int FindCheckInCountByEventID(int EventID, DateTime F, DateTime T)
        {
            return db.CheckIns.Where(t => t.EventID == EventID && t.CheckInDateTime >= F && t.CheckInDateTime <= T).Count();
        }

        public IEnumerable<CheckIn> FindByEventIDOrderDescTake5(int EventID, DateTime F, DateTime T)
        {
            return db.CheckIns.Where(t => t.EventID == EventID && t.CheckInDateTime >= F && t.CheckInDateTime <= T).OrderByDescending(t => t.CheckInDateTime).Take(5);
        }

        public IEnumerable<CheckIn> FindByEventIDPaged(int EventID, int currentPage, int recordsPerPage, DateTime F, DateTime T)
        {
            var checkins = db.CheckIns.Where(e => e.EventID == EventID && e.CheckInDateTime >= F && e.CheckInDateTime <= T);
            return checkins.Skip(currentPage * recordsPerPage).Take(recordsPerPage).OrderByDescending(c => c.CheckInDateTime);
        }

        public IEnumerable<CheckIn> FindByEventID(int EventID)
        {
            return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public IEnumerable<CheckIn> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            return db.CheckIns.Where(t => t.EventID == EventID & t.CheckInDateTime >= StartDateTimeFilter & t.CheckInDateTime <= EndDateTimeFilter);
        }
        
        public int Count() {
            return base.db.CheckIns.Count();
        }
    }
}