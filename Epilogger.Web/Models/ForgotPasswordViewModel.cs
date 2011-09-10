using System.ComponentModel;
namespace Epilogger.Web.Models {
    public class ForgotPasswordViewModel {
        [DisplayName("Email address")]
        public string EmailAddress { get; set; }
    }
}