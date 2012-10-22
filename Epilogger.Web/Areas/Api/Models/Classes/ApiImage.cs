using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiImage
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        public string AzureContainerPrefix { get; set; }
        public string Fullsize { get; set; }
        public string Thumb { get; set; }
        public string OriginalImageLink { get; set; }
        public DateTime DateTime { get; set; }
        public int? DeleteVoteCount { get; set; }
        public bool Deleted { get; set; }
        public string ImageFingerPrint { get; set; }
        public string EpiloggerImageLink { get; set; }
        //public string EpiloggerImageLinkShort { get; set; }
    }
}