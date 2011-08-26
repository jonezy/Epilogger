using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Epilogger.Web.Models {
    public class LoginModel {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }
    }
}