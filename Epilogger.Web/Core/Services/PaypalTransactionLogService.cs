using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;

namespace Epilogger.Web
{
    public class PaypalTransactionLogService : ServiceBase<PaypalTransactionLog>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.PaypalTransactionLog"; }
        }

        public object Save(PaypalTransactionLog entity)
        {
            return entity.Id > 0 ? base.GetRepository<PaypalTransactionLog>().Update(entity) : base.GetRepository<PaypalTransactionLog>().Add(entity);
        }

        public PaypalTransactionLog FindById(Guid uniqueid)
        {
            return db.PaypalTransactionLogs.FirstOrDefault(r => r.UniqueId == uniqueid);
        }
        
    }
}