namespace Epilogger.Web.Models
{
    public class GetFacebookAlbumsViewModel
    {
        public string EventId { get; set; }

        public GetFacebookAlbumsViewModel(string eventId)
        {
            EventId = eventId;
        }
    }
}