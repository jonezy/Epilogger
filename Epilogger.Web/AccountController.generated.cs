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
    public partial class AccountController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AccountController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AccountController(Dummy d) { }

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
        public System.Web.Mvc.ActionResult Validate() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Validate);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult TwitterLogon() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.TwitterLogon);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AccountController Actions { get { return MVC.Account; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Account";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Account";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string Create = "Create";
            public readonly string Validate = "Validate";
            public readonly string Login = "Login";
            public readonly string TwitterLogon = "TwitterLogon";
            public readonly string Logout = "Logout";
            public readonly string ForgotPassword = "ForgotPassword";
            public readonly string ResetPassword = "ResetPassword";
            public readonly string UpdatePassword = "UpdatePassword";
            public readonly string AccountActivationNeeded = "AccountActivationNeeded";
            public readonly string ActivationSent = "ActivationSent";
            public readonly string TwitterAuthTest = "TwitterAuthTest";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string Index = "Index";
            public const string Create = "Create";
            public const string Validate = "Validate";
            public const string Login = "Login";
            public const string TwitterLogon = "TwitterLogon";
            public const string Logout = "Logout";
            public const string ForgotPassword = "ForgotPassword";
            public const string ResetPassword = "ResetPassword";
            public const string UpdatePassword = "UpdatePassword";
            public const string AccountActivationNeeded = "AccountActivationNeeded";
            public const string ActivationSent = "ActivationSent";
            public const string TwitterAuthTest = "TwitterAuthTest";
        }


        static readonly ActionParamsClass_Validate s_params_Validate = new ActionParamsClass_Validate();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Validate ValidateParams { get { return s_params_Validate; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Validate {
            public readonly string validationCode = "validationCode";
        }
        static readonly ActionParamsClass_TwitterLogon s_params_TwitterLogon = new ActionParamsClass_TwitterLogon();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_TwitterLogon TwitterLogonParams { get { return s_params_TwitterLogon; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_TwitterLogon {
            public readonly string oauth_token = "oauth_token";
            public readonly string oauth_verifier = "oauth_verifier";
            public readonly string ReturnUrl = "ReturnUrl";
        }
        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string AccountActivationNeeded = "~/Views/Account/AccountActivationNeeded.cshtml";
            public readonly string ActivationSent = "~/Views/Account/ActivationSent.cshtml";
            public readonly string Create = "~/Views/Account/Create.cshtml";
            public readonly string ForgotPassword = "~/Views/Account/ForgotPassword.cshtml";
            public readonly string Index = "~/Views/Account/Index.cshtml";
            public readonly string Login = "~/Views/Account/Login.cshtml";
            public readonly string Menu = "~/Views/Account/Menu.cshtml";
            public readonly string ResetPassword = "~/Views/Account/ResetPassword.cshtml";
            public readonly string TwitterAuthTest = "~/Views/Account/TwitterAuthTest.cshtml";
            public readonly string UpdatePassword = "~/Views/Account/UpdatePassword.cshtml";
            public readonly string Validate = "~/Views/Account/Validate.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_AccountController: Epilogger.Web.Controllers.AccountController {
        public T4MVC_AccountController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Index() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Index(Epilogger.Web.Models.AccountModel model, System.Web.Mvc.FormCollection c) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            callInfo.RouteValueDictionary.Add("model", model);
            callInfo.RouteValueDictionary.Add("c", c);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Create() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Create);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Create(Epilogger.Web.Models.CreateAccountModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Create);
            callInfo.RouteValueDictionary.Add("model", model);
            callInfo.RouteValueDictionary.Add("c", c);
            return callInfo;
        }

        

        public override System.Web.Mvc.ActionResult Validate(string validationCode) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Validate);
            callInfo.RouteValueDictionary.Add("validationCode", validationCode);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Login() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Login);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Login(Epilogger.Web.Models.LoginModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Login);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult TwitterLogon(string oauth_token, string oauth_verifier, string ReturnUrl) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.TwitterLogon);
            callInfo.RouteValueDictionary.Add("oauth_token", oauth_token);
            callInfo.RouteValueDictionary.Add("oauth_verifier", oauth_verifier);
            callInfo.RouteValueDictionary.Add("ReturnUrl", ReturnUrl);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Logout() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Logout);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ForgotPassword() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ForgotPassword);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ForgotPassword(Epilogger.Web.Models.ForgotPasswordViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ForgotPassword);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ResetPassword() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ResetPassword);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ResetPassword(Epilogger.Web.Models.ResetPasswordViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ResetPassword);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdatePassword() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePassword);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdatePassword(Epilogger.Web.Models.UpdatePasswordModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePassword);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        //public override System.Web.Mvc.ActionResult AccountActivationNeeded() {
        //    var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.AccountActivationNeeded);
        //    return callInfo;
        //}

        public override System.Web.Mvc.ActionResult ActivationSent() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ActivationSent);
            return callInfo;
        }

        //public override System.Web.Mvc.ActionResult TwitterAuthTest() {
        //    var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.TwitterAuthTest);
        //    return callInfo;
        //}

        //public override System.Web.Mvc.ActionResult TwitterAuthTest(Epilogger.Web.Models.TwitterAuthTestViewModel model) {
        //    var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.TwitterAuthTest);
        //    callInfo.RouteValueDictionary.Add("model", model);
        //    return callInfo;
        //}

    }
}

#endregion T4MVC
#pragma warning restore 1591
