using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;

namespace Epilogger.Web
{
    public class ImageService : ServiceBase<Image>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.Images"; }
        }

        public int FindImageCountByEventID(int EventID, DateTime F, DateTime T)
        {
            return db.Images.Where(t => t.EventID == EventID && t.DateTime >= F && t.DateTime <= T).Count();
        }

        public IEnumerable<Image> FindByEventIDOrderDescTake9(int EventID, DateTime F, DateTime T)
        {
            return db.Images.Where(t => t.EventID == EventID && t.DateTime >= F && t.DateTime <= T).OrderByDescending(t => t.DateTime).Take(9);
        }

        public List<Image> GetRandomImagesByEventID(int EventID, int NumberToGet)
        {
            return db.GetRandomImagesByEventID(EventID, NumberToGet).ExecuteTypedList<Image>();
        }








        public IEnumerable<Image> FindByEventID(int EventID)
        {
            return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public IEnumerable<Image> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            return db.Images.Where(t => t.EventID == EventID & t.DateTime >= StartDateTimeFilter & t.DateTime <= EndDateTimeFilter);
        }

        public Image FindByID(int ID)
        {
            return GetData(t => t.ID == ID).FirstOrDefault();
        }


        public IEnumerable<Image> GetPagedPhotos(int EventID, System.Nullable<int> page, int PhotosPerPage, DateTime F, DateTime T)
        {

            int skipAmount = page.HasValue ? page.Value - 1 : 0;

            var photos = (from t in db.Images
                          where t.EventID == EventID && t.DateTime >= F && t.DateTime <= T
                          orderby t.DateTime descending
                          select t).Skip(skipAmount * PhotosPerPage).Take(PhotosPerPage);

            return photos;

        }
        
    }
}