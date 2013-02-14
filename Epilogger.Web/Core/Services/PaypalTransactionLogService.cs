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
            return base.GetRepository<PaypalTransactionLog>().Add(entity);
        }

        
        
    }
}