using System;
using System.Linq;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;

using Epilogger.Data;
using Epilogger.Web.Models;

namespace Epilogger.Web {
    public class App : System.Web.HttpApplication {
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
            Mapper.CreateMap<Event, EventDisplayViewModel>();

            Mapper.CreateMap<CreateAccountModel, User>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.UseValue(true))
                .ForMember(dest => dest.CreatedDate, opt => opt.UseValue(DateTime.Now));
            
            Mapper.CreateMap<AccountModel, User>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            Mapper.CreateMap<User, AccountModel>()
                .ForMember(dest => dest.TwitterUsername, opt => opt.MapFrom(src => src.UserAuthenticationProfiles.Where(ua => ua.Service == AuthenticationServices.TWITTER.ToString()).FirstOrDefault().ServiceUsername))
                .ForMember(dest => dest.FacebookUsername, opt => opt.MapFrom(src => src.UserAuthenticationProfiles.Where(ua => ua.Service == AuthenticationServices.FACEBOOK.ToString()).FirstOrDefault().ServiceUsername));

            Mapper.AssertConfigurationIsValid();
        }
    }
}