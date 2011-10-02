using System.Collections.Generic;

namespace Epilogger.Web.Models {
    public class AllPhotosDisplayViewModel {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool ShowTopPhotos { get; set; }
        public int PhotoCount { get; set; }
        public int Page { get; set; }
        public int CurrentPageIndex { get; set; }
        public int TimeZoneOffSet { get; set; }
        public IEnumerable<Epilogger.Data.Image> Images { get; set;}
        public List<Epilogger.Data.Image> TopImages { get; set; }
    }
}