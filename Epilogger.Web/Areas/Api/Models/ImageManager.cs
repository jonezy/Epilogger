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
            return _is.FindImageCountByEventID(eventID, (DateTime) SqlDateTime.MinValue, (DateTime) SqlDateTime.MaxValue);
        }

        public List<ApiImage> FindByEventIDOrderDescTakeX(int eventID, int numberToReturn)
        {
            return
                Mapper.Map<List<Image>, List<ApiImage>>(
                    _is.FindByEventIDOrderDescTakeX(eventID, numberToReturn, (DateTime) SqlDateTime.MinValue,
                                                    (DateTime) SqlDateTime.MaxValue).ToList());
        }

        public List<ApiImage> GetTopPhotosByEventID(int eventID, int recordsToReturn)
        {
            return Mapper.Map<List<Image>, List<ApiImage>>(_is.GetTopPhotosByEventID(eventID, recordsToReturn, (DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue).ToList());
        }

        public List<ApiImage> GetNewestPhotosByEventID(int eventID, int numberToGet)
        {
            return Mapper.Map<List<Image>, List<ApiImage>>(_is.GetNewestPhotosByEventID(eventID, numberToGet).ToList());
        }

        public List<ApiTopImageAndTweet> GetTopPhotosAndTweetByEventID(int eventID, int recordsToReturn)
        {
            return Mapper.Map<List<TopImageAndTweet>, List<ApiTopImageAndTweet>>(_is.GetTopPhotosAndTweetByEventID(eventID, recordsToReturn, (DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue).ToList());
        }

        public List<ApiImage> GetPagedPhotos(int eventID, int? page, int photosPerPage)
        {
            var model =
                Mapper.Map<List<Image>, List<ApiImage>>(
                    _is.GetPagedPhotos(eventID, page, photosPerPage, (DateTime) SqlDateTime.MinValue,
                                       (DateTime) SqlDateTime.MaxValue).ToList());
            return model;
        }
    }
 
}