using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Epilogger.Web.Models
{
    public class CreateEventViewModel
    {
        [DisplayName("Event Name")]
        public String Name { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
        public int TimeZoneOffset { get; set; }
    }
}