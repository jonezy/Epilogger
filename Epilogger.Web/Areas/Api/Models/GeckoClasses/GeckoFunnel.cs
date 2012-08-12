using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.GeckoClasses
{
    public class GeckoFunnel
    {
        public string type { get; set; }
        public string percentage { get; set; }
        public List<GeckoFunnelItem> item { get; set; }
    }

    public class GeckoFunnelItem
    {
        public int value { get; set; }
        public string label { get; set; }
    }
}