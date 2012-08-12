using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.GeckoClasses
{
    public class Geckometer
    {
        public int item { get; set; }
        public Ometeritem max { get; set; }
        public Ometeritem min { get; set; }
    }


    public class Ometeritem
    {
        public string text { get; set; }
        public int value { get; set; }
    }



}