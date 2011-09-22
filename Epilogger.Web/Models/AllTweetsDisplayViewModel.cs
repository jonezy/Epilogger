using System.Collections.Generic;


namespace Epilogger.Web.Models {
    public class AllTweetsDisplayViewModel {
        public string ID { get; set; }

        public string Name { get; set; }

        public bool ShowTopTweets { get; set; }

        public int TweetCount { get; set; }
        public int Page { get; set; }
        public int CurrentPageIndex { get; set; }

        public int TimeZoneOffSet { get; set; }
        

        public IEnumerable<TweetDisplayViewModel> Tweets { get; set; }

    }
}