using System;
using System.Collections.Generic;
using Epilogger.Web.Areas.Api.Models.GeckoClasses;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface IStatManager
    {
        List<UserGrowthStats> GetUserGrowth();
        List<UserGrowthStats> GetUserGrowth(DateTime f, DateTime t);
        List<EventGrowthStats> GetEventGrowth();
        List<EventGrowthStats> GetEventGrowth(DateTime f, DateTime t);
        List<TweetGrowthStats> GetTweetGrowth(DateTime f, DateTime t);
        int GetEventCount();
        int GetActiveEventCount();
        int CollectingEventCount();
        int GetActiveUsers(DateTime f, DateTime t);
        int GetTweetCount();
        int GetPhotoCount();
        bool EpiloggerStatus();
        List<GeckoFunnelItem> EpiloggerServiceStatus();
        int NumberOfTweetsInDateRange(DateTime f, DateTime t);
        List<TopEventsByUserActivityStats> GetTopEventByUserActivity(DateTime f, DateTime t);
        List<UserGrowthStats> GetDailyUserGrowth(DateTime f, DateTime t);
    }
}