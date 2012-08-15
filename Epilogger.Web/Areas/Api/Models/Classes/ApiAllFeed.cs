using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiAllFeed
    {
        public ApiTweet Tweet { get; set; }
        public List<ApiImage> Image { get; set; }
    }
}