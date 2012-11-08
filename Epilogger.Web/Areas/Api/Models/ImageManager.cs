using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;
using Epilogger.Web.Core.Stats;
using Epilogger.Web.Models;


namespace Epilogger.Web.Areas.Api.Models
{
    public class ImageManager : IImageManager
    {
        ImageService _is = new ImageService();

        public ImageManager()
        {
            if (_is == null) _is = new ImageService();    
        }

        //****************

        public int FindImageCountByEventID(int eventID)
        {
            //Includes Videos
            return _is.FindImageCountByEventID(eventID, (DateTime) SqlDateTime.MinValue, (DateTime) SqlDateTime.MaxValue);
        }

        public List<ApiImage> FindByEventIDOrderDescTakeX(int eventID, int numberToReturn)
        {
            //Fixed: does not return videos
            return
                Mapper.Map<List<Image>, List<ApiImage>>(
                    _is.FindByEventIdOrderDescTakeX(eventID, numberToReturn, (DateTime) SqlDateTime.MinValue,
                                                    (DateTime) SqlDateTime.MaxValue, false).ToList());
        }

        public List<ApiImage> GetTopPhotosByEventID(int eventID, int recordsToReturn)
        {
            //Fixed: does not return videos
            return Mapper.Map<List<Image>, List<ApiImage>>(_is.GetTopPhotosByEventId(eventID, recordsToReturn, (DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue, false).ToList());
        }

        public List<ApiImage> GetNewestPhotosByEventID(int eventID, int numberToGet)
        {
            //Fixed: does not return videos
            return Mapper.Map<List<Image>, List<ApiImage>>(_is.GetNewestPhotosByEventId(eventID, numberToGet, false).ToList());
        }

        public List<ApiTopImageAndTweet> GetTopPhotosAndTweetByEventID(int eventID, int recordsToReturn)
        {
            //Fixed: does not return videos
            return Mapper.Map<List<TopImageAndTweet>, List<ApiTopImageAndTweet>>(_is.GetTopPhotosAndTweetByEventID(eventID, recordsToReturn, (DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue, false).ToList());
        }

        public List<ApiImage> GetPagedPhotos(int eventId, int? page, int photosPerPage)
        {
            //var model =
            //    Mapper.Map<List<Image>, List<ApiImage>>(
            //        _is.GetPagedPhotos(eventID, page, photosPerPage, (DateTime) SqlDateTime.MinValue,
            //                           (DateTime) SqlDateTime.MaxValue, false).ToList());
            //return model;

            var theImages = _is.GetPagedApiImages(eventId, page, photosPerPage).ToList();
            return Mapper.Map<List<ApiImage>, List<ApiImage>>(theImages);
        }
    }
 
}