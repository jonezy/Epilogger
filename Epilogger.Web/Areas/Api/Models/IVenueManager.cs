using System.Collections.Generic;
using Epilogger.Web.Areas.Api.Models.Classes;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface IVenueManager
    {

        ApiVenue FindByID(int id);
        ApiVenue FindByFourSquareVenueID(string fourSquareVenueID);

    }
}



