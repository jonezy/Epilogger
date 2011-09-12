using System.Collections.Generic;

using System.Web.Mvc;
using RichmondDay.Helpers;

namespace Epilogger.Web.Models {
    public class DashboardIndexViewModel {
        public int PageSize { get { return 12; } }
        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }

        public string TotalRecordsLabel { get; set; }

        public List<DashboardActivityModel> Activity { get; set; }

        public DashboardIndexViewModel(List<DashboardActivityModel> activities, int currentPageIndex, int totalRecords) {
            CurrentPageIndex = currentPageIndex;
            TotalRecords = totalRecords;

            Activity = activities;
        }
    }
}