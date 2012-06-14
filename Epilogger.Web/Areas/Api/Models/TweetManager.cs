using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;


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





        
    }
 
}