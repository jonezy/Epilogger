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


        //public ApiVenue FindByID(int id)
        //{
        //    return Mapper.Map<Venue, ApiVenue>(_vs.FindByID(id));
        //}

        //public ApiVenue FindByFourSquareVenueID(string fourSquareVenueID)
        //{
        //    return Mapper.Map<Venue, ApiVenue>(_vs.FindByFourSquareVenueID(fourSquareVenueID));
        //}


    }
 
}