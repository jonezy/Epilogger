using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using RichmondDay.Helpers;
using SubSonic.Schema;

namespace Epilogger.Web.Core.Stats
{
    public class TweetGrowth
    {
        private const string CacheKey = "TweetGrowth";
        protected virtual double CacheExpiry
        {
            get { return 60; }
        }

        public List<TweetGrowthStats> GetTweetGrowthStats(DateTime f, DateTime t)
        {

                var ss = new StatsService();
                var data = ss.FindTweetGrowthStats(f, t);

            return data;
        }





    }


    


}