using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Epilogger.Web.Controllers;
using Epilogger.Data;
using RichmondDay.Helpers;
using Twitterizer;

namespace Epilogger.Web.Areas.Authentication.Controllers {
    public partial class TwitterController : BaseController, IAuthenticationController
    {
        public virtual ActionResult ConnectRequest()
        {
            string requestToken = OAuthUtility.GetRequestToken(TwitterHelper.TwitterConsumerKey, TwitterHelper.TwitterConsumerSecret, TwitterHelper.TwitterCallbackURL).Token;
            Uri authorizationUrl = OAuthUtility.BuildAuthorizationUri(requestToken,false);
            
            return Redirect(authorizationUrl.ToString());
        }

        public virtual ActionResult ConnectAccount()
        {
            if (Request.QueryString["oauth_token"] != null) {
                var accessTokenResponse = OAuthUtility.GetAccessTokenDuringCallback(TwitterHelper.TwitterConsumerKey, TwitterHelper.TwitterConsumerSecret);
                // check to see if we already have the screen name in the userauth table.
                var userAuthService = new UserAuthenticationProfileService();
                var userAuth = userAuthService.UserAuthorizationByServiceScreenName(accessTokenResponse.ScreenName);

                if (userAuth != null) {
                    userAuth.Token = accessTokenResponse.Token;
                    userAuth.TokenSecret = accessTokenResponse.TokenSecret;
                    userAuthService.Save(userAuth);

                    var user = userAuth.Users.FirstOrDefault();
                    if (user != null)
                    {
                        CookieHelpers.WriteCookie("lc", "uid", user.ID.ToString());
                        CookieHelpers.WriteCookie("lc", "tz", user.TimeZoneOffSet.ToString(CultureInfo.InvariantCulture));

                        //This user already has an account, authed them, redirect back to where they were.
                        this.StoreSuccess("You've been logged into your Epilogger account through twitter.");
                        

                    }
                } else {
                    //There is no auth record, this must be a login from Twitter or a new twitter connection to an existing user
                    var us = new UserService();
                    if (CurrentUserID == Guid.Empty)
                    {
                        //This is a new login, create the user
                        var theNewUser = new User()
                        {
                            CreatedDate = DateTime.UtcNow,
                            IsActive = true,
                            RoleID = 2,
                            Username = accessTokenResponse.ScreenName + "twitter",
                            Password = " ",
                            EmailAddress = " "
                        };
                        us.Save(theNewUser);
                        userAuth = new UserAuthenticationProfile
                        {
                            UserID = theNewUser.ID,
                            Service = AuthenticationServices.TWITTER.ToString(),
                            ServiceUsername = accessTokenResponse.ScreenName,
                            Token = accessTokenResponse.Token,
                            TokenSecret = accessTokenResponse.TokenSecret
                        };
                        userAuthService.Save(userAuth);

                        CookieHelpers.WriteCookie("lc", "uid", theNewUser.ID.ToString());
                        CookieHelpers.WriteCookie("lc", "tz", theNewUser.TimeZoneOffSet.ToString(CultureInfo.InvariantCulture));

                        //This is a new User so goto the profile page
                        this.StoreSuccess("You have logged into Epilogger with your twitter account.");
                        return RedirectToAction("Index", "Account", new { area = "" });
                    }
                    else
                    {
                        //This is a connection to Twitter
                        userAuth = new UserAuthenticationProfile
                        {
                            UserID = CurrentUserID,
                            Service = AuthenticationServices.TWITTER.ToString(),
                            ServiceUsername = accessTokenResponse.ScreenName,
                            Token = accessTokenResponse.Token,
                            TokenSecret = accessTokenResponse.TokenSecret
                        };
                        userAuthService.Save(userAuth);

                        //Had an epilogger account and just auth'ed their account, go back to where they were.
                        this.StoreSuccess("Your twitter account has been linked to your epilogger.com account");
                        

                    }
                    
                }
            }

            this.StoreSuccess("Your twitter account has been linked to your epilogger.com account");
            return RedirectToAction("Index", "Account", new { area = "" });
        }

        public virtual ActionResult Disconnect()
        {
            UserAuthenticationProfileService service = new UserAuthenticationProfileService();
            service.DisconnectService(AuthenticationServices.TWITTER, CurrentUserID);

            this.StoreInfo("Your twitter account has been disconnected from your epilogger.com account");

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}