using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Twitterizer;

namespace Epilogger.Web.Models {
    public class CreateAccountModel {

        [Required(ErrorMessage = "Please enter your desired username")]
        public string Username { get; set; }

        [DisplayName("Email"), Required(ErrorMessage = "Please enter your email address")]
        [DataAnnotationsExtensions.Email()]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        [DisplayName("First Name"), Required(ErrorMessage = "Please enter your First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name"), Required(ErrorMessage = "Please enter your Last Name")]
        public string LastName { get; set; }

        [DisplayName("Birthday")]
        public string DateOfBirth { get; set; }

        public string DisplayProfileImage { get; set; }
        public string ProfileImage { get; set; }

        public string ServiceUserName { get; set; }

        public string AuthService { get; set; }

        public string AuthToken { get; set; }
        public string AuthTokenSecret { get; set; }
        public string AuthScreenname { get; set; }

    }
}