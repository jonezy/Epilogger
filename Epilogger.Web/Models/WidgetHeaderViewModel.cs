using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Models
{
    public class WidgetHeaderViewModel
    {
        public Data.WidgetCustomSetting CustomSettings { get; set; }
        public string Name { get; set; }
        public string EventSlug { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }
}