using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Epilogger.Web.Models
{
    public class AllTweetsDisplayViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public bool ShowTopTweets { get; set; }

        public int TweetCount { get; set; }
        public int Page { get; set; }
        public int CurrentPageIndex { get; set; }
        
        public IEnumerable<Epilogger.Data.Tweet> Tweets { get; set;}
        
    }
}