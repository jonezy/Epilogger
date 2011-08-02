using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

using Epilogger.Data;
using Epilogger.Web.Controllers;

using Facebook;

namespace Epilogger.Web.Areas.Authentication.Controllers {
    public class FacebookController : BaseController {
        private string logoffUrl = ConfigurationManager.AppSettings["FacebookLogoffURL"] as string ?? "";
        private string returnUrl = ConfigurationManager.AppSettings["FacebookCallbackURL"] as string ?? "";

        [HttpPost]
        public ActionResult BuildAuthenticationRequest() {
            var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
            oAuthClient.RedirectUri = new Uri(returnUrl);

            var loginUri = oAuthClient.GetLoginUrl(new Dictionary<string, object> { { "state", returnUrl } });

            return Redirect(loginUri.AbsoluteUri);
        }

        public ActionResult ProcessResponse(string code, string state) {
            FacebookOAuthResult oauthResult;
            if (FacebookOAuthResult.TryParse(Request.Url, out oauthResult)) {
                if (oauthResult.IsSuccess) {
                    var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
                    oAuthClient.RedirectUri = new Uri(returnUrl);
                    dynamic tokenResult = oAuthClient.ExchangeCodeForAccessToken(code);
                    string accessToken = tokenResult.access_token;

                    DateTime expiresOn = DateTime.MaxValue;

                    if (tokenResult.ContainsKey("expires")) {
                        DateTimeConvertor.FromUnixTime(tokenResult.expires);
                    }

                    FacebookClient fbClient = new FacebookClient(accessToken);
                    dynamic me = fbClient.Get("me?fields=id,name");
                    long facebookId = Convert.ToInt64(me.id);

                    UserAuthenticationProfileService userAuthService = new UserAuthenticationProfileService();
                    UserAuthenticationProfile fbAuthorization = userAuthService.UserAuthorizationByServiceScreenName(me.name);
                    if (fbAuthorization != null) {
                        fbAuthorization.Service = AuthenticationServices.FACEBOOK.ToString();
                        fbAuthorization.Token = accessToken;
                    } else {
                        fbAuthorization = new UserAuthenticationProfile();
                        fbAuthorization.UserID = CurrentUserID;
                        fbAuthorization.Service = AuthenticationServices.FACEBOOK.ToString();
                        fbAuthorization.Token = accessToken;
                        fbAuthorization.ServiceUsername = me.name;
                    }

                    userAuthService.Save(fbAuthorization);

                    // prevent open redirection attack by checking if the url is local.
                    if (Url.IsLocalUrl(state)) {
                        return Redirect(state);
                    } else {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return RedirectToAction("Index", "Account", new { area = "" });
        }

        public ActionResult Disconnect() {
            UserAuthenticationProfileService service = new UserAuthenticationProfileService();
            service.DisconnectService(AuthenticationServices.FACEBOOK, CurrentUserID);

            return RedirectToAction("Index", "Account", new { area = "" });
        }
    }
}
