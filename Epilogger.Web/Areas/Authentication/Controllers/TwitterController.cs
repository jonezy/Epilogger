using System;
using System.Linq;
using System.Web.Mvc;
using Epilogger.Web.Controllers;
using Epilogger.Data;
using RichmondDay.Helpers;
using Twitterizer;

namespace Epilogger.Web.Areas.Authentication.Controllers {
    public class TwitterController : BaseController, IAuthenticationController {
        public ActionResult ConnectRequest() {
            string requestToken = OAuthUtility.GetRequestToken(TwitterHelper.TwitterConsumerKey, TwitterHelper.TwitterConsumerSecret, TwitterHelper.TwitterCallbackURL).Token;
            Uri authorizationUrl = OAuthUtility.BuildAuthorizationUri(requestToken,false);
            
            return Redirect(authorizationUrl.ToString());
        }

        public ActionResult ConnectAccount() {
            if (Request.QueryString["oauth_token"] != null) {
                OAuthTokenResponse accessTokenResponse = OAuthUtility.GetAccessTokenDuringCallback(TwitterHelper.TwitterConsumerKey, TwitterHelper.TwitterConsumerSecret);
                // check to see if we already have the screen name in the userauth table.
                UserService userService = new UserService();
                UserAuthenticationProfileService userAuthService = new UserAuthenticationProfileService();
                User user = null;
                UserAuthenticationProfile userAuth = userAuthService.UserAuthorizationByServiceScreenName(accessTokenResponse.ScreenName);

                if (userAuth != null) {
                    userAuth.Token = accessTokenResponse.Token;
                    userAuth.TokenSecret = accessTokenResponse.TokenSecret;
                    userAuthService.Save(userAuth);

                    user = userAuth.Users.FirstOrDefault();
                } else {
                    userAuth = new UserAuthenticationProfile();
                    userAuth.UserID = CurrentUserID;
                    userAuth.Service = AuthenticationServices.TWITTER.ToString();
                    userAuth.ServiceUsername = accessTokenResponse.ScreenName;
                    userAuth.Token = accessTokenResponse.Token;
                    userAuth.TokenSecret = accessTokenResponse.TokenSecret;

                    userAuthService.Save(userAuth);
                }
            }

            this.StoreSuccess("Your twitter account has been linked to your collectbrucelee.com account");

            return RedirectToAction("Index", "Account", new { area = "" });
        }

        public ActionResult Disconnect() {
            UserAuthenticationProfileService service = new UserAuthenticationProfileService();
            service.DisconnectService(AuthenticationServices.TWITTER, CurrentUserID);

            this.StoreInfo("Your twitter account has been disconnected from your collectbrucelee.com account");

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}