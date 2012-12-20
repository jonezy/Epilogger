using System.Collections.Generic;
using Epilogger.Web.Core.Stats;
using Epilogger.Data;

namespace Epilogger.Web.Models {
    public class HomepageViewModel {
        public int PageSize { get { return 12; } }
        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }

        public string TotalRecordsLabel { get; set; }

        public List<HomepageActivityModel> Activity { get; set; }
        public IEnumerable<Epilogger.Data.StatusMessage> StatusMessages { get; set; }

        public List<Event> TrendingEvents { get; set; }

        public HomepageTotal HomepageTotal { get; set; }

        public HomepageViewModel(List<HomepageActivityModel> activities, int currentPageIndex, int totalRecords) {
            CurrentPageIndex = currentPageIndex;
            TotalRecords = totalRecords;

            Activity = activities;
        }

        public HomepageFeaturedEventsViewModel FeaturedEvents { get; set; }

    }
}