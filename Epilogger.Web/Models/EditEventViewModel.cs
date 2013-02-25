using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Epilogger.Data;

namespace Epilogger.Web.Models {
    public class EditEventViewModel {
        public int ID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreatedDateTime { get; set; }

        [DisplayName("Private Event (Only you have access to view this event)")]
        public bool IsPrivate { get; set; }

        [Required(ErrorMessage="Please enter your events name")]
        [DisplayName("Event title:")]
        public String Name { get; set; }

        [DisplayName("Event subtitle:")]
        public String Subtitle { get; set; }

        public IEnumerable<SelectListItem> Times
        {
            get
            {
                var times = new List<SelectListItem>();
                for (var minutes = 60; minutes < 770; minutes += 30)
                {
                    var ts = TimeSpan.FromMinutes(minutes);
                    var tsString = String.Format("{0:0}:{1:00}", ts.Hours, ts.Minutes);
                    var item = new SelectListItem { Text = tsString, Value = tsString };
                    times.Add(item);
                }

                return times;
            }
        }
        public IEnumerable<TimeZone> TimeZones
        {
            get
            {
                var timeZones = new List<TimeZone>
                                    {
                                        new TimeZone {TimeZoneName = "IDLW (-12)", TimeZoneOffset = -12},
                                        new TimeZone {TimeZoneName = "NT   (-11)", TimeZoneOffset = -11},
                                        new TimeZone {TimeZoneName = "AHST (-10)", TimeZoneOffset = -10},
                                        new TimeZone {TimeZoneName = "YST  (-9)", TimeZoneOffset = -9},
                                        new TimeZone {TimeZoneName = "PST  (-8)", TimeZoneOffset = -8},
                                        new TimeZone {TimeZoneName = "MST  (-7)", TimeZoneOffset = -7},
                                        new TimeZone {TimeZoneName = "CST  (-6)", TimeZoneOffset = -6},
                                        new TimeZone {TimeZoneName = "EST  (-5)", TimeZoneOffset = -5},
                                        new TimeZone {TimeZoneName = "AST  (-4)", TimeZoneOffset = -4},
                                        new TimeZone {TimeZoneName = "EBT  (-3)", TimeZoneOffset = -3},
                                        new TimeZone {TimeZoneName = "AT   (-2)", TimeZoneOffset = -2},
                                        new TimeZone {TimeZoneName = "EGT  (-1)", TimeZoneOffset = -1},
                                        new TimeZone {TimeZoneName = "UTC  (0)", TimeZoneOffset = 0},
                                        new TimeZone {TimeZoneName = "CET  (+1)", TimeZoneOffset = 1},
                                        new TimeZone {TimeZoneName = "EET  (+2)", TimeZoneOffset = 2},
                                        new TimeZone {TimeZoneName = "MSK  (+3)", TimeZoneOffset = 3},
                                        new TimeZone {TimeZoneName = "SAMT (+4)", TimeZoneOffset = 4},
                                        new TimeZone {TimeZoneName = "PKT  (+5)", TimeZoneOffset = 5},
                                        new TimeZone {TimeZoneName = "BST  (+6)", TimeZoneOffset = 6},
                                        new TimeZone {TimeZoneName = "ICT  (+7)", TimeZoneOffset = 7},
                                        new TimeZone {TimeZoneName = "IRKT (+8)", TimeZoneOffset = 8},
                                        new TimeZone {TimeZoneName = "JST  (+9)", TimeZoneOffset = 9},
                                        new TimeZone {TimeZoneName = "PGT  (+10)", TimeZoneOffset = 10},
                                        new TimeZone {TimeZoneName = "NCT  (+11)", TimeZoneOffset = 11},
                                        new TimeZone {TimeZoneName = "FJT  (+12)", TimeZoneOffset = 12}
                                    };

                return timeZones;
            }
        }

        public string CollectDataValue { get; set; }
        public IEnumerable<SelectListItem> CollectDataList
        {
            get
            {
                var items = new List<SelectListItem>
                                {
                                    new SelectListItem
                                        {
                                            Text = "only during the event",
                                            Value = "1"
                                        },
                                    new SelectListItem
                                        {
                                            Text = "few hours before/after",
                                            Value = "2"
                                        },
                                    new SelectListItem
                                        {
                                            Text = "few days before/after",
                                            Value = "3",
                                            Selected = true
                                        },
                                    new SelectListItem
                                        {
                                            Text = "2 weeks before/after",
                                            Value = "4"
                                        }
                                };

                return items;
            }
        }
        







        [Required(ErrorMessage = "Please enter your events name")]
        [DisplayName("Friendly URL")]
        public String EventSlug { get; set; }
        
        

        [Required(ErrorMessage = "Please enter some search terms for your event (ex: epilogger OR EPL)")]
        [DisplayName("Search Terms")]
        public string SearchTerms { get; set; }

        [DisplayName("Description:")]
        public String Description { get; set; }

        [DisplayName("Website Url:")]
        public string WebsiteURL { get; set; }

        [DisplayName("Twitter:")]
        public string TwitterAccount { get; set; }

        [DisplayName("Facebook:")]
        public string FacebookPageURL { get; set; }

        [DisplayName("Cost:")]
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
        [DisplayName("EventBrite Url:")]
        public string EventBrightUrl { get; set; }

        public string EventBriteEID { get; set; }

        [DisplayName("Time Zone")]
        public int TimeZoneOffset { get; set; }



        
       
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

        public bool IsPaid { get; set; }
        public string CurrentSection { get; set; }

    }
}