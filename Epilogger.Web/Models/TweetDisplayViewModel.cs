using System;

namespace Epilogger.Web.Models {
    public class TweetDisplayViewModel {
        public string Text { get; set; }
        public string TextAsHTML { get; set; }
        public string Source { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string FromUserScreenName { get; set; }
        public string ToUserScreenName { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}