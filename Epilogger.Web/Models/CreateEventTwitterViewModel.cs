using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Epilogger.Data;
using Epilogger.Web.Core.Helpers;

namespace Epilogger.Web.Models
{
    public class CreateEventTwitterViewModel
    {
        public int ID { get; set; }
      [Required(ErrorMessage = "Please enter some search terms for your event (ex: epilogger OR EPL)")]
        [DisplayName("Search Terms")]
        public string SearchTerms { get; set; }
    }
}