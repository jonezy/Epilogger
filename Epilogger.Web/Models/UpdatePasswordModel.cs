using System.ComponentModel;
namespace Epilogger.Web.Models {
    public class UpdatePasswordModel {
        [DisplayName("Email Address (you'll get an email to this address confirming the new password (NOT WORKING RIGHT NOW))")]
        public string EmailAddress { get; set; }

        [DisplayName("New password")]
        public string NewPassword { get; set; }

        [DisplayName("New password again")]
        public string NewPasswordAgain { get; set; }
    }
}