using System;
using System.Collections.Generic;
using System.Linq;

using Epilogger.Data;

namespace Epilogger.Web.Core.Stats {
    public class TopTweetersStats {


        public List<Tweeter> Calculate(IEnumerable<Tweeter> tweeters) {
            List<Tweeter> topTweeters = new List<Tweeter>();
            // first loop through all the results and get a total
            float totalTweets = 0;
            foreach (var item in tweeters){
                totalTweets = totalTweets + item.Total;
            }

            //calculate percent of the above total
            int count = 1;
            foreach (var item in tweeters) {
                float userTotal = item.Total;
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