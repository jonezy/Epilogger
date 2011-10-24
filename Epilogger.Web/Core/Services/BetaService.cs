using System;
using System.Collections.Generic;
using System.Linq;

using Epilogger.Data;

namespace Epilogger.Web {
    public class BetaService : ServiceBase<BetaSignup>
    {
        protected override string CacheKey {
            get { return "Epilogger.Web.Beta"; }
        }

        public object Save(BetaSignup entity)
        {
            if (entity.ID > 0)
                return base.GetRepository<BetaSignup>().Update(entity);

            return base.GetRepository<BetaSignup>().Add(entity);
        }


    }
}