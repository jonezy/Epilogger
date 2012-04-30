    using System.Web.Mvc;
    using AutoMapper;
    using Epilogger.Data;
    using Epilogger.Web.Models;

namespace Epilogger.Web.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "SingleEvent",
                "Api/Events/Event/{id}",
                new { controller="Events", action = "Event", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "ListEvents",
                "Api/Events/{page}/{count}",
                new { controller = "Events", action = "EventList", page = UrlParameter.Optional, count = UrlParameter.Optional }
            );
            context.MapRoute(
                "ListEventsAll",
                "Api/Events",
                new { controller = "Events", action = "EventList", page = UrlParameter.Optional, count = UrlParameter.Optional }
            );

            context.MapRoute(
                "ListCategoriesAll",
                "Api/Categories",
                new { controller = "Events", action = "Categories" }
            );

            context.MapRoute(
                "ListEventsAll",
                "Api/Events/Search/{searchTerms}",
                new { controller = "Events", action = "Search", searchTerms = UrlParameter.Optional }
            );

            context.MapRoute(
                "Api_default",
                "Api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }


    }
}
