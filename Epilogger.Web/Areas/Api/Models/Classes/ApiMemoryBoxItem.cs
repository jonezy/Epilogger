using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiMemoryBoxItem
    {
        public int ID { get; set; }
        public int MemboxId { get; set; }
        public Guid UserId { get; set; }
        public string ItemType { get; set; }
        public int? ItemId { get; set; }
        public DateTime? AddedDateTime { get; set; }
    }
}