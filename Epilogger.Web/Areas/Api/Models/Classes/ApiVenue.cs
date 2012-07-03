using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiVenue
    {
        public int ID { get; set; }
        public string FoursquareVenueID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CrossStreet { get; set; }
        public double? Geolat { get; set; }
        public double? Geolong { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}