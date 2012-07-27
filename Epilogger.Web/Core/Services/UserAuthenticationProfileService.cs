using System;
using System.Collections.Generic;
using System.Linq;

using Epilogger.Data;

namespace Epilogger.Web {
    public class UserAuthenticationProfileService : ServiceBase<UserAuthenticationProfile> {
        protected override string CacheKey {
            get { return "Epilogger.Web.UserAuthenticationProfiles"; }
        }

        public object Save(UserAuthenticationProfile entity) {
            if (entity.ID > 0)
                return GetRepository<UserAuthenticationProfile>().Update(entity);

            return GetRepository<UserAuthenticationProfile>().Add(entity);
        }

        public void DisconnectService(AuthenticationServices serviceType, Guid userId) {
            var profile = GetData().FirstOrDefault(p => p.UserID == userId && p.Service == serviceType.ToString());
            GetRepository<UserAuthenticationProfile>().Delete(profile);
        }

        public UserAuthenticationProfile UserAuthorizationByService(AuthenticationServices serviceType, string platform) {
            return GetData().FirstOrDefault(ua => ua.Service == serviceType.ToString() && ua.Platform == platform);
        }

        public IEnumerable<UserAuthenticationProfile> UserAuthorizationByServiceScreenName(string screenName) {
            return GetData().Where(ua => ua.ServiceUsername == screenName);
        }

        public UserAuthenticationProfile UserAuthorizationByServiceScreenNameAndPlatform(string screenName, string platform, AuthenticationServices service)
        {
            return GetData().FirstOrDefault(ua => ua.ServiceUsername == screenName && ua.Platform == platform && ua.ServiceUsername == service.ToString());
        }

    }
}