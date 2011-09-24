using System.Collections.Generic;
using System.Linq;

using Epilogger.Data;

namespace Epilogger.Web.Core.Stats {
    public class TopTweetersStats {
        public TopTweetersStats() {
            // select the # of tweets per user and then sort by total
            // then calculate % of the top ten
        }

        public List<Tweeter> Calculate(IEnumerable<Tweet> tweets) {
            List<Tweeter> topTweeters = new List<Tweeter>();
            var results = (from t in tweets
                           group t by t.FromUserScreenName into grouping
                           orderby grouping.Count() descending 
                           select new Tweeter { Name = grouping.FirstOrDefault().FromUserScreenName, Total = grouping.Count() }).ToList();


            for (int i = 0; i < 10; i++) {
                topTweeters.Add(results[i]);
            }

            return topTweeters;
        }

    }

    public class Tweeter {
        public string Name { get; set; }
        public int Total { get; set; }
    }
}