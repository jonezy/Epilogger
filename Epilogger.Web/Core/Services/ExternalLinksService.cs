using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;

namespace Epilogger.Web
{
    public class ExternalLinkService : ServiceBase<URL>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.ExternalLink"; }
        }


        public IEnumerable<URL> FindByEventIDOrderDescTake3(int EventID, DateTime F, DateTime T)
        {
            return db.URLS.Where(t => t.EventID == EventID && t.DateTime >= F && t.DateTime <= T).OrderByDescending(t => t.DateTime).Take(3);
        }


        //public IEnumerable<URL> FindByEventID(int EventID)
        //{
        //    return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        //}

        public IEnumerable<URL> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            return db.URLS.Where(t => t.EventID == EventID & t.DateTime >= StartDateTimeFilter & t.DateTime <= EndDateTimeFilter);
        }
        public IEnumerable<URL> FindByEventIDPaged(int EventID, int currentPage, int recordsPerPage, DateTime F, DateTime T)
        {
            var Links = db.URLS.Where(e => e.EventID == EventID && e.DateTime >= F && e.DateTime <= T);
            return Links.Skip(currentPage * recordsPerPage).Take(recordsPerPage).OrderBy(c => c.DateTime);
        }

        public int FindCountByEventID(int EventID, DateTime F, DateTime T)
        {
            return db.URLS.Where(t => t.EventID == EventID && t.DateTime >= F && t.DateTime <= T).Count();
        }

    }
}