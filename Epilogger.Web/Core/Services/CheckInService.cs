using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;

namespace Epilogger.Web
{
    public class CheckInService : ServiceBase<CheckIn>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.CheckIn"; }
        }

        public List<CheckIn> FindByEventID(int EventID)
        {
            return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public List<CheckIn> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            return GetData(t => t.EventID == EventID & t.CheckInDateTime >= StartDateTimeFilter & t.CheckInDateTime <= EndDateTimeFilter);
        }

    }
}