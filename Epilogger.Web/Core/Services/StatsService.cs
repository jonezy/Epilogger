using System.Collections.Generic;
using System;
using System.Data.SqlTypes;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;
using SubSonic.Schema;
using System.Data;

namespace Epilogger.Web {
    public class StatsService : ServiceBase<Venue> {
        protected override string CacheKey {
            get { return "Epilogger.Stats"; }
        }


        //public object Save(Venue entity) {
        //    if (entity.ID > 0)
        //        return base.GetRepository<Venue>().Update(entity);

        //    return base.GetRepository<Venue>().Add(entity);
        //}

        public List<UserGrowthStats> FindUserGrowthStats(DateTime from, DateTime to)
        {
            
            var sp = db.GetUserGrowthStats(from, to);
            return sp.ExecuteTypedList<UserGrowthStats>();

        }

        public List<UserGrowthStats> FindUserGrowthStats()
        {

            var sp = db.GetUserGrowthStats((DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue);
            return sp.ExecuteTypedList<UserGrowthStats>();

        }


        public List<EventGrowthStats> FindEventGrowthStats(DateTime from, DateTime to)
        {
            var sp = db.GetEventGrowthStats(from, to);
            return sp.ExecuteTypedList<EventGrowthStats>();
        }

        public List<EventGrowthStats> FindEventGrowthStats()
        {
            var sp = db.GetEventGrowthStats((DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue);
            return sp.ExecuteTypedList<EventGrowthStats>();
        }

        public List<TweetGrowthStats> FindTweetGrowthStats(DateTime f, DateTime t)
        {
            var sp = db.GetTweetsPerHourStats(f, t);
            return sp.ExecuteTypedList<TweetGrowthStats>();
        }

        public List<TopEventsByUserActivityStats> FindTopEventsByUserActivityStats(DateTime f, DateTime t)
        {
            var sp = db.GetTopEventByUserActivity(f, t);
            return sp.ExecuteTypedList<TopEventsByUserActivityStats>();
        }




    }


    public class UserGrowthStats
    {
        public DateTime Date { get; set; }
        public int NumberOfUsers { get; set; }
    }

    public class EventGrowthStats
    {
        public DateTime Date { get; set; }
        public int NumberOfEvents { get; set; }
    }

    public class TweetGrowthStats
    {
        public DateTime Date { get; set; }
        public int NumberOfTweets { get; set; }
    }

    public class TopEventsByUserActivityStats
    {
        public int EventId { get; set; }
        public int HitCount { get; set; }
        public string EventName { get; set; }
    }


}