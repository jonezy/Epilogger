using System;
using System.Collections.Generic;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface IStatManager
    {
        List<UserGrowthStats> GetUserGrowth();
        List<UserGrowthStats> GetUserGrowth(DateTime f, DateTime t);
        List<EventGrowthStats> GetEventGrowth();
        List<EventGrowthStats> GetEventGrowth(DateTime f, DateTime t);
        int GetEventCount();
        int GetActiveEventCount();
        int CollectingEventCount();
    }
}