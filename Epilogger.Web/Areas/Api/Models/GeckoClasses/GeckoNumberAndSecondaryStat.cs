using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.GeckoClasses
{
    public class GeckoNumberAndSecondaryStat
    {
        public List<GeckoItem> item { get; set; }
        
    }

    public class GeckoItem
    {
        public string text { get; set; }
        public int value { get; set; }
    }

}