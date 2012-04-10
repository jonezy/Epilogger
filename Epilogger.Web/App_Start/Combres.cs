[assembly: WebActivator.PreApplicationStartMethod(typeof(Epilogger.Web.App_Start.Combres), "PreStart")]
namespace Epilogger.Web.App_Start {
	using System.Web.Routing;
	using global::Combres;
	
    public static class Combres {
        public static void PreStart() {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}