using System;

namespace Epilogger.Web.Models {
    public class DashboardActivityModel {
        public ActivityType ActivityType { get; set; }
        public DateTime Date { get; set; }
        public string ActivityContent { get; set; }
        public string EventName { get; set; }
        public int EventID { get; set; }
        public string EventSlug { get; set; }
    }

    public enum ActivityType {
        ALL             = 0,
        PHOTOS_VIDEOS   = 1,
        TWEET           = 2,
        EVENT_CREATION  = 3,
        EVENT_RATING    = 4,
        FOLLOW_EVENT    = 5
    }
}