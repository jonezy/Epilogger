using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RichmondDay.Helpers;

namespace Epilogger.Web {
    public static class Helpers {
        public static Guid GetUserGuid() {
            Guid uid;
            Guid.TryParse(CookieHelpers.GetCookieValue("lc", "uid").ToString(), out uid);

            return uid;
        }

        public static int GetUserTimeZoneOffset()
        {
            int TimeZoneOffSet;
            TimeZoneOffSet =  int.Parse(CookieHelpers.GetCookieValue("lc", "tz").ToString());
            return TimeZoneOffSet;
        }


        public static DateTime ToUserTimeZone(this DateTime dt)
        {
            return dt.Add(new TimeSpan(GetUserTimeZoneOffset(), 0, 0));
        }

    }
}