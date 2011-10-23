
using System;
namespace Epilogger.Web.Models {
    public class EventToolbarViewModel {
        public int EventID { get; set; }
        public Guid CreatedByID { get; set; }
        public Guid CurrentUserID { get; set; }
        public UserRoleType CurrentUserRole { get; set; }
    }
}