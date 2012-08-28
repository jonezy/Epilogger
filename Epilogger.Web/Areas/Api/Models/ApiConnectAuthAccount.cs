using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models
{
    public class ApiConnectAuthAccount
    {
        public Guid UserId { get; set; }
        public string AuthScreenName { get; set; }
        public string AuthToken { get; set; }
        public string AuthTokenSecret { get; set; }
    }
}