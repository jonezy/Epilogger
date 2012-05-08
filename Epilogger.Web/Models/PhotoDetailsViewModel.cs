using System.Collections.Generic;
using Epilogger.Data;

namespace Epilogger.Web.Models
{
    public class PhotoDetailsViewModel
    {
        public int EventId { get; set; }
        public string EventSlug { get; set; }
        public int PhotoID { get; set; }
        public Data.Image Image { get; set; }
        public IEnumerable<Tweet> Tweets { get; set; }

    }
}