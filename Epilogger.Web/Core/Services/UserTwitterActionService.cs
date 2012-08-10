using System;
using System.Linq;
using Epilogger.Data;

namespace Epilogger.Web
{
    public class UserTwitterActionService : ServiceBase<UserTwitterAction>
    {
       
        protected override string CacheKey
        {
            get { return "Epilogger.Web.UserTwitterActions"; }
        }


        public bool HasUserRepliedToTweet(Guid userId, long tweetId)
        {
            return db.UserTwitterActions.Any(t => t.TweetId == tweetId && t.UserId == userId && t.TwitterAction=="Reply");
        }
        public bool HasUserRetweetedTweet(Guid userId, long tweetId)
        {
            return db.UserTwitterActions.Any(t => t.TweetId == tweetId && t.UserId == userId && t.TwitterAction == "Retweet");
        }
        public bool HasUserFavoritedTweet(Guid userId, long tweetId)
        {
            return db.UserTwitterActions.Any(t => t.TweetId == tweetId && t.UserId == userId && t.TwitterAction == "Favorite");
        }


        public int GetUniqueUsersForDateRange(DateTime f, DateTime t)
        {
            return db.UserTwitterActions.Where(e => e.DateTime <= f && e.DateTime >= t).Select(e => e.UserId).Distinct().Count();
        }


        public object Save(UserTwitterAction entity)
        {
            return GetRepository<UserTwitterAction>().Add(entity);
        }
      
    }
}