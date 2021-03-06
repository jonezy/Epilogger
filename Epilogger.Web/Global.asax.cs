﻿using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models;
using Epilogger.Web.Areas.Api.Models.Classes;
using Epilogger.Web.Core.Email;
using Epilogger.Web.Core.Stats;
using Epilogger.Web.Models;
using Timezone.Framework;

using T4MVC;

namespace Epilogger.Web {

    public class App : System.Web.HttpApplication {
        /// <summary>
        /// The application's base url (eg: http://www.example.com, http://localhost:21215/)
        /// </summary>
        public static string BaseUrl {
            get {
                string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/");
                var httpContext = HttpContext.Current;

                var uriBuilder = new UriBuilder {
                    Host = httpContext.Request.Url.Host,
                    Path = "/",
                    Port = 80,
                    Scheme = "http",
                };

                if (httpContext.Request.IsLocal) {
                    uriBuilder.Port = httpContext.Request.Url.Port;
                }

                return new Uri(uriBuilder.Uri, VirtualPathUtility.ToAbsolute("~/")).AbsoluteUri;
            }
        }

        public static SmtpConfiguration MailConfiguration {
            get {
                return new SmtpConfiguration() {       
                    Server = ConfigurationManager.AppSettings["SiteSettings.Mail.Server"] as string ?? "",
                    Port = int.Parse(ConfigurationManager.AppSettings["SiteSettings.Mail.ServerPort"] as string),
                    Username = ConfigurationManager.AppSettings["SiteSettings.Mail.Username"] as string ?? "",
                    Password = ConfigurationManager.AppSettings["SiteSettings.Mail.Password"] as string ?? "",
                    DefaultFromAddress = ConfigurationManager.AppSettings["SiteSettings.Mail.DefaultFromAddress"] as string ?? ""
                };
            }
        }
        /// <summary>
        /// Boolean flag to indicate whether or not caching should be enabled.
        /// </summary>
        public static bool CachingEnabled {
            get {
                if (ConfigurationManager.AppSettings["CachingEnabled"] != null) {
                    bool enabled;
                    bool.TryParse(ConfigurationManager.AppSettings["CachingEnabled"].ToString(), out enabled);

                    return enabled;
                }

                return false;
            }
        }
        
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new ErrorLogging());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("ValidateAccount",
                "account/validate/{validationCode}",
                new { controller = "account", action = "validate" }
            );

            /* AccountModel Stuff */
            //routes.MapRoute("Signup", "join/signup", new { controller = "account", action = "Signup" } );
            //routes.MapRoute("CreateAccountTwitter", "join/twitter", new { controller = "account", action = "Twitter" });
            //routes.MapRoute("CreateAccountFacebookAuth", "join/facebookauth", new { controller = "account", action = "Facebookauth" });
            //routes.MapRoute("DoFacebookLogin", "join/DoFacebookLogin", new { controller = "account", action = "DoFacebookLogin" });
            //routes.MapRoute("CreateAccountFacebook", "join/facebook", new { controller = "account", action = "Facebook" });

            routes.MapRoute("Login", "login", new { controller = "account", action = "login" });

            routes.MapRoute(
                "SettingTwitterAuth", 
                "account/TwitterAuth", 
                new { controller = "account", action = "TwitterAuth"
            });

            routes.MapRoute(
                "StarRatings",
                "events/StarRatings",
                new { controller = "events", action = "StarRatings" }
            );

            routes.MapRoute(
                "VenueSearch",
                "events/venuesearch",
                new { controller = "events", action = "venuesearch" }
            );

            routes.MapRoute(
                "SearchVenues",
                "events/searchvenues",
                new { controller = "events", action = "searchvenues" }
            );

            routes.MapRoute(
                "AddBlogPost",
                "events/AddBlogPost",
                new { controller = "events", action = "AddBlogPost" }
            );

            routes.MapRoute(
                "GetLatestTweets",
                "Events/GetLastTweetsJSON",
                new { controller = "events", action = "GetLastTweetsJSON" }
            );

            routes.MapRoute(
                "GetLatestPhotos",
                "Events/GetLastPhotosJSON",
                new { controller = "events", action = "GetLastPhotosJSON" }
            );

            routes.MapRoute(
                "LiveGetLastTweets",
                "Events/LiveGetLastTweetsJson",
                new { controller = "events", action = "LiveGetLastTweetsJson" }
            );

