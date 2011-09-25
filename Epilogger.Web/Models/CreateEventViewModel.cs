using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Epilogger.Web.Models {
    public class CreateEventViewModel {
        public int ID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreatedDateTime { get; set; }


        [Required]
        [DisplayName("Name")]
        public String Name { get; set; }
        [DisplayName("Subtitle")]
        public String Subtitle { get; set; }
        [DisplayName("Cost")]
        public String Cost { get; set; }
        [DisplayName("Description")]
        public String Description { get; set; }

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
        [DisplayName("Time Zone")]
        public int TimeZoneOffset { get; set; }

        public IEnumerable<SelectListItem> TimeZones { get; set; }

    }
}