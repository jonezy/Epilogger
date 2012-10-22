using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;
using SubSonic.Schema;
using System.Data;
using Epilogger.Web.Models;

namespace Epilogger.Web
{

    public class TopPhotos { 
        public int ImageID { get; set; }
        public int countcol { get; set; }
    }

    public class ImageService : ServiceBase<Image>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.Images"; }
        }

        public int FindImageCountByEventID(int EventID, DateTime F, DateTime T)
        {
            return db.Images.Count(t => t.EventID == EventID && t.DateTime >= F && t.DateTime <= T && t.Deleted == false);
        }

        public IEnumerable<Image> FindByEventIDOrderDescTake9(int eventID, DateTime F, DateTime T)
        {
            return db.Images.Where(t => t.EventID == eventID && t.DateTime >= F && t.DateTime <= T && t.Deleted == false).OrderByDescending(t => t.DateTime).Take(9);
        }

        public IEnumerable<Image> FindByEventIdOrderDescTakeX(int eventID, int numberToReturn, DateTime F, DateTime T, bool includeVideos = true)
        {
            if (includeVideos)
                return db.Images.Where(t => t.EventID == eventID && t.DateTime >= F && t.DateTime <= T && t.Deleted == false).OrderByDescending(t => t.DateTime).Take(numberToReturn);

            return db.Images.Where(t => t.EventID == eventID && t.DateTime >= F && t.DateTime <= T && t.Deleted == false && t.MediaType==1).OrderByDescending(t => t.DateTime).Take(numberToReturn);
        }

        public List<Image> GetRandomImagesByEventID(int eventID, int numberToGet)
        {
            return db.GetRandomImagesByEventID(eventID, numberToGet).ExecuteTypedList<Image>();
        }

        public List<Image> GetTopPhotosByEventId(int eventID, int recordsToReturn, DateTime fromDateTime, DateTime toDateTime, bool includeVideos = true)
        {
            var ps = db.GetTopPhotosByEventID(eventID, recordsToReturn, fromDateTime, toDateTime, includeVideos).ExecuteTypedList<TopPhotos>();

            var theTopPhotos = new List<Image>();
            foreach (TopPhotos TP in ps)
            {
                Image TheImage = new Image();
                TheImage = FindByID(TP.ImageID);
                theTopPhotos.Add(TheImage);
            }

            return theTopPhotos;
        }

        public IEnumerable<Image> GetNewestPhotosByEventId(int eventID, int numberToGet, bool includeVideos = true)
        {
            if (includeVideos)
                return db.Images.Where(e => e.EventID == eventID).OrderByDescending(e => e.DateTime).Take(numberToGet).ToList();

            return db.Images.Where(e => e.EventID == eventID && e.MediaType==1).OrderByDescending(e => e.DateTime).Take(numberToGet).ToList();
        }



        public List<TopImageAndTweet> GetTopPhotosAndTweetByEventID(int eventID, int recordsToReturn, DateTime fromDateTime, DateTime toDateTime, bool includeVideos = true)
        {
            List<TopPhotos> ps = db.GetTopPhotosByEventID(eventID, recordsToReturn, fromDateTime, toDateTime, includeVideos).ExecuteTypedList<TopPhotos>();

            var ts = new TweetService();
            
            List<TopImageAndTweet> TheTopPhotos = new List<TopImageAndTweet>();
            foreach (TopPhotos TP in ps)
            {
                TopImageAndTweet TheImage = new TopImageAndTweet();
                TheImage.Image = FindByID(TP.ImageID);
                var firstOrDefault = TheImage.Image.ImageMetaData.FirstOrDefault();
                if (firstOrDefault != null)
                    TheImage.Tweet = ts.FindByTwitterID(firstOrDefault.TwitterID);
                TheTopPhotos.Add(TheImage);
            }

            return TheTopPhotos;
        }

        //TopImageAndTweet




        public IEnumerable<Image> FindByEventID(int eventID)
        {
            return FindByEventID(eventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public IEnumerable<Image> FindByEventID(int eventID, DateTime startDateTimeFilter, DateTime endDateTimeFilter)
        {
            return db.Images.Where(t => t.EventID == eventID & t.DateTime >= startDateTimeFilter & t.DateTime <= endDateTimeFilter && t.Deleted == false);
        }

        public Image FindByID(int id)
        {
            return GetData(t => t.ID == id).FirstOrDefault();
        }


        public IEnumerable<Image> GetPagedPhotos(int eventID, int? page, int photosPerPage, DateTime F, DateTime T, bool includeVideos = true)
        {

            var skipAmount = page.HasValue ? page.Value - 1 : 0;

            IEnumerable<Image> photos;
            if (includeVideos)
            {
                photos = (from t in db.Images
                              where t.EventID == eventID && t.DateTime >= F && t.DateTime <= T && t.Deleted == false
                              orderby t.DateTime descending
                              select t).Skip(skipAmount * photosPerPage).Take(photosPerPage);    
            }
            else
            {
                photos = (from t in db.Images
                              where t.EventID == eventID && t.DateTime >= F && t.DateTime <= T && t.Deleted == false && t.MediaType==1
                              orderby t.DateTime descending
                              select t).Skip(skipAmount * photosPerPage).Take(photosPerPage);
            }
            

            return photos;

        }

        public int Count() {
            return base.db.Images.Count(t => t.Deleted == false);
        }

        public bool MarkImageAsDeleted(long imageID)
        {
            try
            {
                var firstOrDefault = db.Images.FirstOrDefault(t => t.ID == imageID);
                if (firstOrDefault != null)
                    firstOrDefault.Deleted = true;
                Save(firstOrDefault);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public object Save(Image entity)
        {
            if (entity.ID > 0)
                return base.GetRepository<Image>().Update(entity);

            return base.GetRepository<Image>().Add(entity);
        }

        public List<Image> FindForLiveModeAjax(int eventId, DateTime pageLoadTime, int numberToReturn)
        {
            return db.Images.Where(t => t.EventID == eventId && t.DateTime > pageLoadTime.AddSeconds(1)).OrderByDescending(t => t.DateTime).Take(numberToReturn).ToList();
            //return db.Images.Where(t => t.EventID == eventId && t.DateTime > pageLoadTime).OrderByDescending(t => t.DateTime).Take(numberToReturn).ToList();
        }

        public IEnumerable<Image> GetImagesInMemoryBoxPaged(int memBoxId, int page, int count)
        {
            return db.GetPhotosFromMemoryBox(memBoxId, page, count).ExecuteTypedList<Image>();
        }


    }
}