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
        [ChildActionOnly]
        public virtual ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult UploadFromFile(string eventId, IEnumerable<HttpPostedFileBase> files)
        {
            using (var messageProducer = new MQ.MSGProducer("Epilogger", "UploadedImageSender"))
            {
                var imageMessage = new MQ.Messages.ImageMSG() {  };
                messageProducer.SendMessage(imageMessage);
            }

            return View();
        }
    }
}
