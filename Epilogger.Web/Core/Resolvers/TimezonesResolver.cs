using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Twitterizer;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Epilogger.Web {
    public class TimezonesResolver : ValueResolver<Event, IEnumerable<SelectListItem>> {
        protected override IEnumerable<SelectListItem> ResolveCore(Event source) {
            Dictionary<string, string> timeZones = new Dictionary<string, string>();
            timeZones.Add("-12", "(GMT -12:00) Eniwetok, Kwajalein");
            timeZones.Add("-11", "(GMT -11:00) Midway Island, Samoa");
            timeZones.Add("-10", "(GMT -10:00) Hawaii");
            timeZones.Add("-9", "(GMT -9:00) Alaska");
            timeZones.Add("-8", "(GMT -8:00) Pacific Time (US & Canada)");
            timeZones.Add("-7", "(GMT -7:00) Mountain Time (US & Canada)");
            timeZones.Add("-6", "(GMT -6:00) Central Time (US & Canada), Mexico City");
            timeZones.Add("-5", "(GMT -5:00) Eastern Time (US & Canada), Bogota, Lima");
            timeZones.Add("-4", "(GMT -4:00) Atlantic Time (Canada), Caracas, La Paz");
            timeZones.Add("-3.5", "(GMT -3:30) Newfoundland");
            timeZones.Add("-3", "(GMT -3:00) Brazil, Buenos Aires, Georgetown");
            timeZones.Add("-2", "(GMT -2:00) Mid-Atlantic");
            timeZones.Add("-1", "(GMT -1:00 hour) Azores, Cape Verde Islands");
            timeZones.Add("0.0", "(GMT) Western Europe Time, London, Lisbon, Casablanca");
            timeZones.Add("1", "(GMT +1:00 hour) Brussels, Copenhagen, Madrid, Paris");
            timeZones.Add("2", "(GMT +2:00) Kaliningrad, South Africa");
            timeZones.Add("3", "(GMT +3:00) Baghdad, Riyadh, Moscow, St. Petersburg");
            timeZones.Add("3.5", "(GMT +3:30) Tehran");
            timeZones.Add("4", "(GMT +4:00) Abu Dhabi, Muscat, Baku, Tbilisi");
            timeZones.Add("4.5", "(GMT +4:30) Kabul");
            timeZones.Add("5", "(GMT +5:00) Ekaterinburg, Islamabad, Karachi, Tashkent");
            timeZones.Add("5.5", "(GMT +5:30) Bombay, Calcutta, Madras, New Delhi");
            timeZones.Add("5.75", "(GMT +5:45) Kathmandu");
            timeZones.Add("6", "(GMT +6:00) Almaty, Dhaka, Colombo");
            timeZones.Add("7", "(GMT +7:00) Bangkok, Hanoi, Jakarta");
            timeZones.Add("8", "(GMT +8:00) Beijing, Perth, Singapore, Hong Kong");
            timeZones.Add("9", "(GMT +9:00) Tokyo, Seoul, Osaka, Sapporo, Yakutsk");
            timeZones.Add("9.5", "(GMT +9:30) Adelaide, Darwin");
            timeZones.Add("10", "(GMT +10:00) Eastern Australia, Guam, Vladivostok");
            timeZones.Add("11", "(GMT +11:00) Magadan, Solomon Islands, New Caledonia");
            timeZones.Add("12", "(GMT +12:00) Auckland, Wellington, Fiji, Kamchatka");

            return timeZones.Select(t => new SelectListItem() { Text = t.Value, Value = t.Key, Selected = source.TimeZoneOffset.ToString() == t.Key });
        }
    }

}