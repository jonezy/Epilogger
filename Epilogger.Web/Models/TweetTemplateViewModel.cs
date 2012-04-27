using Epilogger.Data;

namespace Epilogger.Web.Models
{
    public class TweetTemplateViewModel
    {
        public Tweet Tweet { get; set; }
        public bool CanDelete { get; set; }
        public int EventId { get; set; }
    }
}