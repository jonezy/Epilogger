using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Models
{
    public class SearchInEventViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Eventslug { get; set; }
        public string SearchTerm { get; set; }


        public int CurrentPageIndex { get; set; }
        

        public List<SearchInEventModel> SearchResults { get; set; }

        public EventToolbarViewModel ToolbarViewModel { get; set; }
    }
}