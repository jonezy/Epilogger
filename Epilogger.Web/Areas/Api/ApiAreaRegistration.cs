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


            context.MapRoute(
                "TweetCountByImageID",
                "Api/Tweets/{eventid}/image/{imageid}/count",
                new { controller = "ApiEvents", action = "TweetCountByImageID", eventid = UrlParameter.Optional, imageid = UrlParameter.Optional }
            );


            context.MapRoute(
                "TweetsByImageID",
                "Api/Tweets/{eventid}/image/{imageid}/{page}/{count}",
                new { controller = "ApiEvents", action = "TweetsByImageID", eventid = UrlParameter.Optional, imageid = UrlParameter.Optional, page = UrlParameter.Optional, count = UrlParameter.Optional }
            );

            context.MapRoute(
               "Top10Tweeters",
               "Api/Tweets/{eventid}/Top10Tweeters",
               new { controller = "ApiEvents", action = "Top10Tweeters", eventid = UrlParameter.Optional }
           );


            context.MapRoute(
                "TweetsForEventPaged",
                "Api/Tweets/{eventid}/{page}/{count}",
                new { controller = "ApiEvents", action = "Tweets", eventid = UrlParameter.Optional, page = UrlParameter.Optional, count = UrlParameter.Optional }
            );


            /* Images */
            context.MapRoute(
                "ImageCountByEventID",
                "Api/Images/{eventid}/count",
                new { controller = "ApiEvents", action = "ImageCountByEventID", eventid = UrlParameter.Optional }
            );

            context.MapRoute(
                "TopImagesByEventID",
                "Api/Images/{eventid}/Top/{numberToReturn}",
                new { controller = "ApiEvents", action = "TopImagesByEventID", eventid = UrlParameter.Optional, numberToReturn = UrlParameter.Optional }
            );

            context.MapRoute(
                "NewestPhotosByEventID",
                "Api/Images/{eventid}/Newest/{numberToReturn}",
                new { controller = "ApiEvents", action = "NewestPhotosByEventID", eventid = UrlParameter.Optional, numberToReturn = UrlParameter.Optional }
            );


            context.MapRoute(
                "ImagesByEventIDOrderDescTakeX",
                "Api/Images/{eventid}/{numberToReturn}",
                new { controller = "ApiEvents", action = "ImagesByEventIDOrderDescTakeX", eventid = UrlParameter.Optional, numberToReturn = UrlParameter.Optional }
            );
            
            context.MapRoute(
                "ImagesByEventPaged",
                "Api/Images/{eventid}/{page}/{count}",
                new { controller = "ApiEvents", action = "ImagesByEventPaged", eventid = UrlParameter.Optional, page = UrlParameter.Optional, count = UrlParameter.Optional }
            );

            
            context.MapRoute(
                "TopPhotosAndTweetByEventID",
                "Api/ImagesTweets/{eventid}/{numberToReturn}",
                new { controller = "ApiEvents", action = "TopPhotosAndTweetByEventID", eventid = UrlParameter.Optional, numberToReturn = UrlParameter.Optional }
            );
            

            /*
             * 
             * 
             * 
             *
             * TopPhotosAndTweetByEventID
             *
             */








            context.MapRoute(
                "Api_default",
                "Api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }


    }
}
