using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.GeckoClasses
{
    public class GeckoLineChart
    {

        public List<string> item { get; set; }
        public GeckoSettings settings { get; set; }

    }

    public class GeckoSettings
    {
        public List<string> axisx { get; set; }
        public List<string> axisy { get; set; }
        public string colour { get; set; }
    }


}