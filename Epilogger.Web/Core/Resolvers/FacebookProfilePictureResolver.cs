using System.Linq;

using AutoMapper;

using Epilogger.Data;

namespace Epilogger.Web {
    public class FacebookProfilePictureResolver : ValueResolver<User, string> {
        protected override string ResolveCore(User source) {
            if (source.UserAuthenticationProfiles == null)
                return string.Empty;

            try {
                UserAuthenticationProfile userAuth = source.UserAuthenticationProfiles.Where(ua => ua.Service == "FACEBOOK").FirstOrDefault();
                return FacebookHelper.GetProfilePicture(userAuth.Token);
                
            } catch { return ""; }
        }
    }
}