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
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

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
            Mapper.CreateMap<Event, EventDisplayViewModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.CheckIns, opt => opt.Ignore())
                .ForMember(dest => dest.Tweets, opt => opt.Ignore())
                .ForMember(dest => dest.ExternalLinks, opt => opt.Ignore())
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
                .ForMember(dest => dest.TweetCount, opt => opt.Ignore());
                

            Mapper.CreateMap<CreateAccountModel, User>()
                .ForMember(dest => dest.ForgotPasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordHelpers.EncryptPassword(src.Password)))
                .ForMember(dest => dest.IsActive, opt => opt.UseValue(true))
                .ForMember(dest => dest.CreatedDate, opt => opt.UseValue(DateTime.UtcNow))
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore())
                .ForMember(dest => dest.TimeZoneOffSet, opt => opt.UseValue(-5));
            
            Mapper.CreateMap<AccountModel, User>()
                .ForMember(dest => dest.ForgotPasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.TimeZoneOffSet, opt => opt.UseValue(-5));

            Mapper.CreateMap<User, AccountModel>()
                .ForMember(dest => dest.TwitterUsername, opt => opt.MapFrom(src => src.UserAuthenticationProfiles.Where(ua => ua.Service == AuthenticationServices.TWITTER.ToString()).FirstOrDefault().ServiceUsername))
                .ForMember(dest => dest.FacebookUsername, opt => opt.MapFrom(src => src.UserAuthenticationProfiles.Where(ua => ua.Service == AuthenticationServices.FACEBOOK.ToString()).FirstOrDefault().ServiceUsername));

            Mapper.CreateMap<CreateEventViewModel, Event>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.SubTitle, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.WebsiteURL, opt => opt.Ignore())
                .ForMember(dest => dest.Cost, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryID, opt => opt.UseValue(19))
                .ForMember(dest => dest.VenueID, opt => opt.Ignore())
                .ForMember(dest => dest.SearchTerms, opt => opt.Ignore())
                .ForMember(dest => dest.EventStatus, opt => opt.UseValue(0))
                .ForMember(dest => dest.NumberOfTweets, opt => opt.UseValue(0))
                .ForMember(dest => dest.IsPrivate, opt => opt.UseValue(false))
                .ForMember(dest => dest.IsAdult, opt => opt.UseValue(0))
                .ForMember(dest => dest.IsActive, opt => opt.UseValue(0))
                .ForMember(dest => dest.CollectionStartDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.CollectionEndDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.CollectionMode, opt => opt.Ignore())
                .ForMember(dest => dest.TwitterAccount, opt => opt.Ignore())
                .ForMember(dest => dest.FacebookPageURL, opt => opt.Ignore());

            Mapper.CreateMap<User, DashboardProfileViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => string.Format("{0} {1}", src.FirstName, src.LastName)))
                .ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.Events.OrderByDescending(e => e.StartDateTime).ToList()));

            Mapper.CreateMap<Event, DashboardEventViewModel>();

            Mapper.CreateMap<Event, AllPhotosDisplayViewModel>()
                .ForMember(dest => dest.PhotoCount, opt => opt.Ignore())
                .ForMember(dest => dest.Page, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore());



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