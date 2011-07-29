using System.Web.Mvc;
using System;

namespace Epilogger.Web {
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple=true)]
    public class LogErrors : FilterAttribute, IExceptionFilter {
        
        public string FriendlyErrorMessage { get; set; }

        public void OnException(ExceptionContext filterContext) {
            Elmah.ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
            
            Controller controller = (Controller)filterContext.Controller;
            controller.StoreError(FriendlyErrorMessage ?? filterContext.Exception.Message);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
