using System.Collections.Generic;

namespace Epilogger.Web.Models
{
    public class MyEventsViewModel
    {
        public int PageSize { get { return 50; } }
        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }

        public List<DashboardEventViewModel> Events { get; set; }
    }
}