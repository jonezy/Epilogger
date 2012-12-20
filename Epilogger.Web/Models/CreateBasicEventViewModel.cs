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
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDateTime { get; set; }


        [Required(ErrorMessage="Please enter your events name")]
        [DisplayName("Name")]
        public String Name { get; set; }

        //[Required(ErrorMessage = "Please enter your events name")]
        //[DisplayName("Friendly URL")]
        public String EventSlug { get; set; }
        
        [DisplayName("Subtitle")]
        public String Subtitle { get; set; }

        [Required(ErrorMessage="Please enter your events start date and time")]
        [DisplayName("Event Start Date and Time")]
        public DateTime StartDateTime { get; set; }
        
        [DisplayName("Event End Date and Time")]
        public DateTime? EndDateTime { get; set; }

        [DisplayName("Data Collection Start Time")]
        public DateTime CollectionStartDateTime { get; set; }

        [DisplayName("Data Collection End Time")]
        public DateTime? CollectionEndDateTime { get; set; }
        
        [DisplayName("Time Zone")]
        public int TimeZoneOffset { get; set; }

        public static IEnumerable<TimeZone> TimeZones
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
        public static IEnumerable<SelectListItem> CollectDataList
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

        [DisplayName("Category:"), Required(ErrorMessage="Please select a category for your event")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories {
            get {
                // grab categories from the database!
                var service = new CategoryService();
                var categories = service.AllCategories();

                return categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.ID.ToString()});
            }
        }

        public static IEnumerable<SelectListItem> Times
        {
            get
            {
                var times = new List<SelectListItem>();
                for (var minutes = 60; minutes < 770; minutes += 30)
                {
                    var ts = TimeSpan.FromMinutes(minutes);
                    var tsString = String.Format("{0:0}:{1:00}", ts.Hours, ts.Minutes);
                    var item = new SelectListItem {Text = tsString, Value = tsString};
                    times.Add(item);
                }

                return times;
            }
        }

        public bool allDay { get; set; }

        public EventToolbarViewModel ToolbarViewModel { get; set; }

        public UserRoleType CurrentUserRole { get; set; }
        public Guid CurrentUserId { get; set; }



    }
}