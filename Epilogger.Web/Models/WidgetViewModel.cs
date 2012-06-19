using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Models
{
    public class WidgetViewModel
    {
        public int width { get; set; }
        public int height { get; set; }

        public int ID { get; set; }
        public string EventSlug { get; set; }

        public int TweetCount { get; set; }
        public int ImageCount { get; set; }
        public int CheckInCount { get; set; }

        public Core.Stats.WidgetTotal EpiloggerCounts { get; set; }
        public IEnumerable<Epilogger.Data.Tweet> Tweets { get; set; }
        public IEnumerable<Epilogger.Data.Image> Images { get; set; }
        public IEnumerable<Epilogger.Data.CheckIn> CheckIns { get; set; }

        public string ButtonBackgroundColor { get; set; }
        public string ButtonTextColor { get; set; }

    }
}