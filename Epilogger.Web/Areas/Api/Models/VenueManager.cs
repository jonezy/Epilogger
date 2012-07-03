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
    public class VenueManager : IVenueManager
    {
        VenueService _vs = new VenueService();

        public VenueManager()
        {
            if (_vs == null) _vs = new VenueService();    
        }

        //****************


        public ApiVenue FindByID(int id)
        {
            return Mapper.Map<Venue, ApiVenue>(_vs.FindByID(id));
        }

        public ApiVenue FindByFourSquareVenueID(string fourSquareVenueID)
        {
            return Mapper.Map<Venue, ApiVenue>(_vs.FindByFourSquareVenueID(fourSquareVenueID));
        }


    }
 
}