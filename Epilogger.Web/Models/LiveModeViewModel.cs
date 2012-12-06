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
        public int EventId { get; set; }
        public LiveModeCustomSetting CustomSettings { get; set; }
        
        public Core.Stats.WidgetTotal EpiloggerCounts { get; set; }
        public List<Tweeter> TopTweeters { get; set; }

        public string Hashtag { get; set; }

        public List<Tweet> Tweets { get; set; }
        public List<Image> Images { get; set; }


        [StringLength(7, ErrorMessage = "must be 7 characters in length (#FFF000)")]
        [DisplayName("Background colour")]
        public string Background { get; set; }

        [StringLength(7, ErrorMessage = "must be 7 characters in length (#FFF000)")]
        [DisplayName("Footer text colour")]
        public string FooterTextColor { get; set; }

        [StringLength(7, ErrorMessage = "must be 7 characters in length (#FFF000)")]
        [DisplayName("Twitter username colour")]
        public string TwitterUserNameColour { get; set; }

        [StringLength(7, ErrorMessage = "must be 7 characters in length (#FFF000)")]
        [DisplayName("Link colour")]
        public string LinkColour { get; set; }

        [DisplayName("Logo")]
        public string CompanyLogo { get; set; }

        [DisplayName("Sponsor Logo")]
        public string SponsorLogo { get; set; }
    }
}