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
            //This takes into account the Daylight savings time and all that BS.
            TimeZone localZone = TimeZone.CurrentTimeZone;
            TimeSpan currentOffset = localZone.GetUtcOffset(DateTime.Now);
            return currentOffset.Hours;

            //int TimeZoneOffSet;
            //TimeZoneOffSet =  int.Parse(CookieHelpers.GetCookieValue("lc", "tz").ToString());
            //return TimeZoneOffSet;

        }



        public static DateTime ToUserTimeZone(this DateTime dt)
        {
            TimeSpan offset;
            //user User offset
            offset = new TimeSpan(GetUserTimeZoneOffset(), 0, 0);

            return dt.Add(offset);
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


        public static DateTime FromUserTimeZoneToUtc(this DateTime dt)
        {
            TimeSpan offset;
            //user User offset
            offset = new TimeSpan(-GetUserTimeZoneOffset(), 0, 0);

            return dt.Add(offset);
        }
        public static DateTime FromUserTimeZoneToUtc(this DateTime dt, int EventTimeZoneOffSet)
        {
            TimeSpan offset;
            if (GetUserGuid() == Guid.Parse("{00000000-0000-0000-0000-000000000000}"))
            {
                //Use Event Offset
                offset = new TimeSpan(-EventTimeZoneOffSet, 0, 0);
            }
            else
            {
                //user User offset
                offset = new TimeSpan(-GetUserTimeZoneOffset(), 0, 0);
            }

            return dt.Add(offset);
        }




        public static string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        public static string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        public static int RandomInt(Random rng, int min, int max)
        {
            //return min + (rng.Next() * (max - min));
            return rng.Next(min, max);
        }


    }
}