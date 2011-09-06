using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;

namespace Epilogger.Web
{
    public class ExternalLinkService : ServiceBase<Tweet>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.ExternalLink"; }
        }

        public IEnumerable<URL> FindByEventID(int EventID)
        {
            return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public IEnumerable<URL> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            EpiloggerDB db = new EpiloggerDB();
            //return db.URLS.Where(t => t.EventID == EventID & t. >= StartDateTimeFilter & t.CheckInDateTime <= EndDateTimeFilter).AsEnumerable();
            return db.URLS.Where(t => t.EventID == EventID).AsEnumerable();
            //TODO - Add dates to the Links in the DB.
        }

    }
}