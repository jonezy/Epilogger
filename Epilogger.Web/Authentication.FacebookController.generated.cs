// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Epilogger.Web.Areas.Authentication.Controllers {
    public partial class FacebookController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public FacebookController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected FacebookController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ConnectRequestWithCallback() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ConnectRequestWithCallback);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public FacebookController Actions { get { return MVC.Authentication.Facebook; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Authentication";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Facebook";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Facebook";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string ConnectRequest = "ConnectRequest";
            public readonly string ConnectRequestWithCallback = "ConnectRequestWithCallback";
            public readonly string ConnectAccount = "ConnectAccount";
            public readonly string Disconnect = "Disconnect";
            public readonly string DisconnectClean = "DisconnectClean";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string ConnectRequest = "ConnectRequest";
            public const string ConnectRequestWithCallback = "ConnectRequestWithCallback";
            public const string ConnectAccount = "ConnectAccount";
            public const string Disconnect = "Disconnect";
            public const string DisconnectClean = "DisconnectClean";
        }


        static readonly ActionParamsClass_ConnectRequestWithCallback s_params_ConnectRequestWithCallback = new ActionParamsClass_ConnectRequestWithCallback();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ConnectRequestWithCallback ConnectRequestWithCallbackParams { get { return s_params_ConnectRequestWithCallback; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ConnectRequestWithCallback {
            public readonly string callBackUrl = "callBackUrl";
        }
        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_FacebookController: Epilogger.Web.Areas.Authentication.Controllers.FacebookController {
        public T4MVC_FacebookController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult ConnectRequest() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ConnectRequest);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ConnectRequestWithCallback(string callBackUrl) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ConnectRequestWithCallback);
            callInfo.RouteValueDictionary.Add("callBackUrl", callBackUrl);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ConnectAccount() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ConnectAccount);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Disconnect() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Disconnect);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult DisconnectClean() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.DisconnectClean);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
