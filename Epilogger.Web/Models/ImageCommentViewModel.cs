using System.Collections.Generic;
using Epilogger.Data;

namespace Epilogger.Web.Models
{
    public class ImageCommentViewModel
    {
        public int EventId { get; set; }
        public int ImageId { get; set; }
        public IEnumerable<Tweet> Tweets { get; set; }
        public int TotalTweetCount { get; set; }
        public bool StyleFirstTweet { get; set; }
        public string ModifyDisplayClass { get; set; }
        public bool ShowControls { get; set; }
        public bool CanDelete { get; set; }
        public int? page { get; set; }
        public string CommentLocation { get; set; }
    }
}