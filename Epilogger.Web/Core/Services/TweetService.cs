using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;

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

        public List<Tweet> FindByEventID(int EventID)
        {
            return GetData().Where(t => t.EventID == EventID).ToList();
        }
        
    }
}