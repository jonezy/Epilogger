using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Epilogger.Data;
using Epilogger.Web.Core.Helpers;

namespace Epilogger.Web.Models {
    public class CreateFinalEventViewModel {

        public String Name { get; set; }

        public String EventSlug { get; set; }
        
        public String Subtitle { get; set; }

        public String SearchTerms {get;set;}

        public String CollectionTime { get; set; }

        public String EventStartEndTime { get; set; }

    }
}