
namespace Epilogger.Web.Models {
    public class GlobalNavigationModel {
        public string Username { get; set; }
        public bool UserLoggedIn { get; set; }
        public UserRoleType CurrentUserRole { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}