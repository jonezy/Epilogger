﻿using System.Collections.Generic;
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


        public int FindCheckInCountByEventID(int eventID, DateTime f, DateTime T)
        {
            return db.CheckIns.Count(t => t.EventID == eventID && t.CheckInDateTime >= f && t.CheckInDateTime <= T);
        }

        public IEnumerable<CheckIn> FindByEventIDOrderDescTake16(int eventID, DateTime F, DateTime T)
        {
            return db.CheckIns.Where(t => t.EventID == eventID && t.CheckInDateTime >= F && t.CheckInDateTime <= T).OrderByDescending(t => t.CheckInDateTime).Take(16);
        }

        public IEnumerable<CheckIn> FindByEventIDOrderDescTakeAll(int eventID, DateTime F, DateTime T)
        {
            return db.CheckIns.Where(t => t.EventID == eventID && t.CheckInDateTime >= F && t.CheckInDateTime <= T).OrderByDescending(t => t.CheckInDateTime);
        }

        public IEnumerable<CheckIn> FindByEventIDPaged(int eventID, int currentPage, int recordsPerPage, DateTime F, DateTime T)
        {
            var checkins = db.CheckIns.Where(e => e.EventID == eventID && e.CheckInDateTime >= F && e.CheckInDateTime <= T);
            return checkins.Skip(currentPage * recordsPerPage).Take(recordsPerPage).OrderByDescending(c => c.CheckInDateTime);
        }

        public IEnumerable<CheckIn> FindByEventID(int eventID)
        {
            return FindByEventID(eventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public IEnumerable<CheckIn> FindByEventID(int eventID, DateTime startDateTimeFilter, DateTime endDateTimeFilter)
        {
            return db.CheckIns.Where(t => t.EventID == eventID & t.CheckInDateTime >= startDateTimeFilter & t.CheckInDateTime <= endDateTimeFilter);
        }
        
        public int Count() {
            return base.db.CheckIns.Count();
        }
    }
}