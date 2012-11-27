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
                List<TimeZone> timeZones = new List<TimeZone>();
                timeZones.Add(new TimeZone { TimeZoneName = "idl", TimeZoneOffset = -12 });
                timeZones.Add(new TimeZone { TimeZoneName = "sst", TimeZoneOffset = -11 });
                timeZones.Add(new TimeZone { TimeZoneName = "hast", TimeZoneOffset = -10 });
                timeZones.Add(new TimeZone { TimeZoneName = "akst", TimeZoneOffset = -9 });
                timeZones.Add(new TimeZone { TimeZoneName = "pst", TimeZoneOffset = -8 });
                timeZones.Add(new TimeZone { TimeZoneName = "mst", TimeZoneOffset = -7 });
                timeZones.Add(new TimeZone { TimeZoneName = "cst", TimeZoneOffset = -6 });
                timeZones.Add(new TimeZone { TimeZoneName = "est", TimeZoneOffset = -5 });
                //timeZones.Add("-4", "(GMT -4:00) Atlantic Time (Canada), Caracas, La Paz");
                //timeZones.Add("-3.5", "(GMT -3:30) Newfoundland");
                //timeZones.Add("-3", "(GMT -3:00) Brazil, Buenos Aires, Georgetown");
                //timeZones.Add("-2", "(GMT -2:00) Mid-Atlantic");
                //timeZones.Add("-1", "(GMT -1:00 hour) Azores, Cape Verde Islands");
                //timeZones.Add("0.0", "(GMT) Western Europe Time, London, Lisbon, Casablanca");
                //timeZones.Add("1", "(GMT +1:00 hour) Brussels, Copenhagen, Madrid, Paris");
                //timeZones.Add("2", "(GMT +2:00) Kaliningrad, South Africa");
                //timeZones.Add("3", "(GMT +3:00) Baghdad, Riyadh, Moscow, St. Petersburg");
                //timeZones.Add("3.5", "(GMT +3:30) Tehran");
                //timeZones.Add("4", "(GMT +4:00) Abu Dhabi, Muscat, Baku, Tbilisi");
                //timeZones.Add("4.5", "(GMT +4:30) Kabul");
                //timeZones.Add("5", "(GMT +5:00) Ekaterinburg, Islamabad, Karachi, Tashkent");
                //timeZones.Add("5.5", "(GMT +5:30) Bombay, Calcutta, Madras, New Delhi");
                //timeZones.Add("5.75", "(GMT +5:45) Kathmandu");
                //timeZones.Add("6", "(GMT +6:00) Almaty, Dhaka, Colombo");
                //timeZones.Add("7", "(GMT +7:00) Bangkok, Hanoi, Jakarta");
                //timeZones.Add("8", "(GMT +8:00) Beijing, Perth, Singapore, Hong Kong");
                //timeZones.Add("9", "(GMT +9:00) Tokyo, Seoul, Osaka, Sapporo, Yakutsk");
                //timeZones.Add("9.5", "(GMT +9:30) Adelaide, Darwin");
                //timeZones.Add("10", "(GMT +10:00) Eastern Australia, Guam, Vladivostok");
                //timeZones.Add("11", "(GMT +11:00) Magadan, Solomon Islands, New Caledonia");
                //timeZones.Add("12", "(GMT +12:00) Auckland, Wellington, Fiji, Kamchatka");
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