using System.Linq;

using AutoMapper;

using Epilogger.Data;

namespace Epilogger.Web {
    public class FacebookProfilePictureResolver : ValueResolver<User, string> {
        protected override string ResolveCore(User source) {
            if (source.UserAuthenticationProfiles == null)
                return string.Empty;

            try
            {
                var userAuth = source.UserAuthenticationProfiles.FirstOrDefault(ua => ua.Service == "FACEBOOK" && ua.Platform== "Web");
                if (userAuth != null) return FacebookHelper.GetProfilePicture(userAuth.Token);
            }
            catch { return ""; }
            return null;
        }
    }
}