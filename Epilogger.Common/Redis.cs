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
            using (var redisClient = new RedisClient("ec2-184-73-140-83.compute-1.amazonaws.com"))
            {
                return redisClient.GetAllItemsFromSortedSetDesc("trending:events");
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
