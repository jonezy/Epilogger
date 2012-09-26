using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using RichmondDay.Helpers;
using SubSonic.Schema;

namespace Epilogger.Web.Core.Stats
{
    public class UserGrowth
    {
        private const string CacheKey = "UserGrowth";
        protected virtual double CacheExpiry
        {
            get { return 60; }
        }

        public List<UserGrowthStats> GetUserGrowthStats()
        {
            //var cacheHelper = new CacheHelper(HttpContext.Current.Cache);
            //var data = cacheHelper.Get(CacheKey.ToString(CultureInfo.InvariantCulture)) as List<UserGrowthStats>;

            //if (data == null)
            //{
                
                var ss = new StatsService();
                var data = ss.FindUserGrowthStats();

            //    cacheHelper.Add(CacheKey.ToString(CultureInfo.InvariantCulture), data, DateTime.Now.AddMinutes(CacheExpiry));
            //}

            return data;
        }


        public List<UserGrowthStats> GetUserGrowthStats(DateTime f, DateTime t)
        {
            //var cacheHelper = new CacheHelper(HttpContext.Current.Cache);
            //var data = cacheHelper.Get(CacheKey.ToString(CultureInfo.InvariantCulture)) as List<UserGrowthStats>;

            //if (data == null)
            //{

                var ss = new StatsService();
                var data = ss.FindUserGrowthStats(f, t);

                //cacheHelper.Add(CacheKey.ToString(CultureInfo.InvariantCulture), data, DateTime.Now.AddMinutes(CacheExpiry));
            //}

            return data;
        }

        public List<UserGrowthStats> GetDailyUserGrowthStats(DateTime f, DateTime t)
        {
           
            var ss = new StatsService();
            var data = ss.FindDailyUserGrowthStats(f, t);

            return data;
        }

    }


    


}