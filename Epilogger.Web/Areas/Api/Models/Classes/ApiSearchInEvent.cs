using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiSearchInEvent
    {
        public Epilogger.Data.Tweet Tweet { get; set; }
        public Epilogger.Data.Image Image { get; set; }
    }
}