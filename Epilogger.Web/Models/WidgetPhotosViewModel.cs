using System.Collections.Generic;

namespace Epilogger.Web.Models
{
    public class WidgetPhotosViewModel
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public string EventSlug { get; set; }
        public string Name { get; set; }
        public Core.Stats.WidgetTotal EpiloggerCounts { get; set; }
        public IEnumerable<Data.Image> Images { get; set; }

        public Data.WidgetCustomSetting CustomSettings { get; set; }
        public int HeightOffset { get; set; }
    }
}