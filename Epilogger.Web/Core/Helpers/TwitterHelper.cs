using System.Configuration;

using Twitterizer;

public static class TwitterHelper {
    public static string TwitterConsumerKey {
        get {
            return ConfigurationManager.AppSettings["TwitterConsumerKey"] as string ?? "";
        }
    }

    public static string TwitterConsumerSecret {
        get {
            return ConfigurationManager.AppSettings["TwitterConsumerSecret"] as string ?? "";
        }
    }

    public static string TwitterCallbackURL {
        get {
            return ConfigurationManager.AppSettings["TwitterCallbackURL"] as string ?? "";
        }
    }

    public static OAuthTokens GetTokens(string token, string tokenSecret) {
        OAuthTokens tokens = new OAuthTokens() { 
            ConsumerKey = TwitterConsumerKey, 
            ConsumerSecret = TwitterConsumerSecret,
            AccessToken = token,     
            AccessTokenSecret = tokenSecret
        };

        return tokens;
    }
}
