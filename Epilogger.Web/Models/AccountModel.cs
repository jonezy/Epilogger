﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Epilogger.Web.Models {
    public class AccountModel {
        public string Username { get; set; }

        public string ProfilePicture { get; set; }
        public string TwitterProfilePicture { get; set; }
        public string FacebookProfilePicture { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }
        
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Email address")]
        public string EmailAddress { get; set; }

        [DisplayName("Your timezone")]
        public int TimeZone { get; set; }

        public IEnumerable<SelectListItem> TimeZones {get;set;}
    
        [DisplayName("Date of birth")]
        public string DateOfBirth { get; set; }

        public List<ConnectedNetworksViewModel> ConnectedNetworks { get; set; }
    }
}