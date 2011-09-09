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

        public List<ImageMetaDatum> FindByEventID(int EventID)
        {
            return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public List<ImageMetaDatum> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            return GetData(t => t.EventID == EventID & t.DateTime >= StartDateTimeFilter & t.DateTime <= EndDateTimeFilter);
        }

        public List<ImageMetaDatum> FindByImageID(int ImageID)
        {
            return FindByImageID(ImageID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public List<ImageMetaDatum> FindByImageID(int ImageID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            return GetData(i => i.ImageID == ImageID & i.DateTime >= StartDateTimeFilter & i.DateTime <= EndDateTimeFilter);
        }


    }
}