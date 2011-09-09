using System.Collections.Generic;

using System.Web.Mvc;
using RichmondDay.Helpers;

namespace Epilogger.Web.Models {
    public class DashboardIndexViewModel {
        public int PageSize { get { return 12; } }
        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }

        public string TotalRecordsLabel { get; set; }

        public IPagedList<DashboardActivityModel> Activity { get; set; }

        public DashboardIndexViewModel(IList<DashboardActivityModel> activities, int currentPageIndex) {
            CurrentPageIndex = currentPageIndex;
            TotalRecords = activities.Count;

            Activity = activities.ToPagedList(CurrentPageIndex, PageSize, TotalRecords);
        }
    }
}