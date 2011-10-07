using System;
using System.Collections.Generic;
using System.Linq;

using Epilogger.Data;

namespace Epilogger.Web {
    public class BlogService : ServiceBase<BlogPost> {
        protected override string CacheKey {
            get { return "Epilogger.Web.Blog"; }
        }

        public object Save(BlogPost entity) {
            if (entity.ID > 0)
                return base.GetRepository<BlogPost>().Update(entity);

            return base.GetRepository<BlogPost>().Add(entity);
        }

        public IEnumerable<BlogPost> FindByEventID(int EventID) {
            return FindByEventID(EventID, DateTime.Parse("1900-01-01 00:00:00"), DateTime.Parse("9999-12-31 00:00:00"));
        }

        public IEnumerable<BlogPost> FindByEventID(int EventID, DateTime StartDateTimeFilter, DateTime EndDateTimeFilter) {
            return db.BlogPosts.Where(t => t.EventID == EventID & t.DateTime >= StartDateTimeFilter & t.DateTime <= EndDateTimeFilter);
        }

    }
}