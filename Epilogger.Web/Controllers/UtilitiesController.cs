using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epilogger.Data;
using SubSonic.Query;
using System.Collections;


namespace Epilogger.Web.Controllers
{
    public partial class UtilitiesController : Controller
    {
        List<string> ets = new List<string>();
        EpiloggerDB db = new EpiloggerDB();

        public virtual ActionResult Autocomplete(string term)
        {
            var routeList = (from e in db.Events
                             where e.Name.Contains(term)
                             from v in db.Venues
                             where v.ID == e.VenueID
                             select
                                 new
                                     {
                                         id = e.ID,
                                         label = e.Name,
                                         startDate = e.StartDateTime.ToString("MMM dd, yyyy"),
                                         url =  e.EventSlug,
                                         venue = v.City + ", " + v.State
                                     }).Take(9);

            return Json(routeList, JsonRequestBehavior.AllowGet);
        }


        public virtual ActionResult Autocomplete2(string term)
        {
            string html = "";
            string ven = "";

            //var tests3 = (from f in db.Events
            //             where f.Name.StartsWith(term)
            //             select new {f.Name,f.ID, f.StartDateTime, f.VenueID, f.Venues}).Take(9);

            
          

            var test = from f in db.Events
                       join v in db.Venues on f.VenueID equals v.ID into g
                       where f.Name.StartsWith(term)
                       select new
                       {
                           f.Name,
                           f.StartDateTime,
                           Venues = g.Select(x => x.Address)
                       };

            foreach (var e in test)
            {
               
                ets.Add("<div class=\"eventDetails\">" + e.Name + ":" + e.StartDateTime + ":" + e.Venues+ "</div>");
            }

           // string[] items = ets.ToArray();

            var filteredItems = ets.Where(
               item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

    }
}
