using Epilogger.Data;

namespace Epilogger.Web.Models
{
    public class TweetTemplateViewModel
    {
        public Tweet Tweet { get; set; }
        public bool CanDelete { get; set; }
        public int EventId { get; set; }
        public bool ShowControls { get; set; }
        public bool Replied { get; set; }
        public bool Retweeted { get; set; }
        public bool Favorited { get; set; }
    }

}