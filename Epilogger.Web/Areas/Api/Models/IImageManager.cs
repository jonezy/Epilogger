using System.Collections.Generic;
using Epilogger.Web.Areas.Api.Models.Classes;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface IImageManager
    {
        int FindImageCountByEventID(int eventID);
        List<ApiImage> FindByEventIDOrderDescTakeX(int eventID, int numberToReturn);
        List<ApiImage> GetTopPhotosByEventID(int eventID, int recordsToReturn);
        List<ApiImage> GetNewestPhotosByEventID(int eventID, int numberToGet);
        List<ApiTopImageAndTweet> GetTopPhotosAndTweetByEventID(int eventID, int recordsToReturn);
        List<ApiImage> GetPagedPhotos(int eventID, int? page, int photosPerPage);
    }
}



