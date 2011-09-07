using System;

namespace Epilogger.Web.Models {
    public class DashboardActivityModel {
        public ActivityType ActivityType { get; set; }
        public DateTime Date { get; set; }
        public string ActivityContent { get; set; }
        public DashboardEventViewModel Event { get; set; }
    }

    public enum ActivityType {
        ALL,
        PHOTOS_VIDEOS,
        TWEET,
        EVENT_CREATION
    }
}