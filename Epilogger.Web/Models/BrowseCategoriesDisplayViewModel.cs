using System.Collections.Generic;
namespace Epilogger.Web.Models {
    public class BrowseCategoriesDisplayViewModel {

        public string CategoryName { get; set; }
        public bool Authorized { get; set; }

        public IEnumerable<DashboardEventViewModel> Events { get; set; }
        public IEnumerable<Epilogger.Data.EventCategory> EventCategories { get; set; }
    }
}