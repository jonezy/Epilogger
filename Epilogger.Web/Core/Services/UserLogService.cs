using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;

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