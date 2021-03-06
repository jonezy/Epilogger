﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using RichmondDay.Helpers;
using System.Drawing;

namespace Epilogger.Web {
    public static class Helpers {
        public static Guid GetUserGuid() {
            Guid uid;
            Guid.TryParse(CookieHelpers.GetCookieValue("lc", "uid").ToString(), out uid);

            return uid;
        }

        static IEnumerable<T> Randomize<T>(this IEnumerable<T> source) {
            var array = source.ToArray();
            // randomize indexes (several approaches are possible)
            return array;
        }

        //DEPRECATED!

        //public static int GetUserTimeZoneOffset() {
        //    //This takes into account the Daylight savings time and all that BS. But seems to fuck up when on 
        //    //TimeZone localZone = TimeZone.CurrentTimeZone;
        //    //TimeSpan currentOffset = localZone.GetUtcOffset(DateTime.Now);
        //    //return currentOffset.Hours;

        //    int TimeZoneOffSet;
        //    TimeZoneOffSet = int.Parse(CookieHelpers.GetCookieValue("lc", "tz").ToString());
        //    return TimeZoneOffSet;

        //}



        //public static DateTime ToUserTimeZone(this DateTime dt) {
        //    TimeSpan offset;
        //    //user User offset
        //    offset = new TimeSpan(GetUserTimeZoneOffset(), 0, 0);

        //    return dt.Add(offset);
        //}

        //public static DateTime ToUserTimeZone(this DateTime dt, int EventTimeZoneOffSet) {
        //    if (dt == DateTime.MinValue)
        //        return dt;

        //    TimeSpan offset;
        //    if (GetUserGuid() == Guid.Parse("{00000000-0000-0000-0000-000000000000}")) {
        //        //Use Event Offset
        //        offset = new TimeSpan(EventTimeZoneOffSet, 0, 0);
        //    } else {
        //        //user User offset
        //        offset = new TimeSpan(GetUserTimeZoneOffset(), 0, 0);
        //    }

        //    return dt.Add(offset);
        //}


        //public static DateTime FromUserTimeZoneToUtc(this DateTime dt) {
        //    TimeSpan offset;
        //    //user User offset
        //    offset = new TimeSpan(-GetUserTimeZoneOffset(), 0, 0);

        //    return dt.Add(offset);
        //}
        //public static DateTime FromUserTimeZoneToUtc(this DateTime dt, int EventTimeZoneOffSet) {
        //    TimeSpan offset;
        //    if (GetUserGuid() == Guid.Parse("{00000000-0000-0000-0000-000000000000}")) {
        //        //Use Event Offset
        //        offset = new TimeSpan(-EventTimeZoneOffSet, 0, 0);
        //    } else {
        //        //user User offset
        //        offset = new TimeSpan(-GetUserTimeZoneOffset(), 0, 0);
        //    }

        //    return dt.Add(offset);
        //}




        public static string base64Encode(string data) {
            try {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            } catch (Exception e) {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        public static string base64Decode(string data) {
            try {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            } catch (Exception e) {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        public static int RandomInt(Random rng, int min, int max) {
            //return min + (rng.Next() * (max - min));
            return rng.Next(min, max);
        }


        public static void ResizeImageStream(Stream imgStream, int newWidth, int maxHeight, bool onlyResizeIfWider, out Stream newImageStream)
        {
            var fullsizeImage = System.Drawing.Image.FromStream(imgStream);

            // Prevent using images internal thumbnail
            fullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            fullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (onlyResizeIfWider)
            {
                if (fullsizeImage.Width <= newWidth)
                {
                    newWidth = fullsizeImage.Width;
                }
            }

            var newHeight = fullsizeImage.Height * newWidth / fullsizeImage.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead
                newWidth = fullsizeImage.Width * maxHeight / fullsizeImage.Height;
                newHeight = maxHeight;
            }

            var newImage = fullsizeImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            fullsizeImage.Dispose();

            // Save resized picture
            newImageStream = newImage.ToStream(ImageFormat.Png);
        }


        
        public static Stream ToStream(this System.Drawing.Image image, ImageFormat formaw)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }
        
        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://", System.StringComparison.Ordinal) > -1)
                return serverUrl;

            var newUrl = serverUrl;
            var originalUri = HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                "://" + originalUri.Authority + newUrl;
            return newUrl;
        }

        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


    }
}