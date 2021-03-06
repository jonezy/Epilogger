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
namespace Epilogger.Web.Controllers {
    public partial class DashboardController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public DashboardController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected DashboardController(Dummy d) { }

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
        public System.Web.Mvc.ActionResult Index() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Index);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Events() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Events);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult AllEvents() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.AllEvents);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Subscriptions() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Subscriptions);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public DashboardController Actions { get { return MVC.Dashboard; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Dashboard";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Dashboard";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string Profile = "Profile";
            public readonly string Events = "Events";
            public readonly string AllEvents = "AllEvents";
            public readonly string Subscriptions = "Subscriptions";
            public readonly string Account = "Account";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string Index = "Index";
            public const string Profile = "Profile";
            public const string Events = "Events";
            public const string AllEvents = "AllEvents";
            public const string Subscriptions = "Subscriptions";
            public const string Account = "Account";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index {
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_Events s_params_Events = new ActionParamsClass_Events();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Events EventsParams { get { return s_params_Events; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Events {
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_AllEvents s_params_AllEvents = new ActionParamsClass_AllEvents();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AllEvents AllEventsParams { get { return s_params_AllEvents; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AllEvents {
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_Subscriptions s_params_Subscriptions = new ActionParamsClass_Subscriptions();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Subscriptions SubscriptionsParams { get { return s_params_Subscriptions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Subscriptions {
            public readonly string page = "page";
        }
        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string _EventListItem = "~/Views/Dashboard/_EventListItem.cshtml";
            public readonly string Account = "~/Views/Dashboard/Account.cshtml";
            public readonly string AllEvents = "~/Views/Dashboard/AllEvents.cshtml";
            public readonly string Events = "~/Views/Dashboard/Events.cshtml";
            public readonly string Index = "~/Views/Dashboard/Index.cshtml";
            public readonly string Menu = "~/Views/Dashboard/Menu.cshtml";
            public readonly string Profile = "~/Views/Dashboard/Profile.cshtml";
            public readonly string Subscriptions = "~/Views/Dashboard/Subscriptions.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_DashboardController: Epilogger.Web.Controllers.DashboardController {
        public T4MVC_DashboardController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Index(int? page) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            callInfo.RouteValueDictionary.Add("page", page);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Profile() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Profile);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Events(int? page) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Events);
            callInfo.RouteValueDictionary.Add("page", page);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AllEvents(int? page) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.AllEvents);
            callInfo.RouteValueDictionary.Add("page", page);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Subscriptions(int? page) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Subscriptions);
            callInfo.RouteValueDictionary.Add("page", page);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Account() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Account);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
