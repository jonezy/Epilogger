using System.Linq;

using Epilogger.Data;

namespace Epilogger.Web {
    public class UserAuthenticationProfileService : ServiceBase<UserAuthenticationProfile> {
        protected override string CacheKey {
            get { return "Epilogger.Web.UserAuthenticationProfiles"; }
        }

        public object Save(UserAuthenticationProfile entity) {
            if (entity.ID > 0)
                return GetRepository<UserAuthenticationProfile>(db).Update(entity);

            return GetRepository<UserAuthenticationProfile>(db).Add(entity);
        }

        public UserAuthenticationProfile UserAuthorizationByService(AuthenticationServices serviceType) {
            return GetData().Where(ua => ua.Service == serviceType.ToString()).FirstOrDefault();
        }

        public UserAuthenticationProfile UserAuthorizationByServiceScreenName(string screenName) {
            return GetData().Where(ua => ua.ServiceUsername == screenName).FirstOrDefault();
        }
    }
}