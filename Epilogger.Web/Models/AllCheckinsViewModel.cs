using System.Collections.Generic;
namespace Epilogger.Web.Models {
    public class AllCheckinsViewModel {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<CheckinDisplayViewModel> Checkins { get; set; }
    }
}