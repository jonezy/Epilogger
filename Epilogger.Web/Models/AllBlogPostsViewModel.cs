using System.Collections.Generic;
namespace Epilogger.Web.Models {
    public class AllBlogPostsViewModel {
        public string ID { get; set; }
        public string Name { get; set; }

        public int PageSize { get { return 12; } }
        public int CurrentPageIndex { get; set; }
        public int TotalRecords { get; set; }
        public List<BlogPostDisplayViewModel> BlogPosts { get; set; }

        public EventToolbarViewModel ToolbarViewModel { get; set; }

        public AllBlogPostsViewModel()
        {
        }

        public void SetAllBlogPostsViewModel(List<BlogPostDisplayViewModel> activities, int currentPageIndex, int totalRecords) {
            CurrentPageIndex = currentPageIndex;
            TotalRecords = totalRecords;

            BlogPosts = activities;
        }
    }
}