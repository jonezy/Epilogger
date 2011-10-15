using System;

namespace Epilogger.Web.Models {
    public class HomepageActivityModel {
        public ActivityType ActivityType { get; set; }
        public DateTime Date { get; set; }
        public string ActivityContent { get; set; }
        public Epilogger.Data.Image Image { get; set; }
        public string EventName { get; set; }
        public int EventID { get; set; }
    }
}