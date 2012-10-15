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
    public partial class HomeController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected HomeController(Dummy d) { }

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
        public System.Web.Mvc.ActionResult LogClick() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.LogClick);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ClickMap() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ClickMap);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult _GetClickMap() {
            return new T4MVC_ActionResult(Area, Name, ActionNames._GetClickMap);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult SocialBar() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.SocialBar);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController Actions { get { return MVC.Home; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Home";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Home";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string StatusMessages = "StatusMessages";
            public readonly string LogClick = "LogClick";
            public readonly string ClickMap = "ClickMap";
            public readonly string _GetClickMap = "_GetClickMap";
            public readonly string Search = "Search";
            public readonly string About = "About";
            public readonly string Contact = "Contact";
            public readonly string Terms = "Terms";
            public readonly string Privacy = "Privacy";
            public readonly string SocialBar = "SocialBar";
            public readonly string WebStatus = "WebStatus";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string Index = "Index";
            public const string StatusMessages = "StatusMessages";
            public const string LogClick = "LogClick";
            public const string ClickMap = "ClickMap";
            public const string _GetClickMap = "_GetClickMap";
            public const string Search = "Search";
            public const string About = "About";
            public const string Contact = "Contact";
            public const string Terms = "Terms";
            public const string Privacy = "Privacy";
            public const string SocialBar = "SocialBar";
            public const string WebStatus = "WebStatus";
        }


        static readonly ActionParamsClass_LogClick s_params_LogClick = new ActionParamsClass_LogClick();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_LogClick LogClickParams { get { return s_params_LogClick; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_LogClick {
            public readonly string clickactions = "clickactions";
        }
        static readonly ActionParamsClass_ClickMap s_params_ClickMap = new ActionParamsClass_ClickMap();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ClickMap ClickMapParams { get { return s_params_ClickMap; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ClickMap {
            public readonly string clickactions = "clickactions";
        }
        static readonly ActionParamsClass__GetClickMap s_params__GetClickMap = new ActionParamsClass__GetClickMap();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass__GetClickMap _GetClickMapParams { get { return s_params__GetClickMap; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass__GetClickMap {
            public readonly string clickactions = "clickactions";
        }
        static readonly ActionParamsClass_SocialBar s_params_SocialBar = new ActionParamsClass_SocialBar();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SocialBar SocialBarParams { get { return s_params_SocialBar; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SocialBar {
            public readonly string eventid = "eventid";
            public readonly string photoid = "photoid";
        }
        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string _GetClickMap = "~/Views/Home/_GetClickMap.cshtml";
            public readonly string About = "~/Views/Home/About.cshtml";
            public readonly string BetaSignUp = "~/Views/Home/BetaSignUp.cshtml";
            public readonly string Contact = "~/Views/Home/Contact.cshtml";
            public readonly string Copy_of_Index = "~/Views/Home/Copy of Index.cshtml";
            public readonly string Index = "~/Views/Home/Index.cshtml";
            public readonly string Privacy = "~/Views/Home/Privacy.cshtml";
            public readonly string Search = "~/Views/Home/Search.cshtml";
            public readonly string SocialBar = "~/Views/Home/SocialBar.cshtml";
            public readonly string StatusMessages = "~/Views/Home/StatusMessages.cshtml";
            public readonly string Terms = "~/Views/Home/Terms.cshtml";
            public readonly string WebStatus = "~/Views/Home/WebStatus.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_HomeController: Epilogger.Web.Controllers.HomeController {
        public T4MVC_HomeController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Index() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult StatusMessages() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.StatusMessages);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult StatusMessages(Epilogger.Data.StatusMessage model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.StatusMessages);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult LogClick(Epilogger.Data.UserClickAction clickactions) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.LogClick);
            callInfo.RouteValueDictionary.Add("clickactions", clickactions);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ClickMap(Epilogger.Data.userClickTracking clickactions) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ClickMap);
            callInfo.RouteValueDictionary.Add("clickactions", clickactions);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult _GetClickMap(Epilogger.Data.userClickTracking clickactions) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames._GetClickMap);
            callInfo.RouteValueDictionary.Add("clickactions", clickactions);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Search() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Search);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Search(Epilogger.Web.Models.SearchEventViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Search);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult About() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.About);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Contact() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Contact);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Terms() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Terms);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Privacy() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Privacy);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SocialBar(int eventid, int photoid) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SocialBar);
            callInfo.RouteValueDictionary.Add("eventid", eventid);
            callInfo.RouteValueDictionary.Add("photoid", photoid);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult WebStatus() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.WebStatus);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
