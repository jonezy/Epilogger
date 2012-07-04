
using System.Globalization;
using RichmondDay.Helpers;
using System;
using System.Web;
namespace Epilogger.Web.Core.Stats {
    public class WidgetTotals {

        private string CacheKey = "WidgetTotals";
        protected virtual double CacheExpiry {
            get { return 60; }
        } // time in seconds the cache should live for

        public WidgetTotal GetWidgetTotals(int eventId, DateTime fromDateTime, DateTime toDateTime)
        {
            var cacheHelper = new CacheHelper(HttpContext.Current.Cache);
            var data = cacheHelper.Get(CacheKey + eventId.ToString(CultureInfo.InvariantCulture)) as WidgetTotal;

            if (data == null) {
                data = new WidgetTotal();

                var ts = new TweetService();
                var imgs = new ImageService();

                var tweetCount = ts.FindTweetCountByEventID(eventId, fromDateTime, toDateTime);
                var photoCount = imgs.FindImageCountByEventID(eventId, fromDateTime, toDateTime);
                var uniqueTweeterCount = ts.FindUniqueTweetCountByEventID(eventId, fromDateTime, toDateTime);

                data.TweetCount = ConvertNumberToKorM(tweetCount);
                data.PhotoCount = ConvertNumberToKorM(photoCount);
                data.UniqueTweeterCount = ConvertNumberToKorM(uniqueTweeterCount);

                cacheHelper.Add(CacheKey + eventId.ToString(CultureInfo.InvariantCulture), data, DateTime.Now.AddMinutes(CacheExpiry));
            }

            return data;
        }


        private string ConvertNumberToKorM(int numberToConvert)
        {
            string countString;
            if (numberToConvert > 1000000)
            {
                countString = Math.Floor(d: (double)(numberToConvert / 1000000)).ToString(CultureInfo.InvariantCulture) + "M";
            }
            else if (numberToConvert > 1000)
            {
                countString = Math.Floor(d: (double)(numberToConvert / 1000)).ToString(CultureInfo.InvariantCulture) + "K";
            }
            else
            {
                countString = numberToConvert.ToString(CultureInfo.InvariantCulture);
            }
            return countString;
        }

    }

    public class WidgetTotal
    {
        public String TweetCount { get; set; }
        public String PhotoCount { get; set; }
        public String UniqueTweeterCount { get; set; }
    }

}