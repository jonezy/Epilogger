using System.Collections.Generic;
namespace Epilogger.Web.Models {
    public class BrowseEventsDisplayViewModel {

        public string BrowsePageFilter { get; set; }

        public bool Authorized { get; set; }

        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }

        public IEnumerable<DashboardEventViewModel> Events { get; set; }
        public IEnumerable<Epilogger.Data.Event> UpcomingEvents { get; set; }
        public IEnumerable<Epilogger.Data.EventCategory> EventCategories { get; set; }
        public List<HotestEventsModel> HottestEvents { get; set; }
        public Epilogger.Data.Event RandomEvent { get; set; }
    }
}