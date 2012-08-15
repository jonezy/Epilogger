using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiTweeter
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Total { get; set; }
        public int PercentOfTotal { get; set; }
        public int PhotoCount { get; set; }
    }
}