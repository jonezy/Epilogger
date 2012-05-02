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
    public partial class ImageController : Controller
    {
        EventService _es = new EventService();

        [ChildActionOnly]
        public virtual ActionResult Upload()
        {
            return View();
        }

        public virtual ActionResult UploadFromFile(string id, IEnumerable<HttpPostedFileBase> files)
        {
            var requestedEvent = _es.FindBySlug(id);

            using (var messageProducer = new MQ.MSGProducer("Epilogger", "Image"))
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
    }
}
