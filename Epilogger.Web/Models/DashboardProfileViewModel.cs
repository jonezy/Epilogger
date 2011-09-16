using System;
using System.Collections.Generic;

namespace Epilogger.Web.Models {
    public class DashboardProfileViewModel {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public List<DashboardEventViewModel> Events { get; set; }
    }
}