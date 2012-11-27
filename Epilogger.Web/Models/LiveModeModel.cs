using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Epilogger.Data;
using Twitterizer;

namespace Epilogger.Web.Models {
    public class LiveModeModel {
        public int ID{ get; set; }
        public int EventId { get; set; }

        [DisplayName("Background colour")]
        public string Background{ get; set; }

        [DisplayName("Footer text colour")]
        public string FooterTextColor { get; set; }

        [DisplayName("Twitter username colour")]
        public string TwitterUserNameColor { get; set; }

        [DisplayName("Logo")]
        public string CompanyLogo { get; set; }

        [DisplayName("Sponsor Logo")]
        public string SponsorLogo{ get; set; }
    }
}