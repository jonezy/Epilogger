using System.Collections.Generic;

namespace Epilogger.Web.Models {
    public class AllPhotosDisplayViewModel {
        public string ID { get; set; }
        public string Name { get; set; }

        public bool ShowTopPhotos { get; set; }

        public int PhotoCount { get; set; }
        public int Page { get; set; }
        public int CurrentPageIndex { get; set; }

        public IEnumerable<Epilogger.Data.Image> Images { get; set; }
    }
}