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


        public static DateTime ToUserTimeZone(this DateTime dt, int EventTimeZoneOffSet)
        {
            TimeSpan offset;
            if (GetUserGuid() == Guid.Parse("{00000000-0000-0000-0000-000000000000}"))
            {
                //Use Event Offset
                offset = new TimeSpan(EventTimeZoneOffSet, 0, 0);
            }
            else
            {
                //user User offset
                offset = new TimeSpan(GetUserTimeZoneOffset(), 0, 0);
            }

            return dt.Add(offset);
        }

    }
}