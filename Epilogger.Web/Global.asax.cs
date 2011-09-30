using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Core.Email;
using Epilogger.Web.Models;

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

            routes.MapRoute(
                "VenueSearch",
                "events/venuesearch",
                new { controller = "events", action = "venuesearch" }
            );

            routes.MapRoute(
                "GetLatestTweets",
                "Events/GetLastTweetsJSON",
                new { controller = "events", action = "GetLastTweetsJSON" }
            );

            routes.MapRoute(
                "CreateEvent",
                "events/create",
                new { controller = "events", action = "create"}
            );

            routes.MapRoute(
                "GetImageComments",
                "events/getimagecomments/{id}",
                new { controller = "events", action = "getimagecomments", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                "EventBroseRoutes",
                "events/{filter}",
                new { controller = "events", action = "index", filter = UrlParameter.Optional }
            );

            routes.MapRoute(
                "EventsRoutes",
                "events/{id}/{action}",
                new { controller = "events", action = "index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            RegisterAutomapperMappings();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
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
                .ForMember(dest => dest.ToDateTime, opt => opt.Ignore());

            Mapper.CreateMap<Event, AllContentViewModel>();
            Mapper.CreateMap<Event, AllBlogPostsViewModel>();
            Mapper.CreateMap<Event, AllLinksViewModel>()
                .ForMember(dest => dest.CurrentPageIndex, opt => opt.Ignore())
                .ForMember(dest => dest.TotalRecords, opt => opt.Ignore())
                .ForMember(dest => dest.Links, opt => opt.Ignore());

            Mapper.CreateMap<CheckIn, CheckinDisplayViewModel>()
                .ForMember(dest =>dest.Tweet, opt => opt.MapFrom(src => src.Tweets.FirstOrDefault()));

            Mapper.CreateMap<CreateAccountModel, User>()
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                .ForMember(dest => dest.LastName, opt => opt.Ignore())
                .ForMember(dest => dest.ForgotPasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordHelpers.EncryptPassword(src.Password)))
                .ForMember(dest => dest.IsActive, opt => opt.UseValue(true))
                .ForMember(dest => dest.CreatedDate, opt => opt.UseValue(DateTime.UtcNow))
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.Parse(src.DateOfBirth)));

            Mapper.CreateMap<UserAuthenticationProfile, ConnectedNetworksViewModel>();

            Mapper.CreateMap<User, AccountModel>()
                .ForMember(dest => dest.ConnectedNetworks, opt => opt.Ignore())
                .ForMember(dest => dest.TimeZone, opt => opt.MapFrom(src => src.TimeZoneOffSet))
                .ForMember(dest => dest.TwitterProfilePicture, opt => opt.ResolveUsing<TwitterProfilePictureResolver>())
                .ForMember(dest => dest.FacebookProfilePicture, opt => opt.ResolveUsing<FacebookProfilePictureResolver>())
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToShortDateString() : ""))
                .ForMember(dest => dest.TimeZones, opt => opt.ResolveUsing<UserTimezonesResolver>());

            Mapper.CreateMap<CreateEventViewModel, Event>()
                .ForMember(dest => dest.CategoryID, opt => opt.UseValue(19))
                .ForMember(dest => dest.VenueID, opt => opt.Ignore())
                .ForMember(dest => dest.EventStatus, opt => opt.UseValue(1))
                .ForMember(dest => dest.NumberOfTweets, opt => opt.UseValue(0))
                .ForMember(dest => dest.IsPrivate, opt => opt.UseValue(false))
                .ForMember(dest => dest.IsAdult, opt => opt.UseValue(false))
                .ForMember(dest => dest.IsActive, opt => opt.UseValue(true))
                .ForMember(dest => dest.CollectionMode, opt => opt.UseValue(2));

            Mapper.CreateMap<User, DashboardProfileViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => string.Format("{0} {1}", src.FirstName, src.LastName)))
                //.ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.Events.OrderByDescending(e => e.StartDateTime).ToList()));
                .ForMember(dest => dest.Events, opt => opt.Ignore());

            Mapper.CreateMap<Event, DashboardEventViewModel>()
                .ForMember(dest => dest.TotalTweets, opt => opt.MapFrom(src => src.Tweets.Count()))
                .ForMember(dest => dest.TotalMedia, opt => opt.MapFrom(src => src.Images.Count()));
            
            Mapper.CreateMap<Event, AllPhotosDisplayViewModel>()
                .ForMember(dest => dest.PhotoCount, opt => opt.Ignore())
                .ForMember(dest => dest.Page, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentPageIndex, opt => opt.Ignore())
                .ForMember(dest => dest.ShowTopPhotos, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            Mapper.CreateMap<Event, AllTweetsDisplayViewModel>()
                .ForMember(dest => dest.TweetCount, opt => opt.Ignore())
                .ForMember(dest => dest.Page, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentPageIndex, opt => opt.Ignore())
                .ForMember(dest => dest.ShowTopTweets, opt => opt.Ignore())
                .ForMember(dest => dest.Tweets, opt => opt.Ignore())
                .ForMember(dest => dest.UniqueTweeterCount, opt => opt.Ignore())
                .ForMember(dest => dest.TopTweeters, opt => opt.Ignore());


            Mapper.CreateMap<Event, CreateEventViewModel>()
                .ForMember(dest => dest.TimeZones, opt => opt.ResolveUsing<TimezonesResolver>())
                .ForMember(dest => dest.CollectionStartDateTime, opt => opt.MapFrom(src => src.CollectionStartDateTime.ToUserTimeZone(src.TimeZoneOffset.HasValue ? src.TimeZoneOffset.Value : int.Parse("-5"))))
                .ForMember(dest => dest.CollectionEndDateTime, opt => opt.MapFrom(src => src.CollectionEndDateTime.HasValue ? src.CollectionEndDateTime.Value.ToUserTimeZone(src.TimeZoneOffset.HasValue ? src.TimeZoneOffset.Value : int.Parse("-5")) : src.CollectionEndDateTime))
                .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime.ToUserTimeZone(src.TimeZoneOffset.HasValue ? src.TimeZoneOffset.Value : int.Parse("-5"))))
                .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime.HasValue ? src.EndDateTime.Value.ToUserTimeZone(src.TimeZoneOffset.HasValue ? src.TimeZoneOffset.Value : int.Parse("-5")) : src.EndDateTime));

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