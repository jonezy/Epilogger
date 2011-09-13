using System;

namespace Epilogger.Web.Models {
    public class DashboardEventViewModel {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public string SearchTerms { get; set; }
    }
}