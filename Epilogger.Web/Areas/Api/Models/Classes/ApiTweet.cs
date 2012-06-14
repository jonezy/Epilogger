using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiTweet
    {
        public long ID { get; set; }
        public long TwitterID { get; set; }
        public int? EventID { get; set; }
        public string Text { get; set; }
        public string TextAsHTML { get; set; }
        public string Source { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ToUserID { get; set; }
        public long? FromUserID { get; set; }
        public string FromUserScreenName { get; set; }
        public string ToUserScreenName { get; set; }
        public string IsoLanguageCode { get; set; }
        public string ProfileImageURL { get; set; }
        public long? SinceID { get; set; }
        public string Location { get; set; }
        public string RawSource { get; set; }
        public int? DeleteVoteCount { get; set; }
        public bool? Deleted { get; set; }
    }
}