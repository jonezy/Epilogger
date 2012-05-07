using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epilogger.Common;
using Epilogger.Data;

namespace Epilogger.Web.Controllers
{
    public partial class ImageUploadController : Controller
    {
        EventService _es = new EventService();

        public virtual ActionResult ChooseUploadSource()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult UploadFromComputer(string id)
        {
            return View();
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

        public virtual ActionResult UploadFromFacebook(string eventId)
        {
            return View();
        }

        public virtual ActionResult UploadFromFlickr(string eventId)
        {
            return View();
        }
    }
}
