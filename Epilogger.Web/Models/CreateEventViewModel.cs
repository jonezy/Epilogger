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


        [Required(ErrorMessage="Please enter your events name")]
        [DisplayName("Name")]
        public String Name { get; set; }

        [DisplayName("Subtitle")]
        public String Subtitle { get; set; }

        [Required(ErrorMessage = "Please enter some search terms for your event (ex:#epilogger OR epilogger)")]
        [DisplayName("Search Terms")]
        public string SearchTerms { get; set; }
        
        [DisplayName("Description")]
        public String Description { get; set; }

        [DisplayName("Website")]
        public string WebsiteURL { get; set; }

        [DisplayName("Twitter")]
        public string TwitterAccount { get; set; }
        
        [DisplayName("Facebook")]
        public string FacebookPageURL { get; set; }

        [DisplayName("Cost")]
        public String Cost { get; set; }

        [Required(ErrorMessage="Please enter your events start date and time.")]
        [DisplayName("Event Start Date and Time")]
        public DateTime StartDateTime { get; set; }
        
        [DisplayName("EventEnd Date and Time")]
        public DateTime? EndDateTime { get; set; }

        [DisplayName("Data Collection Start Time")]
        public DateTime CollectionStartDateTime { get; set; }

        [DisplayName("Data Collection End Time")]
        public DateTime? CollectionEndDateTime { get; set; }
        
        public string VenueID { get; set; }

        [DisplayName("Time Zone")]
        public int TimeZoneOffset { get; set; }

        public IEnumerable<SelectListItem> TimeZones { get; set; }

    }
}