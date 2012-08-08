using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;
using Epilogger.Web.Core.Stats;
using Epilogger.Web.Models;


namespace Epilogger.Web.Areas.Api.Models
{
    public class StatManager : IStatManager
    {
        StatsService _ss = new StatsService();

        public StatManager()
        {
            if (_ss == null) _ss = new StatsService();    
        }

        //****************

        /* Users */
        public List<UserGrowthStats> GetUserGrowth()
        {
            var stats = new UserGrowth();
            return stats.GetUserGrowthStats();
        }

        public List<UserGrowthStats> GetUserGrowth(DateTime f, DateTime t)
        {
            var stats = new UserGrowth();
            return stats.GetUserGrowthStats(f, t);
        }

        /* Events */
        public List<EventGrowthStats> GetEventGrowth()
        {
            var stats = new EventGrowth();
            return stats.GetEventGrowthStats();
        }

        public List<EventGrowthStats> GetEventGrowth(DateTime f, DateTime t)
        {
            var stats = new EventGrowth();
            return stats.GetEventGrowthStats(f, t);
        }


        public int GetEventCount()
        {
            return new EventStats().AllEventCount();
        }

        public int GetActiveEventCount()
        {
            return new EventStats().ActiveEventCount();
        }

        public int CollectingEventCount()
        {
            return new EventStats().CollectingEventCount();
        }
        



    }
 
}