using System.Collections.Generic;

namespace Epilogger.Web.Models
{
    public class WidgetTweetsViewModel
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public string EventSlug { get; set; }
        public string Name { get; set; }
        public Core.Stats.WidgetTotal EpiloggerCounts { get; set; }
        public IEnumerable<Data.Tweet> Tweets { get; set; }
    }
}