using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Admin.Models
{
    public class AdminDashboardViewModel
    {

        public int TotalUsers { get; set; }
        public int TotalEvents { get; set; }

        public int EventsToday { get; set; }
        public int EventsYesterday { get; set; }
        public float AvgNumberOfEventsPerDay { get; set; }

        public int EventsThisWeek { get; set; }
        public int EventsLastWeek { get; set; }
        public float AvgNumberOfEventsPerWeek { get; set; }

    }
}