using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Epilogger.Data;
using Epilogger.Web.Core.Helpers;

namespace Epilogger.Web.Models {
    public class CreateBasicEventViewModel {
        public int ID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreatedDateTime { get; set; }


        [Required(ErrorMessage="Please enter your events name")]
        [DisplayName("Name")]
        public String Name { get; set; }

        //[Required(ErrorMessage = "Please enter your events name")]
        //[DisplayName("Friendly URL")]
        public String EventSlug { get; set; }
        
        [DisplayName("Subtitle")]
        public String Subtitle { get; set; }
        
        //[DisplayName("Description")]
        //public String Description { get; set; }

        //[DisplayName("Website")]
        //public string WebsiteURL { get; set; }

        //[DisplayName("Twitter")]
        //public string TwitterAccount { get; set; }
        
        //[DisplayName("Facebook")]
        //public string FacebookPageURL { get; set; }

        //[DisplayName("Cost")]
        //public String Cost { get; set; }

        [Required(ErrorMessage="Please enter your events start date and time")]
        [DisplayName("Event Start Date and Time")]
        public DateTime StartDateTime { get; set; }
        
        [DisplayName("Event End Date and Time")]
        public DateTime? EndDateTime { get; set; }

        [DisplayName("Data Collection Start Time")]
        public DateTime CollectionStartDateTime { get; set; }

        [DisplayName("Data Collection End Time")]
        public DateTime? CollectionEndDateTime { get; set; }
        
        //public string FoursquareVenueID { get; set; }
        //public int? VenueID { get; set; }
        //public Epilogger.Data.Venue Venue { get; set; }

        ////[RegularExpression(@".*eventbrite.c.*", ErrorMessage = "Invalid Eventbrite URL. example.eventbrite.com")]
        ////Had to remove for now.
        //[DisplayName("EventBrite URL")]
        //public string EventBrightUrl { get; set; }

        //public string EventBriteEID { get; set; }

        [DisplayName("Time Zone")]
        public int TimeZoneOffset { get; set; }

        public IEnumerable<TimeZone> TimeZones
        {
            get
            {
                //TimeZone timezone = new TimeZone();
                var timeZones = new List<TimeZone>
                                    {
                                        new TimeZone {TimeZoneName = "idl", TimeZoneOffset = -12},
                                        new TimeZone {TimeZoneName = "sst", TimeZoneOffset = -11},
                                        new TimeZone {TimeZoneName = "hast", TimeZoneOffset = -10},
                                        new TimeZone {TimeZoneName = "akst", TimeZoneOffset = -9},
                                        new TimeZone {TimeZoneName = "pst", TimeZoneOffset = -8},
                                        new TimeZone {TimeZoneName = "mst", TimeZoneOffset = -7},
                                        new TimeZone {TimeZoneName = "cst", TimeZoneOffset = -6},
                                        new TimeZone {TimeZoneName = "est", TimeZoneOffset = -5},
                                        new TimeZone {TimeZoneName = "ast", TimeZoneOffset = -4},
                                        new TimeZone {TimeZoneName = "brt", TimeZoneOffset = -3},
                                        new TimeZone {TimeZoneName = "brst", TimeZoneOffset = -2},
                                        new TimeZone {TimeZoneName = "azot", TimeZoneOffset = -1},
                                        new TimeZone {TimeZoneName = "wet", TimeZoneOffset = 0},
                                        new TimeZone {TimeZoneName = "cet", TimeZoneOffset = 1},
                                        new TimeZone {TimeZoneName = "cat", TimeZoneOffset = 2},
                                        new TimeZone {TimeZoneName = "eat", TimeZoneOffset = 3},
                                        new TimeZone {TimeZoneName = "msk", TimeZoneOffset = 4},
                                        new TimeZone {TimeZoneName = "pkt", TimeZoneOffset = 5},
                                        new TimeZone {TimeZoneName = "bst", TimeZoneOffset = 6},
                                        new TimeZone {TimeZoneName = "ict", TimeZoneOffset = 7},
                                        new TimeZone {TimeZoneName = "cst", TimeZoneOffset = 8},
                                        new TimeZone {TimeZoneName = "jst", TimeZoneOffset = 9},
                                        new TimeZone {TimeZoneName = "pgt", TimeZoneOffset = 10},
                                        new TimeZone {TimeZoneName = "nct", TimeZoneOffset = 11},
                                        new TimeZone {TimeZoneName = "fjt", TimeZoneOffset = 12}
                                    };

                return timeZones;
            }
        }

        public IEnumerable<SelectListItem> collectDataList
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem
                {
                    Text = "only during the event",
                    Value = "1"
                });
                items.Add(new SelectListItem
                {
                    Text = "few hours before/after",
                    Value = "2"
                });
                items.Add(new SelectListItem
                {
                    Text = "few days before/after",
                    Value = "3",
                    Selected = true
                });
                items.Add(new SelectListItem
                {
                    Text = "2 weeks before/after",
                    Value = "4"
                });

                return items;
            }
        }

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

        public IEnumerable<SelectListItem> Times
        {
            get
            {
                List<SelectListItem> times = new List<SelectListItem>();
                for (int Minutes = 60; Minutes < 770; Minutes += 30)
                {
                    TimeSpan TS = TimeSpan.FromMinutes(Minutes);
                    String TSString = String.Format("{0:00}:{1:00}", TS.Hours, TS.Minutes);
                    SelectListItem item = new SelectListItem();
                    item.Text = TSString;
                    item.Value = TSString; //TS.Minutes.ToString();
                    times.Add(item);
                }

                return times;
            }
        }

        public EventToolbarViewModel ToolbarViewModel { get; set; }

        public UserRoleType CurrentUserRole { get; set; }
        public Guid CurrentUserID { get; set; }



    }
}