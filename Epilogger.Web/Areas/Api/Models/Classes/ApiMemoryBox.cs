using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiMemoryBox
    {
        public int ID { get; set; }
        public Guid UserId { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}