using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Epilogger.Web.Models {
    public class LoginModel {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}