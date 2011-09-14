using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.Mvc;

using Epilogger.Data;
using Epilogger.Web.Controllers;

using Facebook;

namespace Epilogger.Web.Areas.Authentication.Controllers {
    public class FacebookController : BaseController, IAuthenticationController {
        private string logoffUrl = ConfigurationManager.AppSettings["FacebookLogoffURL"] as string ?? "";
        private string returnUrl = ConfigurationManager.AppSettings["FacebookCallbackURL"] as string ?? "";

        public ActionResult ConnectRequest() {
            return Redirect(BuildRequest(returnUrl));
        }

        public ActionResult ConnectAccount() {
            string code = Request.QueryString["code"] as string ?? "";
            string state = Request.QueryString["state"] as string ?? "";

            FacebookOAuthResult oauthResult;
            FacebookOAuthResult.TryParse(Request.Url, out oauthResult);

            if (oauthResult.IsSuccess) {
                string accessToken = GetAccessToken(code, returnUrl);
                FacebookClient fbClient = new FacebookClient(accessToken);
                dynamic me = fbClient.Get("me");

                UserAuthenticationProfileService authorizationService = new UserAuthenticationProfileService();
                UserAuthenticationProfile userAuth = authorizationService.UserAuthorizationByServiceScreenName(me.username);

                if (userAuth != null) {
                    userAuth.Service = AuthenticationServices.FACEBOOK.ToString();
                    userAuth.Token = accessToken;
                } else {
                    userAuth = new UserAuthenticationProfile();
                    userAuth.UserID = CurrentUserID;
                    userAuth.Service = AuthenticationServices.FACEBOOK.ToString();
                    userAuth.Token = accessToken;
                    userAuth.ServiceUsername = me.username;
                }

                authorizationService.Save(userAuth);
                    
                this.StoreSuccess("Your facebook account has been linked to your epilogger.com account");
                    
                // prevent open redirection attack by checking if the url is local.
                if (Url.IsLocalUrl(state)) {
                    return RedirectToAction("Index", "Account", new { area = "" });
                } else {
                    return RedirectToAction("Index", "Account", new { area = "" });
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Disconnect() {
            UserAuthenticationProfileService service = new UserAuthenticationProfileService();
            service.DisconnectService(AuthenticationServices.FACEBOOK, CurrentUserID);

            this.StoreInfo("Your facebook account has been disconnected from your epiloggerepilogger.com account");

            return Redirect(Request.UrlReferrer.ToString());
        }

        private string BuildRequest(string returnUrl) {
            var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
            oAuthClient.RedirectUri = new Uri(returnUrl);
            var extendedPermissions = new[] { "offline_access"};
            var scope = new StringBuilder();
            scope.Append(string.Join(",", extendedPermissions));

            var parameters = new Dictionary<string, object> { { "state", returnUrl } };
            parameters["scope"] = scope.ToString();

            var loginUri = oAuthClient.GetLoginUrl(parameters);

            return loginUri.AbsoluteUri;
        }

        private string GetAccessToken(string code, string returnUrl) {
            var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
            oAuthClient.RedirectUri = new Uri(returnUrl);
            dynamic tokenResult = oAuthClient.ExchangeCodeForAccessToken(code);

            return tokenResult.access_token;
        }
    }
}
