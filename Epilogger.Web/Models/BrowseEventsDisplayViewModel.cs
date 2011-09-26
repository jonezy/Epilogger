using System.Collections.Generic;
namespace Epilogger.Web.Models {
    public class BrowseEventsDisplayViewModel {

        public string BrowsePageFilter { get; set; }


        public IEnumerable<EventDisplayViewModel> Events { get; set; }
        public IEnumerable<Epilogger.Data.Event> HottestEvents { get; set; }
        

    }
}