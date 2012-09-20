using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Models
{
    public class ProfileImageChoiceViewModel
    {
        public string TwitterProfilePicture { get; set; }
        public string FacebookProfilePicture { get; set; }

        public string TwitterProfilePictureLarge { get; set; }
        public string FacebookProfilePictureLarge { get; set; }
    }
}