using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;

namespace Epilogger.Web
{
    public class ImageMetaDateService : ServiceBase<ImageMetaDatum>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.ImageMetaData"; }
        }

        public IEnumerable<ImageMetaDatum> FindByEventID(int EventID)
        {
            return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public IEnumerable<ImageMetaDatum> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            return db.ImageMetaData.Where(t => t.EventID == EventID & t.DateTime >= StartDateTimeFilter & t.DateTime <= EndDateTimeFilter);
        }

        public IEnumerable<ImageMetaDatum> FindByImageID(int ImageID)
        {
            return FindByImageID(ImageID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public IEnumerable<ImageMetaDatum> FindByImageID(int ImageID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            return db.ImageMetaData.Where(i => i.ImageID == ImageID & i.DateTime >= StartDateTimeFilter & i.DateTime <= EndDateTimeFilter);
        }


    }
}