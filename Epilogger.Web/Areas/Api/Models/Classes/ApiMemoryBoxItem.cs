using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Areas.Api.Models.Classes
{
    public class ApiMemoryBoxItem
    {
        private bool? _includePhotos;
        public int Id { get; set; }
        public int? MemboxId { get; set; }
        public Guid UserId { get; set; }
        public int EventId { get; set; }
        public string ItemType { get; set; }
        public string ItemId { get; set; }
        public DateTime? AddedDateTime { get; set; }
        public bool? IncludePhotos
        {
            get
            {
                return _includePhotos ?? false;
            }
            set { _includePhotos = value; }
        }
    }
}