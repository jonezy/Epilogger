using System.Collections.Generic;
using Epilogger.Web.Areas.Api.Models.Classes;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface ITweetManager
    {
        List<ApiTweet> GetTweetsByEventPages(int eventId, int page, int count);
        List<ApiTweeter> GetTop10Tweeters(int eventId);
        List<ApiTweet> GetTweetsByImageID(int imageId, int eventId, int page, int count);
        int GetTweetCountByImageID(int imageId, int eventId);
    }
}



