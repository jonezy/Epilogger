using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;

namespace Epilogger.Web
{
    public class ImageService : ServiceBase<Tweet>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.Images"; }
        }

        //public List<Tweet> AllImages()
        //{
        //    return base.GetData();
        //}

        public IEnumerable<Image> FindByEventID(int EventID)
        {
            return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public IEnumerable<Image> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            EpiloggerDB db = new EpiloggerDB();
            return db.Images.Where(t => t.EventID == EventID & t.DateTime >= StartDateTimeFilter & t.DateTime <= EndDateTimeFilter).AsEnumerable();
        }

    }
}