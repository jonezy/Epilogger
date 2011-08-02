using System.ComponentModel;
namespace Epilogger.Web.Models {
    public class AccountModel {
        public string Username { get; set; }
        public string TwitterUsername { get; set; }
        public string FacebookUsername { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }
        
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Email address")]
        public string EmailAddress { get; set; }
    }
}