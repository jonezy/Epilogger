using System.Collections.Generic;
using Epilogger.Web.Core.Stats;

namespace Epilogger.Web.Models {
    public class AllTweetsDisplayViewModel {
        public int ID { get; set; }
        public string EventSlug { get; set; }
        public string Name { get; set; }
        public bool ShowTopTweets { get; set; }
        public int TweetCount { get; set; }
        public int Page { get; set; }
        public int CurrentPageIndex { get; set; }
        public int TimeZoneOffSet { get; set; }
        public int UniqueTweeterCount { get; set; }
        public IEnumerable<TweetDisplayViewModel> Tweets { get; set; }
        public List<Tweeter> TopTweeters { get; set; }
        public bool CanDelete { get; set; }

        public EventToolbarViewModel ToolbarViewModel { get; set; }
    }
}