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

        public IEnumerable<ImageMetaDatum> FindByEventID(int eventID)
        {
            return FindByEventID(eventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public IEnumerable<ImageMetaDatum> FindByEventID(int eventID, DateTime startDateTimeFilter, DateTime endDateTimeFilter)
        {
            return db.ImageMetaData.Where(t => t.EventID == eventID & t.DateTime >= startDateTimeFilter & t.DateTime <= endDateTimeFilter);
        }

        public IEnumerable<ImageMetaDatum> FindByImageID(int imageID)
        {
            return FindByImageID(imageID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public IEnumerable<ImageMetaDatum> FindByImageID(int imageID, DateTime startDateTimeFilter, DateTime endDateTimeFilter)
        {
            return db.ImageMetaData.Where(i => i.ImageID == imageID & i.DateTime >= startDateTimeFilter & i.DateTime <= endDateTimeFilter);
        }


    }
}