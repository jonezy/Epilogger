using System;
using System.Collections.Generic;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface IStatManager
    {
        List<UserGrowthStats> GetUserGrowth();
        List<UserGrowthStats> GetUserGrowth(DateTime f, DateTime t);
    }
}