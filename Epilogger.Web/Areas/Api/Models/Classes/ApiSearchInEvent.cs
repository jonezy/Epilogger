using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiSearchInEvent
    {
        public ApiTweet Tweet { get; set; }
        public ApiImage Image { get; set; }
    }
}