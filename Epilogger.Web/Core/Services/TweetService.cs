using System.Collections.Generic;
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
            return base.GetData();
        }


        public EpiloggerDB Thedb()
        {
            return db;
        }



        //Less greedy data functions
        public int FindTweetCountByEventID(int EventID, DateTime F, DateTime T)
        {
            return db.Tweets.Where(t => t.EventID == EventID && t.CreatedDate >= F && t.CreatedDate <= T).Count();
        }

        public int FindUniqueTweetCountByEventID(int EventID, DateTime F, DateTime T)
        {
            return db.Tweets.Where(t => t.EventID == EventID && t.CreatedDate >= F && t.CreatedDate <= T).Select(t => t.FromUserScreenName).Distinct().Count();
        }


        public IEnumerable<Tweet> FindByEventIDOrderDescTake6(int EventID, DateTime F, DateTime T)
        {
            return db.Tweets.Where(t => t.EventID == EventID && t.CreatedDate >= F && t.CreatedDate <= T).OrderByDescending(t => t.CreatedDate).Take(6);
        }





        //Slower funcitons
        public IEnumerable<Tweet> FindByEventID(int EventID)
        {
            //return FindByEventID(EventID, DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2200-12-31 00:00:00"));
            return db.Tweets.Where(t => t.EventID == EventID);
        }

        public IEnumerable<Tweet> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            return db.Tweets.Where(t => t.EventID == EventID & t.CreatedDate >= StartDateTimeFilter & t.CreatedDate <= EndDateTimeFilter);
        }

        public List<Tweet> FindByUserScreenName(string fromUserScreenName, int page, int recordsPerPage) {
            DateTime startDate = DateTime.Parse("2000-01-01 00:00:00");
            DateTime endDate = DateTime.Parse("2200-12-31 00:00:00");

            return GetData(t => t.FromUserScreenName == fromUserScreenName && t.CreatedDate >= startDate && t.CreatedDate <= endDate)
                    .Skip(page * recordsPerPage)
                    .Take(recordsPerPage)
                    .ToList();
        }

        public IEnumerable<Tweet> FindByUserScreenName(string fromUserScreenName) {
            DateTime startDate = DateTime.Parse("2000-01-01 00:00:00");
            DateTime endDate = DateTime.Parse("2200-12-31 00:00:00");

            //return GetData(t => t.FromUserScreenName == fromUserScreenName && t.CreatedDate >= startDate && t.CreatedDate <= endDate);
            return db.Tweets.Where(t => t.FromUserScreenName == fromUserScreenName && t.CreatedDate >= startDate && t.CreatedDate <= endDate);
        }


        public IEnumerable<Tweet> FindByImageID(int ImageID)
        {
            return FindByImageID(ImageID, DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2200-12-31 00:00:00"));
        }

        public IEnumerable<Tweet> FindByImageID(int ImageID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            ImageMetaDateService IMDS = new ImageMetaDateService();
            IEnumerable<ImageMetaDatum> IM = IMDS.FindByImageID(ImageID);

            return from TW in db.Tweets
                   join I in IM on TW.TwitterID equals I.TwitterID
                   orderby TW.CreatedDate
                   select TW;
        }

        public IEnumerable<Tweet> GetPagedTweets(int EventID, System.Nullable<int> page, int TweetsPerPage, DateTime F, DateTime T)
        {

            int skipAmount = page.HasValue ? page.Value - 1 : 0;

            var tweets = (from t in db.Tweets
                          where t.EventID == EventID && t.CreatedDate >= F && t.CreatedDate <= T
                          orderby t.CreatedDate descending
                          select t).Skip(skipAmount * TweetsPerPage).Take(TweetsPerPage);


            return tweets;

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