using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epilogger.Data;

namespace Epilogger.Web {
    public class UserService : ServiceBase<User>{

        protected override string CacheKey {
            get { return "Epilogger.Web.Users"; }
        }
    }
}