using System;
using System.Linq;
using System.Web.Mvc;

using Epilogger.Data;
using Epilogger.Web.Controllers;

using Twitterizer;

namespace Epilogger.Web.Areas.Authentication.Controllers {
    public class TwitterController : BaseController {
        [HttpPost]
        public ActionResult BuildAuthenticationRequest() {
            string requestToken = OAuthUtility.GetRequestToken(TwitterHelper.TwitterConsumerKey, TwitterHelper.TwitterConsumerSecret, TwitterHelper.TwitterCallbackURL).Token;
            Uri authorizationUrl = OAuthUtility.BuildAuthorizationUri(requestToken);

            return Redirect(authorizationUrl.ToString());
        }

        public ActionResult ProcessResponse() {
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

            return RedirectToAction("Index", "Account", new { area = "" });
        }
    }
}
