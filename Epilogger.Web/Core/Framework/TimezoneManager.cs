using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timezone.Framework
{

    public class TimeZoneManager
    {

        private const string UserTimezoneContextKey = "USER_TIMEZONE";

        public static void UpdateTimeZone(HttpRequest request)
        {
            //Get the browser timezone from the cookie
            var cookie = request.Cookies["TimeZoneOffset"];
            if (cookie == null) return;
            var offset = new TimeSpan(0, Int32.Parse(cookie.Value), 0);
            UserTimeZone = TimeZoneInfo.CreateCustomTimeZone("Browser", offset, "Browser", "Browser");
        }

        public static TimeZoneInfo UserTimeZone
        {
            get
            {
                var contextValue = HttpContext.Current.Items[UserTimezoneContextKey];
                return (contextValue == null) ? TimeZoneInfo.Local : (TimeZoneInfo)contextValue;
            }
            set
            {
                HttpContext.Current.Items[UserTimezoneContextKey] = value;
            }
        }

        public static DateTime GetUserTime(DateTime value)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(value, UserTimeZone);
        }

        public static DateTime ToUtcTime(DateTime value)
        {
            return TimeZoneInfo.ConvertTimeToUtc(value, UserTimeZone);
        }

    }

}