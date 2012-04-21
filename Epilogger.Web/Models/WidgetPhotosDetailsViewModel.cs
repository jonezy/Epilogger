using System.Collections.Generic;

namespace Epilogger.Web.Models
{
    public class WidgetPhotosDetailsViewModel
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Core.Stats.WidgetTotal EpiloggerCounts { get; set; }
        public string EventSlug { get; set; }
        public int PhotoID { get; set; }
        public Epilogger.Data.Image Image { get; set; }
        public Data.Tweet Tweet { get; set; }
        public int returnto { get; set; }
    }
}