            routes.MapRoute(
                "LiveGetLastPhotosJson",
                "Events/LiveGetLastPhotosJson",
                new { controller = "events", action = "LiveGetLastPhotosJson" }
            );


            routes.MapRoute(
                "GetBrowseOverviewTabData",
                "Events/GetBrowseOverviewTabData",
                new { controller = "events", action = "GetBrowseOverviewTabData" }
            );

            routes.MapRoute(
                "CreateEvent",
                "events/create",
                new { controller = "events", action = "create"}
            );

            routes.MapRoute(
                "TweetTemplate",
                "events/tweettemplate",
                new { controller = "events", action = "tweettemplate" }
            );

            routes.MapRoute(
                "TweetReply",
                "events/tweetreply",
                new { controller = "events", action = "tweetreply" }
            );
            routes.MapRoute(
                "TweetRetweet",
                "events/tweetretweet",
                new { controller = "events", action = "tweetretweet" }
            );

            routes.MapRoute(
                "TweetFavorite",
                "events/tweetfavorite",
                new { controller = "events", action = "tweetfavorite" }
            );
            routes.MapRoute(
                "TweetBox",
                "events/tweetbox/{eventid}/{photoid}",
                new { controller = "events", action = "tweetbox", eventid = UrlParameter.Optional, photoid = UrlParameter.Optional }
            );


            routes.MapRoute(
                "NeedTwitterAuth",
                "events/needtwitterauth",
                new { controller = "events", action = "needtwitterauth" }
            );

