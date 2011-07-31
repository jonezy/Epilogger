
namespace Epilogger.Web.Models {
    public class UpdatePasswordModel {
        public string EmailAddress { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }
    }
}