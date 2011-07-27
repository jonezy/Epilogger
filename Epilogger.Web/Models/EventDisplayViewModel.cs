using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Models {
    public class EventDisplayViewModel {
        public int ID { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string WebsiteURL { get; set; }
    }
}