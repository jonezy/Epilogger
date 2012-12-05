using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epilogger.Data;

namespace Epilogger.Web.Models
{
    public class CreateEventSuperObject
    {
        public Event Event { get; set; }
        public int CollectDataValue { get; set; }
    }
}