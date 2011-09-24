using System;
using System.Collections.Generic;
using System.Linq;

using Epilogger.Data;

namespace Epilogger.Web.Core.Stats {
    public class TopTweetersStats {
        public List<Tweeter> Calculate(IEnumerable<Tweet> tweets) {
            List<Tweeter> topTweeters = new List<Tweeter>();
            //var results = (from t in tweets
            //               group t by t.FromUserScreenName into grouping
            //               orderby grouping.Count() descending 
            //               select new Tweeter { 
            //                   Name = grouping.FirstOrDefault().FromUserScreenName,
            //                   Picture = grouping.FirstOrDefault().ProfileImageURL,
            //                   Total = grouping.Count()
            //               }).ToList();


            var results = (from t in tweets
                           group t by t.FromUserScreenName into grouping
                           orderby grouping.Count() descending
                           select new Tweeter
                           {
                               Name = grouping.FirstOrDefault().FromUserScreenName,
                               Picture = grouping.FirstOrDefault().ProfileImageURL,
                               Total = grouping.Count()
                           });

            int count = 1;
            foreach (var item in results) {
                float userTotal = item.Total;
                float totalTweets = tweets.Count();

                item.PercentOfTotal = (int)Math.Round(((userTotal / totalTweets) * 100));

                topTweeters.Add(item);

                if (count == 10) break;
                count++;
            }

            return topTweeters;
        }
    }

    public class Tweeter {
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Total { get; set; }
        public int PercentOfTotal { get; set; }
    }
}