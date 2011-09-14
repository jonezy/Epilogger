using System;
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
        var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
        dynamic tokenResult = oAuthClient.GetApplicationAccessToken();

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

    private static void UpdateUsersAuthenticationToken(string newToken) {
        UserService service= new UserService();
        UserAuthenticationProfileService authService = new UserAuthenticationProfileService();

        // get the current userid
        Guid uid;
        Guid.TryParse(CookieHelpers.GetCookieValue("lc", "uid").ToString(), out uid);
         
        // get the users authentication record and update the token
        User user = service.GetUserByID(uid);
        UserAuthenticationProfile userAuth = user.UserAuthenticationProfiles.Where(ua => ua.Service == "FACEBOOK").FirstOrDefault();
        userAuth.Token = newToken;

        authService.Save(userAuth);
    }
}
