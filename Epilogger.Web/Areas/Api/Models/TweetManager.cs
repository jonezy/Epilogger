using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;
using Epilogger.Web.Core.Stats;


namespace Epilogger.Web.Areas.Api.Models
{
    public class TweetManager : ITweetManager
    {
        TweetService _ts = new TweetService();

        public TweetManager()
        {
            if (_ts == null) _ts = new TweetService();    
        }

        //****************
        public List<ApiTweet> GetTweetsByEventPages(int eventId, int page, int count)
        {
            return Mapper.Map<List<Tweet>, List<ApiTweet>>(_ts.GetPagedTweets(eventId, page, count, (DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue).ToList());
        }

        public List<ApiTweeter> GetTop10Tweeters(int eventId)
        {
            return Mapper.Map<List<Tweeter>, List<ApiTweeter>>(_ts.GetTop10TweetersByEventID(eventId, (DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue).ToList());
        }



        public List<ApiTweet> GetTweetsByImageID(int imageId, int eventId, int page, int count)
        {
            return Mapper.Map<List<Tweet>, List<ApiTweet>>(_ts.FindByImageIDPaged(imageId, eventId, page, count).ToList());
        }

        public int GetTweetCountByImageID(int imageId, int eventId)
        {
            return _ts.FindCountByImageID(imageId, eventId);
        }


        


        
    }
 
}