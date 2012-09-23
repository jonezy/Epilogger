using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Epilogger.Data;
using Twitterizer;

namespace Epilogger.Web.Models {
    public class AccountModel {
        public string Username { get; set; }

        [DisplayName("Profile Picture")]
        public string ProfilePicture { get; set; }
        public string ProfilePictureLarge { get; set; }

        public string TwitterProfilePicture { get; set; }
        public string FacebookProfilePicture { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }
        
        [DisplayName("Last name")]
        public string LastName { get; set; }


        [DisplayName("Email Address"), Required(ErrorMessage = "Please enter your email address")]
        [DataAnnotationsExtensions.Email()]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public string Password { get; set; }

        [DisplayName("Date of birth")]
        public string DateOfBirth { get; set; }

        public List<ConnectedNetworksViewModel> ConnectedNetworks { get; set; }

        public dynamic facebookUser  { get; set; }
        public TwitterResponse<TwitterUser> twitterUser { get; set; }

    }
}