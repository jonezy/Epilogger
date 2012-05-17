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
    }
}