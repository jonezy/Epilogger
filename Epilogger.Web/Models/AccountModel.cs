using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Epilogger.Web.Models {
    public class AccountModel {
        public string Username { get; set; }

        public string ProfilePicture { get; set; }
        public string TwitterProfilePicture { get; set; }
        public string FacebookProfilePicture { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }
        
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Email address")]
        public string EmailAddress { get; set; }

        [DisplayName("Your timezone")]
        public double TimeZone { get; set; }

        public IEnumerable<SelectListItem> TimeZones {
            get {
                Dictionary<string, string> timeZones = new Dictionary<string, string>();
                timeZones.Add("-12.0", "(GMT -12:00) Eniwetok, Kwajalein");
                timeZones.Add("-11.0", "(GMT -11:00) Midway Island, Samoa");
                timeZones.Add("-10.0", "(GMT -10:00) Hawaii");
                timeZones.Add("-9.0", "(GMT -9:00) Alaska");
                timeZones.Add("-8.0", "(GMT -8:00) Pacific Time (US & Canada)");
                timeZones.Add("-7.0", "(GMT -7:00) Mountain Time (US & Canada)");
                timeZones.Add("-6.0", "(GMT -6:00) Central Time (US & Canada), Mexico City");
                timeZones.Add("-5.0", "(GMT -5:00) Eastern Time (US & Canada), Bogota, Lima");
                timeZones.Add("-4.0", "(GMT -4:00) Atlantic Time (Canada), Caracas, La Paz");
                timeZones.Add("-3.5", "(GMT -3:30) Newfoundland");
                timeZones.Add("-3.0", "(GMT -3:00) Brazil, Buenos Aires, Georgetown");
                timeZones.Add("-2.0", "(GMT -2:00) Mid-Atlantic");
                timeZones.Add("-1.0", "(GMT -1:00 hour) Azores, Cape Verde Islands");
                timeZones.Add("0.0", "(GMT) Western Europe Time, London, Lisbon, Casablanca");
                timeZones.Add("1.0", "(GMT +1:00 hour) Brussels, Copenhagen, Madrid, Paris");
                timeZones.Add("2.0", "(GMT +2:00) Kaliningrad, South Africa");
                timeZones.Add("3.0", "(GMT +3:00) Baghdad, Riyadh, Moscow, St. Petersburg");
                timeZones.Add("3.5", "(GMT +3:30) Tehran");
                timeZones.Add("4.0", "(GMT +4:00) Abu Dhabi, Muscat, Baku, Tbilisi");
                timeZones.Add("4.5", "(GMT +4:30) Kabul");
                timeZones.Add("5.0", "(GMT +5:00) Ekaterinburg, Islamabad, Karachi, Tashkent");
                timeZones.Add("5.5", "(GMT +5:30) Bombay, Calcutta, Madras, New Delhi");
                timeZones.Add("5.75", "(GMT +5:45) Kathmandu");
                timeZones.Add("6.0", "(GMT +6:00) Almaty, Dhaka, Colombo");
                timeZones.Add("7.0", "(GMT +7:00) Bangkok, Hanoi, Jakarta");
                timeZones.Add("8.0", "(GMT +8:00) Beijing, Perth, Singapore, Hong Kong");
                timeZones.Add("9.0", "(GMT +9:00) Tokyo, Seoul, Osaka, Sapporo, Yakutsk");
                timeZones.Add("9.5", "(GMT +9:30) Adelaide, Darwin");
                timeZones.Add("10.0", "(GMT +10:00) Eastern Australia, Guam, Vladivostok");
                timeZones.Add("11.0", "(GMT +11:00) Magadan, Solomon Islands, New Caledonia");
                timeZones.Add("12.0", "(GMT +12:00) Auckland, Wellington, Fiji, Kamchatka");

                return timeZones.Select(t => new SelectListItem() { Text = t.Value, Value = t.Key});
            }
        }

        [DisplayName("Date of birth")]
        public string DateOfBirth { get; set; }

        public List<ConnectedNetworksViewModel> ConnectedNetworks { get; set; }
    }
}