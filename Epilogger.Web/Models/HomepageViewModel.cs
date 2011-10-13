using System.Collections.Generic;

namespace Epilogger.Web.Models {
    public class HomepageViewModel {
        public int PageSize { get { return 12; } }
        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }

        public string TotalRecordsLabel { get; set; }

        public List<HomepageActivityModel> Activity { get; set; }

        public HomepageViewModel(List<HomepageActivityModel> activities, int currentPageIndex, int totalRecords) {
            CurrentPageIndex = currentPageIndex;
            TotalRecords = totalRecords;

            Activity = activities;
        }
    }
}