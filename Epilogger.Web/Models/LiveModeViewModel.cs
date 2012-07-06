using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epilogger.Data;
using Epilogger.Web.Core.Stats;

namespace Epilogger.Web.Models
{
    public class LiveModeViewModel
    {
        public int EventId { get; set; }

        public Core.Stats.WidgetTotal EpiloggerCounts { get; set; }
        public List<Tweeter> TopTweeters { get; set; }

        public string Hashtag { get; set; }

        public List<Tweet> Tweets { get; set; }
        public List<Image> Images { get; set; }
    }
}