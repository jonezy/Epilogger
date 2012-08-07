using Epilogger.Data;

namespace Epilogger.Web.Models
{
    public class WidgetTweetRetweetViewModel
    {
        public Tweet Tweet { get; set; }
        public string ReplyNewTweet { get; set; }
        public bool IsTwitterAuthed { get; set; }
        public string ShortEventURL { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int EventId { get; set; }
        public string EventSlug { get; set; }
        public string SearchTerms { get; set; }
        public string Name { get; set; }
        public Core.Stats.WidgetTotal EpiloggerCounts { get; set; }

        public string ReturnUrl { get; set; }
        public int Returnto { get; set; }

        public Data.WidgetCustomSetting CustomSettings { get; set; }
        public int HeightOffset { get; set; }

    }
}