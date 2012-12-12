using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epilogger.Data;
using Epilogger.Web.Core.Stats;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Epilogger.Web.Models
{
    public class LiveModeViewModel
    {
        public string EventSlug { get; set; }
        public int EventId { get; set; }
        public LiveModeCustomSetting CustomSettings { get; set; }
        
        public Core.Stats.WidgetTotal EpiloggerCounts { get; set; }
        public List<Tweeter> TopTweeters { get; set; }

        public string Hashtag { get; set; }

        public List<Tweet> Tweets { get; set; }
        public List<Image> Images { get; set; }



    }
}