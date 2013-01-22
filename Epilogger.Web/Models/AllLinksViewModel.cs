using System.Collections.Generic;
namespace Epilogger.Web.Models {
    public class AllLinksViewModel {
        public int ID { get; set; }
        public string EventSlug { get; set; }
        public string Name { get; set; }

        public int PageSize { get { return 12; } }
        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }

        public bool IsExpired { get; set; }

        public IEnumerable<Epilogger.Data.URL> Links { get; set; }
        public EventToolbarViewModel ToolbarViewModel { get; set; }

    }
}