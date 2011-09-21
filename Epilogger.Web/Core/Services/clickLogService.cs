using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;

namespace Epilogger.Web
{
    public class ClickLogService : ServiceBase<userClickTracking>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.userClickTracking"; }
        }

        public object Save(userClickTracking entity)
        {
            return base.GetRepository<userClickTracking>().Add(entity);
        }

        public IEnumerable<userClickTracking> GetLast200ClicksByLocation(string location)
        {
            return db.userClickTrackings.Where(u => u.location == location).OrderByDescending(u => u.ClickDateTime).Take(200);            
        }
        
    }
}