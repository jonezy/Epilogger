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
                "ListTrendingEvents",
                "Api/Events/Trending",
                new { controller = "ApiEvents", action = "TrendingEvents" }
            );
            context.MapRoute(
                "ListFeaturedEvents",
                "Api/Events/Featured",
                new { controller = "ApiEvents", action = "FeaturedEvents" }
            );
            context.MapRoute(
                "SearchEvents",
                "Api/Events/Search/{searchterm}",
                new { controller = "ApiEvents", action = "SearchEvents", searchterm = UrlParameter.Optional }
            );
            
            context.MapRoute(
                "SingleEvent",
                "Api/Events/Event/{id}",
                new { controller="ApiEvents", action = "Event", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "ListEvents",
                "Api/Events/{page}/{count}",
                new { controller = "ApiEvents", action = "EventList", page = UrlParameter.Optional, count = UrlParameter.Optional }
            );
            context.MapRoute(
                "ListEventsAll",
                "Api/Events",
                new { controller = "ApiEvents", action = "EventList", page = UrlParameter.Optional, count = UrlParameter.Optional }
            );
            
            context.MapRoute(
                "ListCategoriesAll",
                "Api/Categories",
                new { controller = "ApiEvents", action = "Categories" }
            );

            context.MapRoute(
                "EventsByCategoryId",
                "Api/Categories/{categoryid}/Events/{page}/{count}",
                new { controller = "ApiEvents", action = "EventsByCategoryID", categoryid = UrlParameter.Optional, page = UrlParameter.Optional, count = UrlParameter.Optional }
            );
            

            //context.MapRoute(
            //    "ListEventsAll",
            //    "Api/Events/Search/{searchTerms}",
            //    new { controller = "Events", action = "Search", searchTerms = UrlParameter.Optional }
            //);

            context.MapRoute(
                "Api_default",
                "Api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }


    }
}
