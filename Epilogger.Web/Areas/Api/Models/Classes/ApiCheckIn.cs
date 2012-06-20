using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiCheckIn
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        public long? TweetID { get; set; }
        public int? UserID { get; set; }
        public DateTime CheckInDateTime { get; set; }
        public string FourSquareCheckInURL { get; set; }
    }
}