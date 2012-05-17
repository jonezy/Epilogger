﻿using System;
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

        public UserAuthenticationProfile UserAuthorizationByService(AuthenticationServices serviceType) {
            return GetData().FirstOrDefault(ua => ua.Service == serviceType.ToString());
        }

        public UserAuthenticationProfile UserAuthorizationByServiceScreenName(string screenName) {
            return GetData().FirstOrDefault(ua => ua.ServiceUsername == screenName);
        }



    }
}