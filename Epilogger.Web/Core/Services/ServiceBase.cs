using System.Web;
using System.Linq;
using Epilogger.Data;
using RichmondDay.Helpers;

using SubSonic.Query;
using SubSonic.Repository;
using System.Collections.Generic;

namespace Epilogger.Web {
    public abstract class ServiceBase<T> where T :class, new() {
        protected virtual double CacheExpiry {
            get { return 60;}
        } // time in seconds the cache should live for

        protected abstract string CacheKey {
            get;
        } // a unique way to identify the item in the cache

        protected CacheHelper CacheHelper;

        protected EpiloggerDB db;

        public ServiceBase() {
            if (db == null) db = new EpiloggerDB();
            
            if (App.CachingEnabled) {
                if (CacheHelper == null) CacheHelper = new CacheHelper(HttpContext.Current.Cache);
            }
        }

        protected SubSonicRepository<T> GetRepository<T>(IQuerySurface db) where T : class, new() {
            return new SubSonicRepository<T>(db);
        }

        protected virtual List<T> GetData() {
            List<T> data = CacheHelper != null ? CacheHelper.Get(CacheKey) as List<T> : null;

            if (data == null) {
                data = GetRepository<T>(db).GetAll().ToList();
                if (CacheHelper != null)
                    CacheHelper.Add(CacheKey, data);
            }

            return data;
        }
    }
}