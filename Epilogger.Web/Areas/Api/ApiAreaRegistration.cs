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
                "AuthEPLUser",
                "Api/Users/Authenticate",
                new { controller = "ApiEvents", action = "AuthEPLUser" }
            );

            context.MapRoute(
                "ConnectTwitterAccountToUser",
                "Api/Users/Authenticate/ConnectTwitterAccountToUser",
                new { controller = "ApiEvents", action = "ConnectTwitterAccountToUser" }
            );

            context.MapRoute(
                "ConnectFacebookAccountToUser",
                "Api/Users/Authenticate/ConnectFacebookAccountToUser",
                new { controller = "ApiEvents", action = "ConnectFacebookAccountToUser" }
            );



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
                "SearchInEvent",
                "Api/Events/Event/{eventId}/SearchInEvent/{searchTerm}",
                new { controller = "ApiEvents", action = "SearchInEvent", eventId = UrlParameter.Optional, searchTerm = UrlParameter.Optional }
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
                "MyEventsByUserId",
                "Api/Events/Me/{userId}/{page}/{count}",
                new { controller = "ApiEvents", action = "GetUserSubscribedAndCreatedEvents", userId = UrlParameter.Optional, page = UrlParameter.Optional, count = UrlParameter.Optional }
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


            context.MapRoute(
                "AllFeedByEventId",
                "Api/AllFeed/{eventid}/{page}/{count}",
                new { controller = "ApiEvents", action = "AllFeed", eventid = UrlParameter.Optional, page = UrlParameter.Optional, count = UrlParameter.Optional }
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
            

            //* CheckIns *
            context.MapRoute(
                "FindCheckInCountByEventID",
                "Api/CheckIns/{eventid}/count",
                new { controller = "ApiEvents", action = "FindCheckInCountByEventID", eventid = UrlParameter.Optional }
            );

            context.MapRoute(
                "FindByEventIDPaged",
                "Api/CheckIns/{eventid}/{page}/{count}",
                new { controller = "ApiEvents", action = "FindByEventIDPaged", eventid = UrlParameter.Optional, page = UrlParameter.Optional, count = UrlParameter.Optional }
            );

            //* Venues *
            context.MapRoute(
                "FindByVenueID",
                "Api/Venues/{id}",
                new { controller = "ApiEvents", action = "GetVenueByID", venueid = UrlParameter.Optional }
            );

            context.MapRoute(
                "FindByFoursquareVenueID",
                "Api/Venues/foursquarevenueid/{foursquareVenueId}", defaults: new { controller = "ApiEvents", action = "GetVenueByFoursquareVenueID", foursquareVenueId = UrlParameter.Optional }
            );



            /* User */
            context.MapRoute(
                "SaveUserFollowsEvent",
                "Api/users/user/SaveUserFollowsEvent",
                new { controller = "ApiEvents", action = "SaveUserFollowsEvent" }
            );

            context.MapRoute(
                "DeleteUserEventSubscription",
                "Api/users/user/DeleteUserEventSubscription/{userId}/{eventId}",
                new { controller = "ApiEvents", action = "DeleteUserEventSubscription", userId = UrlParameter.Optional, eventId = UrlParameter.Optional }
            );

            context.MapRoute(
                "GetUserByID",
                "Api/users/user/{userid}",
                new { controller = "ApiEvents", action = "GetUserByID", userid = UrlParameter.Optional }
            );

            context.MapRoute(
                "GetUserByUsername",
                "Api/users/user/byname/{userName}",
                new { controller = "ApiEvents", action = "GetUserByUsername", userName = UrlParameter.Optional }
            );

            context.MapRoute(
                "GetUserByEmail",
                "Api/users/user/byemail/{email}",
                new { controller = "ApiEvents", action = "GetUserByEmail", email = UrlParameter.Optional }
            );

            /* Memory Boxes */
            context.MapRoute(
                "AddItemToMemBox",
                "Api/MemoryBoxes/MemoryBox/AddItem",
                new { controller = "ApiEvents", action = "AddItemToMemBox" }
            );
            
            context.MapRoute(
                "GetAllItemsInMemBoxPaged",
                "Api/MemoryBoxes/MemoryBox/{memBoxId}/{page}/{count}",
                new { controller = "ApiEvents", action = "GetAllItemsInMemBoxPaged", memBoxId = UrlParameter.Optional, page = UrlParameter.Optional, count = UrlParameter.Optional }
            );

            context.MapRoute(
                "GetAllTweetsInMemBoxPaged",
                "Api/MemoryBoxes/MemoryBox/{memBoxId}/Tweets/{page}/{count}",
                new { controller = "ApiEvents", action = "GetAllTweetsInMemBoxPaged", memBoxId = UrlParameter.Optional, page = UrlParameter.Optional, count = UrlParameter.Optional }
            );

            context.MapRoute(
                "GetAllPhotosInMemBoxPaged",
                "Api/MemoryBoxes/MemoryBox/{memBoxId}/Photos/{page}/{count}",
                new { controller = "ApiEvents", action = "GetAllImagesInMemBoxPaged", memBoxId = UrlParameter.Optional, page = UrlParameter.Optional, count = UrlParameter.Optional }
            );

            context.MapRoute(
                "GetAllMemoryBoxesByUserId",
                "Api/MemoryBoxes/{userid}",
                new { controller = "ApiEvents", action = "GetAllMemoryBoxesByUserId", userid = UrlParameter.Optional }
            );

            context.MapRoute(
                "GetAllMemoryBoxesByUserIdandEventId",
                "Api/MemoryBoxes/{userid}/{eventid}",
                new { controller = "ApiEvents", action = "GetAllMemoryBoxesByUserIdandEventId", userid = UrlParameter.Optional, eventid = UrlParameter.Optional }
            );

            
            

            context.MapRoute(
                "RemoveItemFromMemBox",
                "Api/MemoryBoxes/MemoryBox/RemoveItem/{memBoxItemId}",
                new { controller = "ApiEvents", action = "RemoveItemFromMemBox", memBoxItemId = UrlParameter.Optional }
            );

            
            /* Stats */
            context.MapRoute(
                "GeckoGetUserGrowthDayOverDay",
                "Api/Stats/Gecko/UserGrowth",
                new { controller = "ApiEvents", action = "GeckoGetUserGrowthDayOverDay" }
            );
            context.MapRoute(
                "GeckoGetUserGrowthLastWeek",
                "Api/Stats/Gecko/UserGrowthLastWeek",
                new { controller = "ApiEvents", action = "GeckoGetUserGrowthLastWeek" }
            );
            context.MapRoute(
                "GeckoGetUserGrowthLastMonth",
                "Api/Stats/Gecko/UserGrowthLastMonth",
                new { controller = "ApiEvents", action = "GeckoGetUserGrowthLastMonth" }
            );


            context.MapRoute(
                "GeckoGetEventGrowthDayOverDay",
                "Api/Stats/Gecko/EventGrowth",
                new { controller = "ApiEvents", action = "GeckoGetEventGrowthDayOverDay" }
            );
            context.MapRoute(
                "GeckoGetEventGrowthLastWeek",
                "Api/Stats/Gecko/EventGrowthLastWeek",
                new { controller = "ApiEvents", action = "GeckoGetEventGrowthLastWeek" }
            );
            context.MapRoute(
                "GeckoGetEventGrowthLastMonth",
                "Api/Stats/Gecko/EventGrowthLastMonth",
                new { controller = "ApiEvents", action = "GeckoGetEventGrowthLastMonth" }
            );


            context.MapRoute(
                "GeckoGetAllEventCount",
                "Api/Stats/Gecko/AllEventCount",
                new { controller = "ApiEvents", action = "GeckoEventCount" }
            );
            context.MapRoute(
                "GeckoGetActiveEventCount",
                "Api/Stats/Gecko/ActiveEventCount",
                new { controller = "ApiEvents", action = "GeckoActiveEventCount" }
            );
            context.MapRoute(
                "GeckoGetCollectingEventCount",
                "Api/Stats/Gecko/CollectingEventCount",
                new { controller = "ApiEvents", action = "GeckoCollectingEventCount" }
            );

            context.MapRoute(
                "GeckoEventCountRAG",
                "Api/Stats/Gecko/EventCountRAG",
                new { controller = "ApiEvents", action = "GeckoEventCountRAG" }
            );

            context.MapRoute(
                "GeckoDailyActiveUsers",
                "Api/Stats/Gecko/DailyActiveUsers",
                new { controller = "ApiEvents", action = "GeckoDailyUniqueActiveUsers" }
            );

            context.MapRoute(
                "GeckoMonthlyActiveUsers",
                "Api/Stats/Gecko/MonthlyActiveUsers",
                new { controller = "ApiEvents", action = "GeckoMonthlyActiveUsers" }
            );

            context.MapRoute(
                "GeckoTweetCount",
                "Api/Stats/Gecko/TweetCount",
                new { controller = "ApiEvents", action = "GeckoTweetCount" }
            );

            context.MapRoute(
                "GeckoPhotoCount",
                "Api/Stats/Gecko/PhotoCount",
                new { controller = "ApiEvents", action = "GeckoPhotoCount" }
            );

            context.MapRoute(
                "GeckoEpiloggerWebsiteStatus",
                "Api/Stats/Gecko/EPLStatus",
                new { controller = "ApiEvents", action = "GeckoEpiloggerWebsiteStatus" }
            );

            context.MapRoute(
                "GeckoEpiloggerServiceStatus",
                "Api/Stats/Gecko/EPLServiceStatus",
                new { controller = "ApiEvents", action = "GeckoEpiloggerServiceStatus" }
            );

            context.MapRoute(
                "GeckoNumberOfTweetsInTheLastHour",
                "Api/Stats/Gecko/TweetsInTheLastHour",
                new { controller = "ApiEvents", action = "GeckoNumberOfTweetsInTheLastHour" }
            );
            
            context.MapRoute(
                "GeckoGetTweetGrowthLast24Hours",
                "Api/Stats/Gecko/TweetCountPerHourLast24Hours",
                new { controller = "ApiEvents", action = "GeckoGetTweetGrowthLast24Hours" }
            );

            context.MapRoute(
                "GeckoTopEventByUserActivity",
                "Api/Stats/Gecko/TopEventByUserActivityLastDay",
                new { controller = "ApiEvents", action = "GeckoTopEventByUserActivity" }
            );
            


            context.MapRoute(
                "Api_default",
                "Api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }


    }
}
