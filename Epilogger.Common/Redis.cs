using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ServiceStack.Redis;

namespace Epilogger.Common
{
    public class Redis
    {

        public List<string> GetTrendingEvents()
        {
            try
            {
                using (var redisClient = new RedisClient("ec2-50-17-76-244.compute-1.amazonaws.com"))
                {
                    return redisClient.GetAllItemsFromSortedSetDesc("trending:events");
                }
            }
            catch (Exception)
            {
                return new List<string>();
            }
            
        }


        public class EpiloggerRedisObjects
        {


            [Serializable]
            [JsonObject(MemberSerialization.OptOut)]
            public class TwitterSearchMSG
            {
                public int EventID { get; set; }
                public string SearchTerms { get; set; }
                public bool SearchFromLatestTweet { get; set; }
                public DateTime? SearchSince { get; set; }
                public DateTime? SearchUntil { get; set; }
            }


        }

    }
}
