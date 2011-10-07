using System.ComponentModel;
namespace Epilogger.Web.Models {
    public class AddBlogPostViewModel {
        public int EventID { get; set; }
        public int UserID { get; set; }
        [DisplayName("URL")]
        public string BlogURL { get; set; }
        public string Title { get; set; }
        [DisplayName("Excerpt")]
        public string Description { get; set; }
    }
}