using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Epilogger.Web.Models
{
    public class SearchEventViewModel
    {
        [Required]
        public string SearchTerm { get; set; }
        public int CurrentPageIndex { get; set; }

        public List<DashboardEventViewModel> Events { get; set; }

    }
}