using System;
using System.Web.Mvc;
using System.Web.Routing;

using Elmah;
using Epilogger.Web.Controllers;

namespace Epilogger.Web {
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class ErrorLogging : ActionFilterAttribute, IExceptionFilter {
        public string FriendlyErrorMessage { get; set; }

        public void OnException(ExceptionContext filterContext) {
            if (!filterContext.HttpContext.IsCustomErrorEnabled)
                return;

            // Log only handled exceptions, because all other will be caught by ELMAH anyway.
            if (!filterContext.ExceptionHandled) {
                ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
                filterContext.ExceptionHandled = true;

                Controller controller = filterContext.Controller as BaseController;
                controller.StoreError("There was a problem performing the operation");
                if(!string.IsNullOrEmpty(this.FriendlyErrorMessage))
                    controller.StoreError(this.FriendlyErrorMessage);
                
                RouteValueDictionary returnRoute = new RouteValueDictionary();
                returnRoute.Add("action", filterContext.RouteData.Values["action"].ToString());
                returnRoute.Add("controller", filterContext.RouteData.Values["controller"].ToString());

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = 500;

                // Certain versions of IIS will sometimes use their own error page when
                // they detect a server error. Setting this property indicates that we
                // want it to try to render ASP.NET MVC's error page instead.
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

                if(filterContext.RequestContext.HttpContext.Request.UrlReferrer != null)
                    filterContext.Result = new RedirectResult(filterContext.RequestContext.HttpContext.Request.UrlReferrer.AbsolutePath);
                else 
                    filterContext.Result = filterContext.Result;
            }
        }
    }

}