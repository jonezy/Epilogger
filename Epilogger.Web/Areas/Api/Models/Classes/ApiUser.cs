using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiUser
    {
        public Guid ID { get; set; }
        public int RoleID { get; set; }
        public string Username { get; set; }
        //public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Guid? ForgotPasswordHash { get; set; }
        public string ProfilePicture { get; set; }
        public int TimeZoneOffSet { get; set; }
        public bool IsActive { get; set; }
    }
}