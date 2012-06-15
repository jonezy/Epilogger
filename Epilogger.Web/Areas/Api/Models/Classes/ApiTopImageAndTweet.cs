using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiTopImageAndTweet
    {
        public ApiImage Image { get; set; }
        public ApiTweet Tweet { get; set; }
    }
}