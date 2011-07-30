using Epilogger.Data;
using System;

namespace Epilogger.Web {
    public class UserService : ServiceBase<User>{

        protected override string CacheKey {
            get { return "Epilogger.Web.Users"; }
        }

        public object Save(User entity) {
            if (entity.ID != Guid.Empty)
                return base.GetRepository<User>(db).Update(entity);

            return base.GetRepository<User>(db).Add(entity);
        }
    }
}