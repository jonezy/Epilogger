using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epilogger.Data;

namespace Epilogger.Web.Models
{
    public class HomepageFeaturedEventsViewModel
    {
        public Event Event { get; set; }
        public IEnumerable<Image> TopImages { get; set; }
    }
}