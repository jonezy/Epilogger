using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epilogger.Common;
using Epilogger.Data;
using Epilogger.Web.Models;

namespace Epilogger.Web.Controllers
{
    public partial class ImageUploadController : BaseController
    {
        EventService _es = new EventService();

        public virtual ActionResult ChooseUploadSource(string id, int? status)
        {
            return View((object)status);
        }

        [HttpGet]
        public virtual ActionResult UploadFromComputer(string id)
        {
            return View((object)id);
        }

        [HttpPost]
        public virtual ActionResult UploadFromComputer(string id, IEnumerable<HttpPostedFileBase> files)
        {
            var requestedEvent = _es.FindBySlug(id);

            //ToDo: Move queues names into Epilogger.Common
            using (var messageProducer = new MQ.MSGProducer("Epilogger", "FileImage"))
            {
                foreach (var fileKey in Request.Files.AllKeys)
                {
                    var file = Request.Files[fileKey];

                    var fileMemoryStream = new MemoryStream();
                    file.InputStream.CopyTo(fileMemoryStream);

                    var imageMessage = new MQ.Messages.FileImageMSG(requestedEvent.ID, null, fileMemoryStream.ToArray(), file.FileName);
                    messageProducer.SendMessage(imageMessage);
                }
            }

            return new EmptyResult();
        }

        public virtual ActionResult GetFacebookAlbums(string id)
        {
            return View(new GetFacebookAlbumsViewModel(id));
        }

        [HttpGet]
        public virtual ActionResult UploadFacebookAlbumPhotos(string id, string albumId)
        {
            return View(new UploadFacebookAlbumPhotosViewModel(albumId));
        }

        [HttpPost]
        public virtual ActionResult UploadFacebookAlbumPhotos(string id, string albumId, IEnumerable<string> photosUrls)
        {
            var requestedEvent = _es.FindBySlug(id);

            //ToDo: Move queues names into Epilogger.Common
            using (var messageProducer = new MQ.MSGProducer("Epilogger", "FacebookImage"))
            {
                foreach (var photoUrl in photosUrls)
                {
                    var facebookImageMsg = new MQ.Messages.FacebookImageMSG(requestedEvent.ID, albumId, new Uri(photoUrl), CurrentUserID);
                    messageProducer.SendMessage(facebookImageMsg);
                }
            }

            return new EmptyResult();
        }

        public virtual ActionResult AuthenticateFlickr(string id)
        {
            return View();
        }

        public virtual ActionResult SuccessfullyAuthenticatedFlickr(string id)
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult UploadFlickrImages(string id)
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult UploadFlickrImages(string id, IEnumerable<string> photosUrls)
        {
            var requestedEvent = _es.FindBySlug(id);

            //ToDo: Move queues names into Epilogger.Common
            using (var messageProducer = new MQ.MSGProducer("Epilogger", "FlickrImage"))
            {
                foreach (var photoUrl in photosUrls)
                {
                    var flickrImageMsg = new MQ.Messages.FlickrImageMSG(requestedEvent.ID, new Uri(photoUrl));
                    messageProducer.SendMessage(flickrImageMsg);
                }
            }

            return new EmptyResult();
        }
    }
}
