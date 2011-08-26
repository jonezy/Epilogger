using System.ComponentModel;
namespace Epilogger.Web.Models {
    public class CreateAccountModel {
        [DisplayName("First name")]
        public string FirstName { get; set; }
        
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Email address")]
        public string EmailAddress { get; set; }
        
        public string Username { get; set; }
        public string Password { get; set; }
        
    }
}