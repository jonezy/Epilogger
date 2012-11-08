using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Models.Timeline
{
    public class Asset
    {
        public string media { get; set; }
        public string credit { get; set; }
        public string caption { get; set; }
    }

    public class Asset2
    {
        public string media { get; set; }
        public string thumbnail { get; set; }
        public string credit { get; set; }
        public string caption { get; set; }
    }

    public class Date
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string headline { get; set; }
        public string text { get; set; }
        public string tag { get; set; }
        public Asset2 asset { get; set; }
    }

    public class Era
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string headline { get; set; }
        public string text { get; set; }
        public string tag { get; set; }
    }

    public class Timeline
    {
        public string headline { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public Asset asset { get; set; }
        public List<Date> date { get; set; }
        public List<Era> era { get; set; }
    }

    public class RootObject
    {
        public Timeline timeline { get; set; }
    }

}