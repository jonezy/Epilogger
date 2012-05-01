using Epilogger.Data;

namespace Epilogger.Web.Models
{
    public class TweetReplyViewModel
    {
        public Tweet Tweet { get; set; }
        public Event Event { get; set; }
        public string ReplyNewTweet { get; set; }
        public bool IsTwitterAuthed { get; set; }
    }
}