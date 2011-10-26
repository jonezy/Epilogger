using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;

namespace Epilogger.Web
{
    public class StatusMessagesService : ServiceBase<StatusMessage>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.StatusMessages"; }
        }

        public object Save(StatusMessage entity)
        {
            return base.GetRepository<StatusMessage>().Add(entity);
        }

        public IEnumerable<StatusMessage> GetLast10Messages()
        {
            return db.StatusMessages.OrderByDescending(s => s.MSGDateTime);
        }
        
    }
}