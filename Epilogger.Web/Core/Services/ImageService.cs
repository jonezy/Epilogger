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

        public List<Image> GetTopPhotosByEventID(int EventID, int RecordsToReturn, DateTime FromDateTime, DateTime ToDateTime)
        {
            List<TopPhotos> TPs = db.GetTopPhotosByEventID(EventID, RecordsToReturn, FromDateTime, ToDateTime).ExecuteTypedList<TopPhotos>();

            List<Image> TheTopPhotos = new List<Image>();
            foreach (TopPhotos TP in TPs)
            {
                Image TheImage = new Image();
                TheImage = FindByID(TP.ImageID);
                TheTopPhotos.Add(TheImage);
            }

            return TheTopPhotos;
        }

        public List<TopImageAndTweet> GetTopPhotosAndTweetByEventID(int EventID, int RecordsToReturn, DateTime FromDateTime, DateTime ToDateTime)
        {
            List<TopPhotos> TPs = db.GetTopPhotosByEventID(EventID, RecordsToReturn, FromDateTime, ToDateTime).ExecuteTypedList<TopPhotos>();

            TweetService TS = new TweetService();
            
            List<TopImageAndTweet> TheTopPhotos = new List<TopImageAndTweet>();
            foreach (TopPhotos TP in TPs)
            {
                TopImageAndTweet TheImage = new TopImageAndTweet();
                TheImage.Image = FindByID(TP.ImageID);
                TheImage.Tweet = TS.FindByTwitterID(TheImage.Image.ImageMetaData.FirstOrDefault().TwitterID);
                TheTopPhotos.Add(TheImage);
            }

            return TheTopPhotos;
        }

        //TopImageAndTweet




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

        public int Count() {
            return base.db.Images.Count();
        }
    }
}