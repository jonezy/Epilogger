using System;
using System.Web;

public static class CookieHelpers {
    // TODO: merge this back into rd.helpers.
    public static void WriteCookie(string cookieIdentifier, string cookieKeyIdentifier, string cookieKeyValue) {
        WriteCookie(cookieIdentifier, cookieKeyIdentifier, cookieKeyValue, null);
    }

    public static void WriteCookie(string cookieIdentifier, string cookieKeyIdentifier, string cookieKeyValue, DateTime? expiry) {
        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookieIdentifier);
        if (cookie != null) {
            if (!string.IsNullOrEmpty(cookie.Values.Get(cookieKeyIdentifier)))
                cookie.Values.Remove(cookieKeyIdentifier);

            cookie.Values.Add(cookieKeyIdentifier, cookieKeyValue);
        } else {
            cookie = new HttpCookie(cookieIdentifier);
            HttpContext.Current.Response.Cookies.Remove(cookieIdentifier);
            HttpContext.Current.Response.Cookies.Add(cookie);
            cookie.Values.Add(cookieKeyIdentifier, cookieKeyValue.ToString());
        }

        if (expiry != null && expiry.HasValue)
            HttpContext.Current.Response.Cookies[cookieIdentifier].Expires = expiry.Value;
    }

    public static object GetCookieValue(string cookieIdentifier, string cookieKeyIdentifier) {
        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookieIdentifier);

        if (cookie == null)
            return 0;

        try {
            return cookie.Values[cookieKeyIdentifier];

        } catch {
            return null;
        }
    }

}
