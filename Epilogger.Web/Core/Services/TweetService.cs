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

        //Reading on the performance diff of List vs iEnumerable.
        //http://stackoverflow.com/questions/3628425/linq-ienumerable-vs-list-what-to-use-how-do-they-work
        public List<Tweet> FindByEventIDList(int EventID)
        {
            return GetData().Where(t => t.EventID == EventID).ToList();
        }

        //In this case iEnumerable gives us better performance and allows each part of the program that needs to pull limited number of tweets to do so.
        //To list would evaluate now and pull all tweets before filtering is done.
        //If using ToList. Make sure to specify a limit here. I'd say 100.
        public IEnumerable<Tweet> FindByEventID(int EventID)
        {
            return GetData().Where(t => t.EventID == EventID);
        }

        //This gives us the last 50 Tweets in mem, this can then be filtered with later with no return to the DB
        public List<Tweet> FindByEventIDToList(int EventID)
        {
            return GetData().Where(t => t.EventID == EventID).OrderByDescending(t => t.CreatedDate).Take(50).ToList();
        }

        //public IEnumerable<AzureTweetModel> FindByEventIDAzure(int EventID)
        //{
        //    var atweet = this.CreateQuery<AzureTweetModel>().Where(t => t.PartitionKey == EventID).AsEnumrable;
        //    return atweet;
        //}

    }
}