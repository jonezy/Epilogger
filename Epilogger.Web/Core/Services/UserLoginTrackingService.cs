using System;
using System.Collections.Generic;
using System.Linq;

using Epilogger.Data;

namespace Epilogger.Web {
    public class UserLoginTrackingService : ServiceBase<UserLoginTracking>
    {
        protected override string CacheKey {
            get { return "Epilogger.Web.UserLoginTracking"; }
        }


        public int GetUniqueUserLogins(DateTime f, DateTime t)
        {
            return db.UserLoginTrackings.Where(e => e.DateTime <= f && e.DateTime >= t).Select(e => e.UserId).Distinct().Count();
        }


        public object Save(UserLoginTracking entity)
        {
            if (entity.ID > 0)
                return base.GetRepository<UserLoginTracking>().Update(entity);

            return base.GetRepository<UserLoginTracking>().Add(entity);
        }

        

    }
}