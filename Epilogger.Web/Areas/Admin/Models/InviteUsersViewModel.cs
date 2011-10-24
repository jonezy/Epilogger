using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Epilogger.Web.Areas.Admin.Models {
    public class InviteUsersViewModel {
        [DisplayName("Email Addresses"), Required(ErrorMessage="Please enter at least one email address to invite to the epilogger alpha")]
        public string EmailAddresses { get; set; }
    }
}