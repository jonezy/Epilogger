﻿using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;


namespace Epilogger.Web
{
    public class TweetService : ServiceBase<Tweet>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.Tweets"; }
        }

        public List<Tweet> AllTweets()
        {
            return base.GetData(t => t.Deleted==false);
        }


        public EpiloggerDB Thedb()
        {
            return db;
        }



        //Less greedy data functions
        public int FindTweetCountByEventID(int eventID, DateTime f, DateTime T)
        {
            return db.Tweets.Count(t => t.EventID == eventID && t.CreatedDate >= f && t.CreatedDate <= T && (t.Deleted == null || t.Deleted == false));
        }

        public int FindUniqueTweetCountByEventID(int eventID, DateTime F, DateTime T)
        {
            return db.Tweets.Where(t => t.EventID == eventID && t.CreatedDate >= F && t.CreatedDate <= T && (t.Deleted == null || t.Deleted == false)).Select(t => t.FromUserScreenName).Distinct().Count();
        }


        public IEnumerable<Tweet> FindByEventIDOrderDescTake6(int eventID, DateTime F, DateTime T)
        {
            return db.Tweets.Where(t => t.EventID == eventID && t.CreatedDate >= F && t.CreatedDate <= T && (t.Deleted == null || t.Deleted == false)).OrderByDescending(t => t.CreatedDate).Take(6);
        }

        public IEnumerable<Tweet> FindByEventIDOrderDescTakeX(int eventID, int itemsToReturn, DateTime F, DateTime T)
        {
            return db.Tweets.Where(t => t.EventID == eventID && t.CreatedDate >= F && t.CreatedDate <= T && (t.Deleted == null || t.Deleted == false)).OrderByDescending(t => t.CreatedDate).Take(itemsToReturn);
        }

        public IEnumerable<Epilogger.Web.Core.Stats.Tweeter> GetTop10TweetersByEventID(int eventID, DateTime F, DateTime T)
        {
            return db.GetTop10TweetersByEventID(eventID, F, T).ExecuteTypedList<Epilogger.Web.Core.Stats.Tweeter>();
        }




        //Slower funcitons
        public IEnumerable<Tweet> FindByEventID(int eventID)
        {
            //return FindByEventID(EventID, DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2200-12-31 00:00:00"));
            return db.Tweets.Where(t => t.EventID == eventID && (t.Deleted == null || t.Deleted == false));
        }

        public IEnumerable<Tweet> FindByEventID(int eventID, DateTime startDateTimeFilter, DateTime endDateTimeFilter)
        {
            return db.Tweets.Where(t => t.EventID == eventID & t.CreatedDate >= startDateTimeFilter & t.CreatedDate <= endDateTimeFilter && (t.Deleted == null || t.Deleted == false));
        }

        public List<Tweet> FindByUserScreenName(string fromUserScreenName, int page, int recordsPerPage) {
            DateTime startDate = DateTime.Parse("2000-01-01 00:00:00");
            DateTime endDate = DateTime.Parse("2200-12-31 00:00:00");

            return GetData(t => t.FromUserScreenName == fromUserScreenName && t.CreatedDate >= startDate && t.CreatedDate <= endDate && (t.Deleted == null || t.Deleted == false))
                    .Skip(page * recordsPerPage)
                    .Take(recordsPerPage)
                    .ToList();
        }

        public IEnumerable<Tweet> FindByUserScreenName(string fromUserScreenName) {
            DateTime startDate = DateTime.Parse("2000-01-01 00:00:00");
            DateTime endDate = DateTime.Parse("2200-12-31 00:00:00");

            //return GetData(t => t.FromUserScreenName == fromUserScreenName && t.CreatedDate >= startDate && t.CreatedDate <= endDate);
            return db.Tweets.Where(t => t.FromUserScreenName == fromUserScreenName && t.CreatedDate >= startDate && t.CreatedDate <= endDate && (t.Deleted == null || t.Deleted == false));
        }


        public IEnumerable<Tweet> FindByImageID(int imageID, int eventId)
        {
            return FindByImageID(imageID, DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2200-12-31 00:00:00"), eventId);
        }

        public IEnumerable<Tweet> FindByImageID(int imageID, DateTime startDateTimeFilter, DateTime endDateTimeFilter, int eventId)
        {
            var imds = new ImageMetaDateService();
            IEnumerable<ImageMetaDatum> im = imds.FindByImageID(imageID);

            return from tw in db.Tweets
                   join I in im on tw.TwitterID equals I.TwitterID
                   where tw.EventID == eventId //&& tw.Deleted == null || tw.Deleted == false
                   orderby tw.CreatedDate
                   select tw;
        }





        public int FindCountByImageID(int imageID, int eventId)
        {
            return FindCountByImageID(imageID, DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2200-12-31 00:00:00"), eventId);
        }
        public int FindCountByImageID(int imageID, DateTime startDateTimeFilter, DateTime endDateTimeFilter, int eventId)
        {
            var imds = new ImageMetaDateService();
            IEnumerable<ImageMetaDatum> im = imds.FindByImageID(imageID);

            return (from tw in db.Tweets
                   join I in im on tw.TwitterID equals I.TwitterID
                   where tw.EventID == eventId //&& tw.Deleted == null || tw.Deleted == false
                   orderby tw.CreatedDate
                   select tw).Count();
        }




        public IEnumerable<Tweet> FindByImageIDPaged(int imageID, int eventId, int? page, int itemsPerPage)
        {
            return FindByImageIDPaged(imageID, DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2200-12-31 00:00:00"), eventId, page, itemsPerPage);
        }

        public IEnumerable<Tweet> FindByImageIDPaged(int imageID, DateTime startDateTimeFilter, DateTime endDateTimeFilter, int eventId, int? page, int itemsPerPage)
        {

            var skipAmount = page.HasValue ? page.Value - 1 : 0;
            
            var imds = new ImageMetaDateService();
            var im = imds.FindByImageID(imageID);

            var tweets = (from tw in db.Tweets
                   join I in im on tw.TwitterID equals I.TwitterID
                   where tw.EventID == eventId
                   orderby tw.CreatedDate
                          select tw).Skip(skipAmount * itemsPerPage).Take(itemsPerPage);

            return tweets;

        }



        public IEnumerable<Tweet> GetPagedTweets(int eventID, System.Nullable<int> page, int tweetsPerPage, DateTime F, DateTime T)
        {

            int skipAmount = page.HasValue ? page.Value - 1 : 0;

            var tweets = (from t in db.Tweets
                          where t.EventID == eventID && t.CreatedDate >= F && t.CreatedDate <= T && (t.Deleted == null || t.Deleted == false)
                          orderby t.CreatedDate descending
                          select t).Skip(skipAmount * tweetsPerPage).Take(tweetsPerPage);


            return tweets;
        }

        public Tweet FindByTwitterID(long? twitterID)
        {
            return db.Tweets.FirstOrDefault(t => t.TwitterID == twitterID && (t.Deleted == null || t.Deleted == false));
        }

        public int Count()
        {
            return db.Tweets.Count(t => t.Deleted == null || t.Deleted == false);
        }

        public bool MarkTweetAsDeleted(long tweetID)
        {
            try
            {
                var firstOrDefault = db.Tweets.FirstOrDefault(t => t.ID == tweetID);
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

        public object Save(Tweet entity)
        {
            if (entity.ID > 0)
                return base.GetRepository<Tweet>().Update(entity);

            return base.GetRepository<Tweet>().Add(entity);
        }


        ////Reading on the performance diff of List vs iEnumerable.
        ////http://stackoverflow.com/questions/3628425/linq-ienumerable-vs-list-what-to-use-how-do-they-work
        //public List<Tweet> FindByEventIDList(int EventID)
        //{
        //    return GetData().Where(t => t.EventID == EventID).ToList();
        //}

        ////In this case iEnumerable gives us better performance and allows each part of the program that needs to pull limited number of tweets to do so.
        ////To list would evaluate now and pull all tweets before filtering is done.
        ////If using ToList. Make sure to specify a limit here. I'd say 100.
        //public IEnumerable<Tweet> FindByEventID(int EventID)
        //{
        //    return GetData().Where(t => t.EventID == EventID);
        //}

        ////This gives us the last 50 Tweets in mem, this can then be filtered with later with no return to the DB
        //public List<Tweet> FindByEventIDToList(int EventID)
        //{
        //    return GetData().Where(t => t.EventID == EventID).OrderByDescending(t => t.CreatedDate).Take(50).ToList();
        //}

        ////public IEnumerable<AzureTweetModel> FindByEventIDAzure(int EventID)
        ////{
        ////    var atweet = this.CreateQuery<AzureTweetModel>().Where(t => t.PartitionKey == EventID).AsEnumrable;
        ////    return atweet;
        ////}


        //public List<Tweet> FindByEventID(int EventID)
        //{
        //    return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"), 1000);
        //}

        //public List<Tweet> FindByEventID(int EventID, int NumberToReturn)
        //{
        //    return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"), NumberToReturn);
        //}

        //public List<Tweet> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter, int NumberToReturn)
        //{
        //    EpiloggerDB db = new EpiloggerDB();
        //    return db.Tweets.Where(t => t.EventID == EventID & t.CreatedDate >= StartDateTimeFilter & t.CreatedDate <= EndDateTimeFilter).Take(NumberToReturn).ToList();
        //}

        
    }
}