using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiUserFollowsEvent
    {
        public int ID { get; set; }
        public Guid UserID { get; set; }
        public int EventID { get; set; }
        public DateTime Timestamp { get; set; }   
    }
}
