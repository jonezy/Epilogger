using System;
using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Twitterizer;

namespace Epilogger.Web {
    public class TwitterProfilePictureResolver : ValueResolver<User, string> {
        protected override string ResolveCore(User source) {
            if (source.UserAuthenticationProfiles == null)
                return string.Empty;

            if (source.UserAuthenticationProfiles.FirstOrDefault(ua => ua.Service == "TWITTER" && ua.Platform=="Web") == null)
                return string.Empty;

            try
            {
                var userAuth = source.UserAuthenticationProfiles.FirstOrDefault(ua => ua.Service == "TWITTER" && ua.Platform == "Web");
                if (userAuth != null)
                {
                    var twitterUser = TwitterHelper.GetUser(userAuth.Token, userAuth.TokenSecret, userAuth.ServiceUsername);

                    return twitterUser == null ? string.Empty : twitterUser.ResponseObject.ProfileImageLocation;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return string.Empty;
        }
    }
}