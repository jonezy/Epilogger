using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

using Epilogger.Data;

using RichmondDay.Helpers;

using SubSonic.Repository;

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

        protected SubSonicRepository<T> GetRepository<T>() where T : class, new() {
            return new SubSonicRepository<T>(db);
        }

        //CB Sept 1, 2011
        //This method is a whore when it comes to Tweets. That .GetAll().ToList() will try to return all tweets without evaluating any linq operators.
        //This will be murder for most tables in EPL as they're huge.
        protected virtual List<T> GetData() {
            List<T> data = CacheHelper != null && CacheExpiry > 0 ? CacheHelper.Get(CacheKey) as List<T> : null;

            if (data == null) {
                data = GetRepository<T>().GetAll().ToList();
                if (CacheHelper != null)
                    CacheHelper.Add(CacheKey, data, DateTime.Now.AddSeconds(CacheExpiry));
            }

            return data;
        }

        // CJ Sept 6, 2011
        // Got that whore some medicine.  Just pass in your query expression to this overloaded method and your query will be run
        // against the data repository when refetching instead of getting everything. 
        // The neat thing about this is that it caches each result and uses the query as part of the lookup key, so any time that
        // same query is performed the results will be retrieved from cache
        protected virtual List<T> GetData(Expression<Func<T, bool>> expression) {
            string localCacheKey = string.Format("{0}-{1}", expression.ToString().Replace(" ", ""), CacheKey);
            List<T> data = CacheHelper != null && CacheExpiry > 0 ? CacheHelper.Get(localCacheKey) as List<T> : null;
            
            if (data == null) {
                data = GetRepository<T>().Find(expression).ToList();
                if (CacheHelper != null)
                    CacheHelper.Add(localCacheKey, data, DateTime.Now.AddSeconds(CacheExpiry));
            }

            return data;
        }

    }
}