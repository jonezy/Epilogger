namespace Epilogger.Web.Models
{
    public class UploadFacebookAlbumPhotosViewModel
    {
        public string AlbumId { get; set; }

        public UploadFacebookAlbumPhotosViewModel(string albumId)
        {
            AlbumId = albumId;
        }
    }
}