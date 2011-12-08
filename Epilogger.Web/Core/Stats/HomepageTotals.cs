
using RichmondDay.Helpers;
using System;
using System.Web;
namespace Epilogger.Web.Core.Stats {
    public class HomepageTotals {

        private string CacheKey = "HomepageTotals";
        protected virtual double CacheExpiry {
            get { return 60; }
        } // time in seconds the cache should live for

        public HomepageTotal HomepageTotal {
            get {
                CacheHelper CacheHelper = new CacheHelper(HttpContext.Current.Cache);
                HomepageTotal data = CacheHelper.Get(CacheKey) as HomepageTotal;

                if (data == null) {
                    data = new HomepageTotal();
                    EventService eventService = new EventService();
                    TweetService tweetService = new TweetService();
                    CheckInService checkinService = new CheckInService();
                    ImageService imageService = new ImageService();

                    data.TotalEvents = eventService.Count();
                    data.TotalTweets = tweetService.Count();
                    data.TotalCheckins = checkinService.Count();
                    data.TotalMedia = imageService.Count();

                    CacheHelper.Add(CacheKey, data, DateTime.Now.AddMinutes(CacheExpiry));
                }

                return data;
            }
        }
    }

    public class HomepageTotal {
        public int TotalEvents { get; set; }
        public int TotalMedia { get; set; }
        public int TotalTweets { get; set; }
        public int TotalCheckins { get; set; }

    }

}