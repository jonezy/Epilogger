using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Epilogger.Web.Models
{
    public class CreateEventViewModel
    {
        public Guid UserID { get; set; }
        [Required]
        [DisplayName("TITLE")]
        public String Name { get; set; }



        [DisplayName("SUBTITLE")]
        public String Subtitle { get; set; }


        [DisplayName("CATEGORY")]
        public int Category { get; set; }

        [DisplayName("COST")]
        public int Cost { get; set; }

        [DisplayName("SHORT DESCRIPTION")]
        public int Description { get; set; }


        [Required]
        [DisplayName("Data Collection Start Time")]
        public DateTime CollectionStartDateTime { get; set; }
        
        [DisplayName("Event Start Date and Time")]
        public DateTime StartDateTime { get; set; }
        [Required]
        [DisplayName("EventEnd Date and Time")]
        public DateTime EndDateTime { get; set; }

        [DisplayName("Data Collection End Time")]
        public DateTime CollectionEndDateTime { get; set; }


        [DisplayName("Search Terms")]
        public string SearchTerms { get; set; }


        [DisplayName("Time Zone Offset")]
        public int TimeZoneOffset { get; set; }
    }
}