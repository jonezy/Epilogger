using System.Collections.Generic;
using System;
using Epilogger.Web.Core.Stats;

namespace Epilogger.Web.Models {
    public class AllStatsViewModel {
        public string ID { get; set; }
        public string Name { get; set; }
        public string SearchTerms { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime CollectionStartDateTime { get; set; }
        public DateTime CollectionEndDateTime { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public DateTime MyUTCNow { get; set; }

        public int TweetCount { get; set; }
        public int ImageCount { get; set; }
        public int ExternalLinkCount { get; set; }
        public List<Tweeter> TopTweeters { get; set; }

        public List<TopImageAndTweet> TopImages { get; set; }
        public List<CheckinDisplayViewModel> AllCheckIns { get; set; }

        
        public int PageSize { get { return 12; } }
        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }

        //public IEnumerable<Epilogger.Data.URL> Links { get; set; }

    }
}