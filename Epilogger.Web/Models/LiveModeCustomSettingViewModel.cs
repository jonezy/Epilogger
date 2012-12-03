using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Epilogger.Data;
using Twitterizer;

namespace Epilogger.Web.Models {
    public class LiveModeCustomSettingViewModel {
        public int EventId { get; set; }

        [StringLength(7, ErrorMessage = "must be 7 characters in length (#FFF000)")]
        [DisplayName("Background colour")]
        public string Background{ get; set; }

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
        public string SponsorLogo{ get; set; }
    }
}