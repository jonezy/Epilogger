using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Epilogger.Web.Models {
    public class ResetPasswordViewModel {
        public string UserID { get; set; }

        [Required(ErrorMessage="Please enter your new password"), DisplayName("New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter your new password again"), DisplayName("New password again")]
        public string NewPasswordAgain { get; set; }
    }
}