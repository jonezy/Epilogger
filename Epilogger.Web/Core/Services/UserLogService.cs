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

        //public IEnumerable<BlogPost> FindByEventID(int EventID)
        //{
        //    return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        //}

        //public IEnumerable<BlogPost> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter)
        //{
        //    return db.BlogPosts.Where(t => t.EventID == EventID & t.DateTime >= StartDateTimeFilter & t.DateTime <= EndDateTimeFilter);
        //}

        public object Save(UserClickAction entity)
        {
            return base.GetRepository<UserClickAction>().Add(entity);
        }


    }
}