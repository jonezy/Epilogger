using System.Collections.Generic;
using Epilogger.Web.Areas.Api.Models.Classes;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface ITweetManager
    {
        List<ApiTweet> GetTweetsByEventPages(int eventId, int page, int count);


    }
}



