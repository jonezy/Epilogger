using System;
using System.Configuration;
using System.Linq;

using Epilogger.Data;
using Epilogger.Web;
using Facebook;
using RichmondDay.Helpers;

public static class FacebookHelper {
    public static bool IsTokenValid(string token) {
        var client = new FacebookClient(token);
        
        try {
            dynamic me = client.Get("me");
            return true;
        } catch {
            return false;
        }
    }

    public static string UdpateAccessToken() {
        
        var fb = new FacebookClient();
        dynamic tokenResult = fb.Get("oauth/access_token", new
        { 
            client_id     = ConfigurationManager.AppSettings["FacebookAppId"] as string ?? "",
            client_secret = ConfigurationManager.AppSettings["FacebookAppSecret"] as string ?? "", 
            grant_type    = "client_credentials" 
        });


        //var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
        //dynamic tokenResult = oAuthClient.GetApplicationAccessToken();

        return tokenResult.token;
    }

    public static string GetProfilePicture(string token) {
        if (!IsTokenValid(token)) {
            // token is expired, get a new one and save it to the db.
            token = UdpateAccessToken();
            UpdateUsersAuthenticationToken(token);
        }
        
        var fbClient = new FacebookClient(token);
        dynamic me = fbClient.Get("me");

        return string.Format("https://graph.facebook.com/{0}/picture", me.id);
    }


    public static string GetProfilePictureWithSize(string token, string size) {
        if (!IsTokenValid(token)) {
            // token is expired, get a new one and save it to the db.
            token = UdpateAccessToken();
            UpdateUsersAuthenticationToken(token);
        }
        
        var fbClient = new FacebookClient(token);
        dynamic me = fbClient.Get("me");

        return string.Format("https://graph.facebook.com/{0}/picture?type={1}", me.id, size);
    }

    

    private static void UpdateUsersAuthenticationToken(string newToken) {
        var service= new UserService();
        var authService = new UserAuthenticationProfileService();

        // get the current userid
        Guid uid;
        Guid.TryParse(CookieHelpers.GetCookieValue("lc", "uid").ToString(), out uid);
         
        // get the users authentication record and update the token
        var user = service.GetUserByID(uid);
        var userAuth = user.UserAuthenticationProfiles.FirstOrDefault(ua => ua.Service == "FACEBOOK");
        userAuth.Token = newToken;

        authService.Save(userAuth);
    }
}
