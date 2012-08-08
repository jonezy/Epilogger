using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using RichmondDay.Helpers;
using SubSonic.Schema;

namespace Epilogger.Web.Core.Stats
{
    public class EventGrowth
    {
        private const string CacheKey = "EventGrowth";
        protected virtual double CacheExpiry
        {
            get { return 60; }
        }

        public List<EventGrowthStats> GetEventGrowthStats()
        {
            //var cacheHelper = new CacheHelper(HttpContext.Current.Cache);
            //var data = cacheHelper.Get(CacheKey.ToString(CultureInfo.InvariantCulture)) as List<EventGrowthStats>;

            //if (data == null)
            //{
                
                var ss = new StatsService();
                var data = ss.FindEventGrowthStats();

            //    cacheHelper.Add(CacheKey.ToString(CultureInfo.InvariantCulture), data, DateTime.Now.AddMinutes(CacheExpiry));
            //}

            return data;
        }


        public List<EventGrowthStats> GetEventGrowthStats(DateTime f, DateTime t)
        {
            //var cacheHelper = new CacheHelper(HttpContext.Current.Cache);
            //var data = cacheHelper.Get(CacheKey.ToString(CultureInfo.InvariantCulture)) as List<EventGrowthStats>;

            //if (data == null)
            //{

                var ss = new StatsService();
                var data = ss.FindEventGrowthStats(f, t);

                //cacheHelper.Add(CacheKey.ToString(CultureInfo.InvariantCulture), data, DateTime.Now.AddMinutes(CacheExpiry));
            //}

            return data;
        }



    }


    


}