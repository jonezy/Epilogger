using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Twitterizer;

namespace Epilogger.Web {
    public class TwitterProfilePictureResolver : ValueResolver<User, string> {
        protected override string ResolveCore(User source) {
            if (source.UserAuthenticationProfiles == null)
                return string.Empty;

            if (source.UserAuthenticationProfiles.Where(ua => ua.Service == "TWITTER").FirstOrDefault() == null)
                return string.Empty;

            UserAuthenticationProfile userAuth = source.UserAuthenticationProfiles.Where(ua => ua.Service == "TWITTER").FirstOrDefault();
            TwitterResponse<TwitterUser> twitterUser = TwitterHelper.GetUser(userAuth.Token, userAuth.TokenSecret, userAuth.ServiceUsername);

            if (twitterUser == null)
                return string.Empty;

            return twitterUser.ResponseObject.ProfileImageLocation;
        }
    }
}