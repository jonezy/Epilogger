using System;
using System.Linq;
using Epilogger.Data;

namespace Epilogger.Web
{
    public class UserLogService : ServiceBase<UserClickAction>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.UserClickAction"; }
        }
        
        public object Save(UserClickAction entity)
        {
            return base.GetRepository<UserClickAction>().Add(entity);
        }


        public int GetUniqueUsersForDateRange(DateTime f, DateTime t)
        {
            return db.UserClickActions.Where(e => e.ActionDateTime >= f && e.ActionDateTime <= t && e.UserID != Guid.Empty).Select(e => e.UserID).Distinct().Count();
        }
   
    }
}