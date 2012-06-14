using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiTweet
    {
        long ID { get; set; }
        long TwitterID { get; set; }
        int? EventID { get; set; }
        string Text { get; set; }
        string TextAsHTML { get; set; }
        string Source { get; set; }
        DateTime? CreatedDate { get; set; }
        long? ToUserID { get; set; }
        long? FromUserID { get; set; }
        string FromUserScreenName { get; set; }
        string ToUserScreenName { get; set; }
        string IsoLanguageCode { get; set; }
        string ProfileImageURL { get; set; }
        long? SinceID { get; set; }
        string Location { get; set; }
        string RawSource { get; set; }
        int? DeleteVoteCount { get; set; }
        bool? Deleted { get; set; }
    }
}