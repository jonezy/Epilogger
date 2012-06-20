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
    public class CheckInManager : ICheckInManager
    {
        CheckInService _cs = new CheckInService();

        public CheckInManager()
        {
            if (_cs == null) _cs = new CheckInService();    
        }

        //****************

        public int FindCheckInCountByEventID(int eventId)
        {
            return _cs.FindCheckInCountByEventID(eventId, (DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue);
        }

        public List<ApiCheckIn> FindCheckInsByEventIDPaged(int eventId, int page, int count)
        {
            return
                Mapper.Map<List<CheckIn>, List<ApiCheckIn>>(
                    _cs.FindByEventIDPaged(eventId, page, count, (DateTime)SqlDateTime.MinValue,
                                          (DateTime)SqlDateTime.MaxValue).ToList());
        }

    }
 
}