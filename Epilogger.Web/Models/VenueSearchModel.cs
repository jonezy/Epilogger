
using System.Collections.Generic;
namespace Epilogger.Web.Models {
    public class VenueSearchModel {
        public string VenueName { get; set; }
        public string City { get; set; }

        public IEnumerable<FoursquareVenue> Venues { get; set; }
    }
}