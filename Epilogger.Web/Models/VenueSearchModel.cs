
using System.Collections.Generic;
namespace Epilogger.Web.Models {
    public class VenueSearchModel {
        public string VenueName { get; set; }
        public string ProvinceState { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipPostal { get; set; }

        public IEnumerable<FoursquareVenue> Venues { get; set; }
    }
}