            routes.MapRoute(
                "PhotoDetailsPath",
                "events/photodetails/{eventid}/{photoid}",
                new { controller = "events", action = "photodetails", eventid = UrlParameter.Optional, photoid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "SocialBar",
                "home/socialbar/{eventid}/{photoid}",
                new { controller = "Home", action = "socialbar", eventid = UrlParameter.Optional, photoid = UrlParameter.Optional },
                new[] { "Epilogger.Web.Controllers" }
            );


            routes.MapRoute(
                "DeleteEvent",
                "events/delete/{eventid}",
                new { controller = "events", action = "Delete", eventid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DeleteTweet",
                "events/deletetweetajax/{eventid}/{tweetid}",
                new { controller = "events", action = "DeleteTweetAjax", eventid = UrlParameter.Optional, tweetid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DeleteImage",
                "events/deleteimageajax/{eventid}/{imageid}",
                new { controller = "events", action = "DeleteImageAjax", eventid = UrlParameter.Optional, tweetid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "GetImageComments",
                "events/getimagecomments/{eventID}/{imageid}",
                new { controller = "events", action = "getimagecomments", eventID = UrlParameter.Optional, imageid = UrlParameter.Optional }
            );

            routes.MapRoute("ChooseUploadSourceWithoutStatus", "events/imageupload/source/{id}", MVC.ImageUpload.ChooseUploadSource());
            routes.MapRoute("AuthenticateFlickr", "events/imageupload/flickr/authenticate/{id}", MVC.ImageUpload.AuthenticateFlickr());
            routes.MapRoute("SuccessfullyAuthenticatedFlickr", "events/imageupload/flickr/successfullyauthenticated/{id}", MVC.ImageUpload.SuccessfullyAuthenticatedFlickr());
            routes.MapRoute("UploadFlickrImages", "events/imageupload/flickr/upload/{id}", MVC.ImageUpload.UploadFlickrImages());
            routes.MapRoute("GetFacebookAlbums", "events/imageupload/facebook/albums/{id}", MVC.ImageUpload.GetFacebookAlbums());
            routes.MapRoute("UpoadFacebookAlbumPhotos", "events/imageupload/facebook/upload/{id}/{albumId}", MVC.ImageUpload.UploadFacebookAlbumPhotos());
            routes.MapRoute("UploadImageFromComputer", "events/imageupload/computer/{id}", MVC.ImageUpload.UploadFromComputer());           

            routes.MapRoute(
                "GetImageCommentsPaged",
                "events/getimagecommentspaged/{eventId}/{imageId}/{page}",
                new { controller = "events", action = "getimagecommentspaged", eventId = UrlParameter.Optional, imageId = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ImageCommentControl",
                "events/imagecommentcontrol/{eventId}/{imageId}",
                new { controller = "events", action = "imagecommentcontrol", eventId = UrlParameter.Optional, imageId = UrlParameter.Optional }
            );
           
            routes.MapRoute(
                "EventBrowseRoutes",
                "browse/{filter}",
                new { controller = "browse", action = "index", filter = UrlParameter.Optional }
            );

            routes.MapRoute(
                "BrowseCategtoryRoutes",
                "events/category/{categoryname}",
                new { controller = "events", action = "category", categoryname = UrlParameter.Optional }
            );

            routes.MapRoute(
                "BrowseByCategory",
                "events/browse/{categoryname}",
                new { controller = "events", action = "category", categoryname = UrlParameter.Optional }
            );

            routes.MapRoute(
                "InEventSearch",
                "events/{id}/{action}/{IEsearchterm}",
                new { controller = "events", action = "details", id = UrlParameter.Optional, IEsearchterm = UrlParameter.Optional }
            );

            routes.MapRoute(
                "EventsRoutes1",
                "events/{slug}",
                new { controller = "events", action = "details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ImageResize",
                "events/GetPhotoThumbnail",
                new { controller = "events", action = "GetPhotoThumbnail" }
            );

            routes.MapRoute(
                "EventsRoutes",
                "events/{slug}/{action}",
                new { controller = "events", action = "details", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                "WidgetAuthRoute",
                "widget/TwitterAuth",
                new { controller = "widget", action = "TwitterAuth" }
            );

            routes.MapRoute(
                "WidgetImageResize",
                "widget/GetPhotoThumbnail",
                new { controller = "widget", action = "GetPhotoThumbnail" }
            );


            routes.MapRoute(
                "WidgetRoute",
                "widget/{id}",
                new { controller = "widget", action = "index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "WidgetPhotoDetailsRoute",
                "widget/PhotoDetails/{id}/{photoID}",
                new { controller = "widget", action = "photodetails", id = UrlParameter.Optional, photoID = UrlParameter.Optional }
            );

            routes.MapRoute(
                "WidgetTweetReplyRoute",
                "widget/TwitterReply/{id}/{twitterid}",
                new { controller = "widget", action = "TwitterReply", id = UrlParameter.Optional, twitterid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "WidgetTweetRetweetRoute",
                "widget/TwitterRetweet/{id}/{twitterid}",
                new { controller = "widget", action = "TwitterRetweet", id = UrlParameter.Optional, twitterid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Epilogger.Web.Controllers" }
            );
        }

        protected void Application_BeginRequest()
        {
            TimeZoneManager.UpdateTimeZone(this.Request);
        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            RegisterAutomapperMappings();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //Wire up the date-time model-binders
            ModelBinders.Binders[typeof(DateTime)] = new DateTimeModelBinder();
            ModelBinders.Binders[typeof(DateTime?)] = new DateTimeModelBinder();
            //Wire up the Metadata provider
            ModelMetadataProviders.Current = new DateTimeMetadataProvider();
        }

        private void RegisterAutomapperMappings() {
            Mapper.CreateMap<Tweet, TweetDisplayViewModel>();

            Mapper.CreateMap<Event, EventDisplayViewModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.CheckIns, opt => opt.Ignore())
                .ForMember(dest => dest.Tweets, opt => opt.Ignore())
                .ForMember(dest => dest.ExternalLinks, opt => opt.Ignore())
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
                .ForMember(dest => dest.TweetCount, opt => opt.Ignore())
                .ForMember(dest => dest.ImageCount, opt => opt.Ignore())
                .ForMember(dest => dest.CheckInCount, opt => opt.Ignore())
                .ForMember(dest => dest.HasSubscribed, opt => opt.Ignore())
                .ForMember(dest => dest.HasUserRated, opt => opt.Ignore())
                .ForMember(dest => dest.EventRatings, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentUserID, opt => opt.Ignore())
                .ForMember(dest => dest.FromDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.ToDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venues.FirstOrDefault()))
                .ForMember(dest => dest.ToolbarViewModel, opt => opt.Ignore())
                .ForMember(dest => dest.CanDelete, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentUserRole, opt => opt.Ignore())
                .ForMember(dest => dest.TheUser, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentUserRole, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedEventUser, opt => opt.Ignore());

            Mapper.CreateMap<Event, WidgetViewModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.CheckIns, opt => opt.Ignore())
                .ForMember(dest => dest.Tweets, opt => opt.Ignore())
                .ForMember(dest => dest.TweetCount, opt => opt.Ignore())
                .ForMember(dest => dest.ImageCount, opt => opt.Ignore())
                .ForMember(dest => dest.CheckInCount, opt => opt.Ignore())
                .ForMember(dest => dest.Width, opt => opt.Ignore())
                .ForMember(dest => dest.Height, opt => opt.Ignore())
                .ForMember(dest => dest.EpiloggerCounts, opt => opt.Ignore())
                .ForMember(dest => dest.CustomSettings, opt => opt.Ignore())
                .ForMember(dest => dest.HeightOffset, opt => opt.Ignore());

            Mapper.CreateMap<Event, WidgetPhotosDetailsViewModel>()
                .ForMember(dest => dest.Width, opt => opt.Ignore())
                .ForMember(dest => dest.Height, opt => opt.Ignore())
                .ForMember(dest => dest.PhotoID, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Tweet, opt => opt.Ignore())
                .ForMember(dest => dest.Returnto, opt => opt.Ignore())
                .ForMember(dest => dest.EpiloggerCounts, opt => opt.Ignore())
                .ForMember(dest => dest.CustomSettings, opt => opt.Ignore())
                .ForMember(dest => dest.HeightOffset, opt => opt.Ignore());


            Mapper.CreateMap<Event, WidgetPhotosViewModel>()
                .ForMember(dest => dest.Width, opt => opt.Ignore())
                .ForMember(dest => dest.Height, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.EpiloggerCounts, opt => opt.Ignore())
                .ForMember(dest => dest.CustomSettings, opt => opt.Ignore())
                .ForMember(dest => dest.HeightOffset, opt => opt.Ignore());

            Mapper.CreateMap<Event, WidgetTweetsViewModel>()
                .ForMember(dest => dest.Width, opt => opt.Ignore())
                .ForMember(dest => dest.Height, opt => opt.Ignore())
                .ForMember(dest => dest.Tweets, opt => opt.Ignore())
                .ForMember(dest => dest.EpiloggerCounts, opt => opt.Ignore())
                .ForMember(dest => dest.CustomSettings, opt => opt.Ignore())
                .ForMember(dest => dest.HeightOffset, opt => opt.Ignore());


            Mapper.CreateMap<Event, WidgetCheckinsViewModel>()
                .ForMember(dest => dest.Width, opt => opt.Ignore())
                .ForMember(dest => dest.Height, opt => opt.Ignore())
                .ForMember(dest => dest.Checkins, opt => opt.Ignore())
                .ForMember(dest => dest.EpiloggerCounts, opt => opt.Ignore())
                .ForMember(dest => dest.CustomSettings, opt => opt.Ignore())
                .ForMember(dest => dest.HeightOffset, opt => opt.Ignore());

            Mapper.CreateMap<Event, WidgetTweetReplyViewModel>()
                .ForMember(dest => dest.Width, opt => opt.Ignore())
                .ForMember(dest => dest.Height, opt => opt.Ignore())
                .ForMember(dest => dest.EpiloggerCounts, opt => opt.Ignore())
                .ForMember(dest => dest.CustomSettings, opt => opt.Ignore())
                .ForMember(dest => dest.HeightOffset, opt => opt.Ignore())
                .ForMember(dest => dest.IsTwitterAuthed, opt => opt.Ignore())
                .ForMember(dest => dest.ReplyNewTweet, opt => opt.Ignore())
                .ForMember(dest => dest.ShortEventURL, opt => opt.Ignore())
                .ForMember(dest => dest.Tweet, opt => opt.Ignore())
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ReturnUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Returnto, opt => opt.Ignore());


            Mapper.CreateMap<Event, AllStatsViewModel>()
                .ForMember(dest => dest.CurrentPageIndex, opt => opt.Ignore())
                .ForMember(dest => dest.TotalRecords, opt => opt.Ignore())
                .ForMember(dest => dest.FromDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.ToDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.ImageCount, opt => opt.Ignore())
                .ForMember(dest => dest.TweetCount, opt => opt.Ignore())
                .ForMember(dest => dest.ExternalLinkCount, opt => opt.Ignore())
                .ForMember(dest => dest.TopImages, opt => opt.Ignore())
                .ForMember(dest => dest.MyUTCNow, opt => opt.Ignore())
                .ForMember(dest => dest.TopTweeters, opt => opt.Ignore())
                .ForMember(dest => dest.AllCheckIns, opt => opt.Ignore())
                .ForMember(dest => dest.TopLinks, opt => opt.Ignore())
                .ForMember(dest => dest.ToolbarViewModel, opt => opt.Ignore());

            Mapper.CreateMap<BetaSignup, BetaSignUpViewModel>()
                .ForMember(dest => dest.Activity, opt => opt.Ignore())
                .ForMember(dest => dest.TotalRecords, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentPageIndex, opt => opt.Ignore())
                .ForMember(dest => dest.PageSize, opt => opt.Ignore())
                .ForMember(dest => dest.TotalRecordsLabel, opt => opt.Ignore())
                .ForMember(dest => dest.Submitted, opt => opt.Ignore())
                .ForMember(dest => dest.ipAddress, opt => opt.MapFrom(t => t.IPAddress))
                .ForMember(dest => dest.regDateTime, opt => opt.MapFrom(t => t.DateTimeSubmitted));

            Mapper.CreateMap<BetaSignUpViewModel, BetaSignup>()
                .ForMember(dest => dest.DateTimeSubmitted, opt => opt.MapFrom(t => t.regDateTime))
                .ForMember(dest => dest.IPAddress, opt => opt.MapFrom(t => t.ipAddress))
                .ForMember(dest => dest.ID, opt => opt.Ignore());


            Mapper.CreateMap<Event, AllContentViewModel>();
            
            Mapper.CreateMap<Event, AllLinksViewModel>()
                .ForMember(dest => dest.CurrentPageIndex, opt => opt.Ignore())
                .ForMember(dest => dest.TotalRecords, opt => opt.Ignore())
                .ForMember(dest => dest.Links, opt => opt.Ignore())
                .ForMember(dest => dest.ToolbarViewModel, opt => opt.Ignore());

            Mapper.CreateMap<CheckIn, CheckinDisplayViewModel>()
                .ForMember(dest =>dest.Tweet, opt => opt.MapFrom(src => src.Tweets.FirstOrDefault()));
            
            Mapper.CreateMap<BlogPost, BlogPostDisplayViewModel>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.BlogURL));
            
            Mapper.CreateMap<AddBlogPostViewModel, BlogPost>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.UserID, opt => opt.Ignore())
                .ForMember(dest => dest.EventID, opt => opt.Ignore())
                .ForMember(dest => dest.Thumbnail, opt => opt.Ignore())
                .ForMember(dest => dest.DateTime, opt => opt.UseValue(DateTime.Now));



            Mapper.CreateMap<Event, AllBlogPostsViewModel>()
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentPageIndex, opt => opt.Ignore())
                .ForMember(dest => dest.PageSize, opt => opt.Ignore())
                .ForMember(dest => dest.TotalRecords, opt => opt.Ignore())
                .ForMember(dest => dest.ToolbarViewModel, opt => opt.Ignore());



            Mapper.CreateMap<CreateAccountModel, User>()
                .ForMember(dest => dest.RoleID, opt => opt.UseValue(2))
                .ForMember(dest => dest.ForgotPasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordHelpers.EncryptPassword(src.Password)))
                .ForMember(dest => dest.IsActive, opt => opt.UseValue(false))
                .ForMember(dest => dest.CreatedDate, opt => opt.UseValue(DateTime.UtcNow))
                //.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.Parse(src.DateOfBirth)))
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfileImage))
                .ForMember(dest => dest.TimeZoneOffSet, opt => opt.Ignore())
                .ForMember(dest => dest.EmailVerified, opt => opt.UseValue(false))
                .ForMember(dest => dest.ProfilePictureLarge, opt => opt.Ignore());

            Mapper.CreateMap<UserAuthenticationProfile, ConnectedNetworksViewModel>();

            Mapper.CreateMap<User, AccountModel>()
                .ForMember(dest => dest.ConnectedNetworks, opt => opt.Ignore())
                //.ForMember(dest => dest.TimeZone, opt => opt.Ignore())
                .ForMember(dest => dest.TwitterProfilePicture, opt => opt.ResolveUsing<TwitterProfilePictureResolver>())
                .ForMember(dest => dest.FacebookProfilePicture, opt => opt.ResolveUsing<FacebookProfilePictureResolver>())
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToShortDateString() : ""))
                .ForMember(dest => dest.facebookUser, opt => opt.Ignore())
                .ForMember(dest => dest.twitterUser, opt => opt.Ignore());

            Mapper.CreateMap<CreateEventViewModel, Event>()
                .ForMember(dest => dest.EventStatus, opt => opt.UseValue(1))
                .ForMember(dest => dest.NumberOfTweets, opt => opt.UseValue(0))
                .ForMember(dest => dest.IsPrivate, opt => opt.UseValue(false))
                .ForMember(dest => dest.IsAdult, opt => opt.UseValue(false))
                .ForMember(dest => dest.IsActive, opt => opt.UseValue(true))
                .ForMember(dest => dest.CollectionMode, opt => opt.UseValue(2))
                .ForMember(dest => dest.IsFeatured, opt => opt.UseValue(false))
                .ForMember(dest => dest.FeaturedStartDateTime, opt => opt.UseValue(DateTime.Parse("01/01/1800")))
                .ForMember(dest => dest.FeaturedEndDateTime, opt => opt.UseValue(DateTime.Parse("01/01/1800")))
                .ForMember(dest => dest.EventBrightUrl, opt => opt.Ignore());

            Mapper.CreateMap<User, DashboardProfileViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => string.Format("{0} {1}", src.FirstName, src.LastName)))
                //.ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.Events.OrderByDescending(e => e.StartDateTime).ToList()));
                .ForMember(dest => dest.Events, opt => opt.Ignore());

            Mapper.CreateMap<Event, DashboardEventViewModel>()
                .ForMember(dest => dest.EventCategoryID, opt => opt.MapFrom(src => src.EventCategories.FirstOrDefault().ID))
                .ForMember(dest => dest.TotalTweets, opt => opt.MapFrom(src => src.Tweets.Count()))
                .ForMember(dest => dest.TotalMedia, opt => opt.MapFrom(src => src.Images.Count()));
            
            Mapper.CreateMap<Event, AllPhotosDisplayViewModel>()
                .ForMember(dest => dest.PhotoCount, opt => opt.Ignore())
                .ForMember(dest => dest.Page, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentPageIndex, opt => opt.Ignore())
                .ForMember(dest => dest.ShowTopPhotos, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.TopImages, opt => opt.Ignore())
                .ForMember(dest => dest.ToolbarViewModel, opt => opt.Ignore())
                .ForMember(dest => dest.CanDelete, opt => opt.Ignore());

            Mapper.CreateMap<Event, AllTweetsDisplayViewModel>()
                .ForMember(dest => dest.TweetCount, opt => opt.Ignore())
                .ForMember(dest => dest.Page, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentPageIndex, opt => opt.Ignore())
                .ForMember(dest => dest.ShowTopTweets, opt => opt.Ignore())
                .ForMember(dest => dest.Tweets, opt => opt.Ignore())
                .ForMember(dest => dest.UniqueTweeterCount, opt => opt.Ignore())
                .ForMember(dest => dest.TopTweeters, opt => opt.Ignore())
                .ForMember(dest => dest.ToolbarViewModel, opt => opt.Ignore())
                .ForMember(dest => dest.CanDelete, opt => opt.Ignore());
            


            Mapper.CreateMap<Event, CreateEventViewModel>()
                .ForMember(dest => dest.FoursquareVenueID, opt => opt.MapFrom(src => src.VenueID.HasValue ? src.Venues.FirstOrDefault().FoursquareVenueID : string.Empty))
                .ForMember(dest => dest.TimeZones, opt => opt.ResolveUsing<TimezonesResolver>())
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venues.FirstOrDefault()))
                .ForMember(dest => dest.ToolbarViewModel, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentUserID, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentUserRole, opt => opt.Ignore());
                //.ForMember(dest => dest.CollectionStartDateTime, opt => opt.MapFrom(src => src.CollectionStartDateTime.ToUserTimeZone(src.TimeZoneOffset.HasValue ? src.TimeZoneOffset.Value : int.Parse("-5"))))
                //.ForMember(dest => dest.CollectionEndDateTime, opt => opt.MapFrom(src => src.CollectionEndDateTime.HasValue ? src.CollectionEndDateTime.Value.ToUserTimeZone(src.TimeZoneOffset.HasValue ? src.TimeZoneOffset.Value : int.Parse("-5")) : src.CollectionEndDateTime))
                //.ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime.ToUserTimeZone(src.TimeZoneOffset.HasValue ? src.TimeZoneOffset.Value : int.Parse("-5"))))
                //.ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime.HasValue ? src.EndDateTime.Value.ToUserTimeZone(src.TimeZoneOffset.HasValue ? src.TimeZoneOffset.Value : int.Parse("-5")) : src.EndDateTime));

            Mapper.CreateMap<AddLinkViewModel, URL>()
                .ForMember(dest => dest.DateTime, opt => opt.Ignore())
                .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleteVoteCount, opt => opt.Ignore())
                .ForMember(dest => dest.Events, opt => opt.Ignore())
                .ForMember(dest => dest.FullURL, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.ShortURL, opt => opt.Ignore())
                .ForMember(dest => dest.TweetID, opt => opt.Ignore())
                .ForMember(dest => dest.Tweets, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.EventID, opt => opt.Ignore());


            //Mapper.CreateMap<Tweet, TweetTemplateViewModel>()
            //    .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventID))
            //    .ForMember(dest => dest.Favorited, opt => opt.UseValue(false))
            //    .ForMember(dest => dest.ModifyDisplayClass, opt => opt.Ignore())
            //    .ForMember(dest => dest.Replied, opt => opt.UseValue(false))
            //    .ForMember(dest => dest.Retweeted, opt => opt.UseValue(false))
            //    .ForMember(dest => dest.ShowControls, opt => opt.UseValue(false))
            //    .ForMember(dest => dest.Tweet, opt => opt.MapFrom(src => src));


            //API Mappers
            Mapper.CreateMap<Event, ApiEvent>()
                .ForMember(dest => dest.CanDelete, opt => opt.Ignore())
                .ForMember(dest => dest.FromDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.ToDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.TweetCount, opt => opt.MapFrom(src => src.Tweets.Count()))
                .ForMember(dest => dest.ImageCount, opt => opt.MapFrom(src => src.Images.Count()))
                .ForMember(dest => dest.CheckInCount, opt => opt.MapFrom(src => src.CheckIns.Count()))
                .ForMember(dest => dest.VenueId, opt => opt.MapFrom(src => src.VenueID))
                .ForMember(dest => dest.WebsiteURL, opt => opt.MapFrom(f => f.WebsiteURL == "http://" ? null : f.WebsiteURL));

            Mapper.CreateMap<EventCategory, ApiCategory>();

            Mapper.CreateMap<TweetsAndImage, ApiTweetsAndImage>();
            
            Mapper.CreateMap<Tweet, ApiTweet>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            Mapper.CreateMap<Tweeter, ApiTweeter>()
                .ForMember(dest => dest.PhotoCount, opt => opt.Ignore());

            //PhotoCount

            Mapper.CreateMap<Image, ApiImage>();
            Mapper.CreateMap<TopImageAndTweet, ApiTopImageAndTweet>();
            Mapper.CreateMap<CheckIn, ApiCheckIn>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.Tweets.FirstOrDefault().ProfileImageURL));

            Mapper.CreateMap<User, ApiUser>()
                .ForMember(dest => dest.MobileServiceUsername, opt => opt.Ignore())
                .ForMember(dest => dest.MobileToken, opt => opt.Ignore())
                .ForMember(dest => dest.MobileTokenSecret, opt => opt.Ignore());


            Mapper.CreateMap<Venue, ApiVenue>();
            Mapper.CreateMap<SearchInEventModel, ApiSearchInEvent>();
            Mapper.CreateMap<ApiUserFollowsEvent, UserFollowsEvent>();
            Mapper.CreateMap<UserFollowsEvent, ApiUserFollowsEvent>();

            Mapper.CreateMap<MemoryBoxItem, ApiMemoryBoxItem>();
            Mapper.CreateMap<ApiMemoryBoxItem, MemoryBoxItem>();
            Mapper.CreateMap<MemoryBox, ApiMemoryBox>();
            Mapper.CreateMap<MemoryBoxTweet, ApiMemoryBoxTweet>();
            
            Mapper.AssertConfigurationIsValid();
        }

        public static string ToPublicUrl(Uri relativeUri) {
            var httpContext = HttpContext.Current;

            var uriBuilder = new UriBuilder {
                Host = httpContext.Request.Url.Host,
                Path = "/",
                Port = 80,
                Scheme = "http",
            };

            if (httpContext.Request.IsLocal) {
                uriBuilder.Port = httpContext.Request.Url.Port;
            }

            return new Uri(uriBuilder.Uri, relativeUri).AbsoluteUri;
        }
    }
}