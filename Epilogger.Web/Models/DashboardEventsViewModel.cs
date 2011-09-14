using System.Collections.Generic;

namespace Epilogger.Web.Models {
    public class DashboardEventsViewModel {
        public int PageSize { get { return 12; } }
        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }

        public List<DashboardEventViewModel> Events { get; set; }
    }
}