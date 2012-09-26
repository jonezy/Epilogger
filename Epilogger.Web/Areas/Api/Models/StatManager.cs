using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;
using Epilogger.Web.Areas.Api.Models.GeckoClasses;
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

        public List<UserGrowthStats> GetDailyUserGrowth(DateTime f, DateTime t)
        {
            var stats = new UserGrowth();
            return stats.GetDailyUserGrowthStats(f, t);
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
        
        public int GetActiveUsers(DateTime f, DateTime t)
        {
            return new UserLogService().GetUniqueUsersForDateRange(f, t);
        }

        public int GetTweetCount()
        {
            return new TweetService().Count();
        }

        public int GetPhotoCount()
        {
            return new ImageService().Count();
        }


        public bool EpiloggerStatus()
        {
            //First test if SQL is responding
            try
            {
                var ecount = new EventService().Count();
                if (ecount == 0) return false;
            }
            catch
            {
                return false;
            }

            //Test we can pull up an EPL webpage.
            try
            {
                using (var client = new WebClient())
                {
                    var result = client.DownloadString("http://epilogger.com/home/webstatus");
                    if (result.IndexOf("Epilogger is Up", System.StringComparison.Ordinal) == 0) return false;
                }
            }
            catch
            {
                return false;
            }

            //All is good
            return true;
        }


        public List<GeckoFunnelItem> EpiloggerServiceStatus()
        {

            try
            {
                var EPLservices = new EpiloggerServices().GetEpiloggerServiceStatus();
                if (EPLservices == null) throw new ArgumentNullException("EPLservices");
                var funnelItems = new List<GeckoFunnelItem>();

                foreach (var service in EPLservices)
                {
                    funnelItems.Add(new GeckoFunnelItem { label = service.DisplayName, value = service.Status == ServiceControllerStatus.Running ? 100 : 0 });
                }
                return funnelItems;
            }
            catch
            {
                return null;
            }
            
        }

        public int NumberOfTweetsInDateRange(DateTime f, DateTime t)
        {
            return new TweetService().NumberOfTweetsInDateRange(f, t);
        }

        public List<TweetGrowthStats> GetTweetGrowth(DateTime f, DateTime t)
        {
            return new TweetGrowth().GetTweetGrowthStats(f, t);
        }

        public List<TopEventsByUserActivityStats> GetTopEventByUserActivity(DateTime f, DateTime t)
        {
            return _ss.FindTopEventsByUserActivityStats(f, t);
        }


    }
 
}