using System.Collections.Generic;
namespace Epilogger.Web.Models {
    public class AllCheckinsViewModel {
        public string ID { get; set; }
        public string Name { get; set; }
        
        public int PageSize { get { return 12; } }
        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }
        public List<CheckinDisplayViewModel> Checkins { get; set; }
        public EventToolbarViewModel ToolbarViewModel { get; set; }

        public AllCheckinsViewModel(List<CheckinDisplayViewModel> activities, int currentPageIndex, int totalRecords) {
            CurrentPageIndex = currentPageIndex;
            TotalRecords = totalRecords;

            Checkins = activities;
        }
    }
}