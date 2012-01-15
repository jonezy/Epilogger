using System;

namespace Epilogger.Web.Models {
    public class DashboardEventViewModel {
        public int ID { get; set; }
        public string EventSlug { get; set; }
        public int EventCategoryID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string SearchTerms { get; set; }
        public int TotalTweets { get; set; }
        public int TotalMedia { get; set; }
    }
}