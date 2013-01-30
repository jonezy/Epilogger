using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Epilogger.Data;
using Epilogger.Web.Core.Helpers;

namespace Epilogger.Web.Models {
    public class CreateEventViewModel {
        public int ID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreatedDateTime { get; set; }

        [Required(ErrorMessage="Please enter your events name")]
        [DisplayName("Name")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Please enter your events name")]
        [DisplayName("Friendly URL")]
        public String EventSlug { get; set; }
        
        [DisplayName("Subtitle")]
        public String Subtitle { get; set; }

        [Required(ErrorMessage = "Please enter some search terms for your event (ex: epilogger OR EPL)")]
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

        [Required(ErrorMessage="Please enter your events start date and time")]
        [DisplayName("Event Start Date and Time")]
        public DateTime StartDateTime { get; set; }
        
        [DisplayName("Event End Date and Time")]
        public DateTime? EndDateTime { get; set; }

        [DisplayName("Data Collection Start Time")]
        public DateTime CollectionStartDateTime { get; set; }

        [DisplayName("Data Collection End Time")]
        public DateTime? CollectionEndDateTime { get; set; }

        public string FoursquareVenueID { get; set; }
        public int? VenueID { get; set; }
        public Epilogger.Data.Venue Venue { get; set; }

        //[RegularExpression(@".*eventbrite.c.*", ErrorMessage = "Invalid Eventbrite URL. example.eventbrite.com")]
        //Had to remove for now.
        [DisplayName("EventBrite URL")]
        public string EventBrightUrl { get; set; }

        public string EventBriteEID { get; set; }

        [DisplayName("Time Zone")]
        public int TimeZoneOffset { get; set; }

        

        public IEnumerable<SelectListItem> TimeZones { get; set; }
       
        [DisplayName("Category"), Required(ErrorMessage="Please select a category for your event")]
        public int CategoryID { get; set; }
        public IEnumerable<SelectListItem> Categories {
            get {
                // grab categories from the database!
                CategoryService service = new CategoryService();
                List<EventCategory> categories = service.AllCategories();

                return categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.ID.ToString()});
            }
        }


        public EventToolbarViewModel ToolbarViewModel { get; set; }

        public UserRoleType CurrentUserRole { get; set; }
        public Guid CurrentUserID { get; set; }

    }
}