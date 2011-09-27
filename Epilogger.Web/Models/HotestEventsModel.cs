using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Models
{
    public class HotestEventsModel
    {
        public Epilogger.Data.Event Event { get; set; }
        public List<Epilogger.Data.Image> RandomHottestImages { get; set; }

    }
}