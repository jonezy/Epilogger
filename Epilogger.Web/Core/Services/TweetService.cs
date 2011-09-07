using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;
using Microsoft.WindowsAzure.StorageClient;

enum SortType
    {
        Ascending = 1,
   	    Descending = 2         
    }


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


        public IEnumerable<Tweet> FindByEventID(int EventID)
        {
            return FindByEventID(EventID, DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2200-12-31 00:00:00"));
        }

        public IEnumerable<Tweet> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        {
            EpiloggerDB db = new EpiloggerDB();
            return db.Tweets.Where(t => t.EventID == EventID & t.CreatedDate >= StartDateTimeFilter & t.CreatedDate <= EndDateTimeFilter).AsEnumerable();
        }

        public IEnumerable<Tweet> FindByUserScreenName(string fromUserScreenName) {
            DateTime startDate = DateTime.Parse("2000-01-01 00:00:00");
            DateTime endDate = DateTime.Parse("2200-12-31 00:00:00");

            return GetData(t => t.FromUserScreenName == fromUserScreenName && t.CreatedDate >= startDate && t.CreatedDate <= endDate);
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

    }
}