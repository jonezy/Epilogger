using System.Collections.Generic;
using Epilogger.Web.Areas.Api.Models.Classes;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface ICheckInManager
    {

        int FindCheckInCountByEventID(int eventID);
        List<ApiCheckIn> FindCheckInsByEventIDPaged(int eventID, int page, int count);
        
    }
}



