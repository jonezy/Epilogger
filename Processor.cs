using System;
using System.Linq;
using Epilogger.Common;
using Epilogger.Data;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;


namespace Epilogger.Worker.ImageProcessor
{
    public class Processor
    {
        private volatile bool _stopThread;

        public void RequestStop()
        {
            _stopThread = true;
        }

        public void StartProcessing()
        {
            _stopThread = false;

            //Initialize the Queue
            var im = new MQ.MSGConsumer<MQ.Messages.ImageMSG>("Epilogger", "Image");

            while (!_stopThread)
            {
                //Define the Message

                //Get the Message
                var msg = im.GetMSG();

                try
                {
                    //Ack the message has been processed
                    im.AckMSG();

                    //Do the Image Processing
                    var ip = new ExecuteImageProcesing(msg);
                    ip.Execute();
                }
                catch (Exception ex)
                {
                    //Ack the message has been processed
                    im.AckMSG();

                    //Put the Message into an Error queue
                    var mpe = new MQ.MSGProducer("Epilogger", "Errors");
                    var errorMsg = new MQ.Messages.ErrorsMSG
                                       {
                                           Error = ex.ToString(),
                                           Source = "Epilogger.Worker.ImageProcessor",
                                           ErrorMSG = msg
                                       };

                    mpe.SendMessage(errorMsg);
                    mpe.Dispose();
                    
                }
            }

            im.Dispose();
        }


        private class ExecuteImageProcesing
        {
            private readonly MQ.Messages.ImageMSG _msg;

            public ExecuteImageProcesing(MQ.Messages.ImageMSG msg)
            {
                _msg = msg;
            }

            public void Execute()
            {
                if (_msg == null) { throw new Exception("An Image Message class must be passed into the constructor"); }

                try
                {
                    var ts = new TweetService();
                    var imgSrv = new ImageService();
                    var imd = new ImageMetaDateService();

                    var fullImagesUrl = _msg.ImageURL;
                    var tw = ts.FindById(_msg.TweetID);

                    //Events keep their own images.

                    //Check to see if this image has already been stored, if it has reuse the stored image and add another meta data record to it. 
                    //If it hasn't store it.
                    //IEnumerable<Image> ExistingImages = IS.GetImageByURL(FullImagesURL);
                    var existingImages = imgSrv.GetImageByURLAndEventID(fullImagesUrl, int.Parse(_msg.EventID.ToString())).ToList();

                    if (existingImages.Any())
                    {
                        foreach (var p in existingImages)
                        {
                            //Insert metadata for image
                            var limd = new ImageMetaDatum
                                           {
                                               ImageID = p.ID,
                                               EventID = int.Parse(_msg.EventID.ToString()),
                                               UserID = System.Guid.Empty,
                                               ImageSource = _msg.ImageSource,
                                               TwitterID = tw.TwitterID,
                                               TwitterName = _msg.TwitterName,
                                               DateTime = tw.CreatedDate
                                           };
                            imd.Save(limd);
                        }

                    }
                    else
                    {
                        //Insert image
                        //Get a MemStream from the Image URL, upload to Azure.
                        var fullImageStream = new MemoryStream(new System.Net.WebClient().DownloadData(fullImagesUrl));
                        var fullAzureURL = StoreImage("twitter-" + _msg.EventID + "-" + tw.ID + ".jpg", "twitterphotos-full", fullImageStream);

                        //Store the Info in the DB
                        var lImage = new Image
                                         {
                                             EventID = int.Parse(_msg.EventID.ToString()),
                                             AzureContainerPrefix = "twitterphotos",
                                             Fullsize = fullAzureURL.AbsoluteUri,
                                             Thumb = fullAzureURL.AbsoluteUri,
                                             OriginalImageLink = fullImagesUrl.AbsoluteUri,
                                             DateTime = tw.CreatedDate ?? DateTime.Parse("2000-01-01"),
                                             DeleteVoteCount = 0,
                                             Deleted = false
                                         };
                        imgSrv.Save(lImage);

                        //Insert metadata for image
                        var limd = new ImageMetaDatum
                                       {
                                           ImageID = lImage.ID,
                                           EventID = int.Parse(_msg.EventID.ToString()),
                                           UserID = System.Guid.Empty,
                                           ImageSource = _msg.ImageSource,
                                           TwitterID = tw.TwitterID,
                                           TwitterName = _msg.TwitterName,
                                           DateTime = tw.CreatedDate
                                       };
                        imd.Save(limd);
                    }

                    AddImageCountToTrendingQueue(_msg);

                }
                catch (Exception)
                {
                    throw;
                }

            }

            private void AddImageCountToTrendingQueue(MQ.Messages.ImageMSG msg)
            {

                //Throw the new numbers into the Trending Queue for additional processing.
                var mpt = new MQ.MSGProducer("Epilogger", "Trending");
                var TMsg = new MQ.Messages.TrendingUpdateMSG
                                {
                                    EventID = _msg.EventID,
                                    MediaType = MQ.Messages.MediaType.Photo,
                                    Incrementvalue = 1,
                                    UpdateDateTime = DateTime.UtcNow
                                };
                mpt.SendMessage(TMsg);
                mpt.Dispose();
            }

            private static Uri StoreImage(string filename, string containerName, Stream memStream)
            {
                try
                {
                    // Variables for the cloud storage objects.
                    CloudStorageAccount cloudStorageAccount1 = default(CloudStorageAccount);
                    CloudBlobClient blobClient = default(CloudBlobClient);
                    CloudBlobContainer blobContainer = default(CloudBlobContainer);
                    BlobContainerPermissions containerPermissions = default(BlobContainerPermissions);
                    CloudBlob blob = default(CloudBlob);

                    cloudStorageAccount1 = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=epiloggerphotos;AccountKey=xbSt0uQAqExzWpc60pcmP6k49Uu7raxPG1BA5+aBhrAAdNxaoiFAZ67jQmG/iiJIeFeemnp74NRuuFAXaaxGJQ==");

                    // Create the blob client, which provides
                    // authenticated access to the Blob service.
                    blobClient = cloudStorageAccount1.CreateCloudBlobClient();

                    // Get the container reference.
                    blobContainer = blobClient.GetContainerReference(containerName);
                    // Create the container if it does not exist.
                    blobContainer.CreateIfNotExist();

                    // Set permissions on the container.
                    containerPermissions = new BlobContainerPermissions();
                    // This sample sets the container to have public blobs. Your application
                    // needs may be different. See the documentation for BlobContainerPermissions
                    // for more information about blob container permissions.
                    containerPermissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                    blobContainer.SetPermissions(containerPermissions);

                    // Get a reference to the blob.
                    blob = blobContainer.GetBlobReference(filename);
                    blob.Properties.ContentType = "image/jpeg";

                    // Upload a file from the local system to the blob.
                    //blob.UploadFile("c:\TestAzure.jpg")
                    blob.UploadFromStream(memStream);

                    return blob.Uri;

                }
                catch (StorageClientException e)
                {
                    return new Uri("");
                }
                catch (Exception e)
                {
                    return new Uri("");
                }

            }


            //public void FingerPrint()
            //{
            //    //Load the image
                





            //    // Load the image. Escape out if it's not a valid jpeg.
            //    if (!$image = @imagecreatefromjpeg($this->filePath."/".$filename)) {
            //        return -1;
            //    }

            //    // Create thumbnail sized copy for fingerprinting
            //    $width = imagesx($image);
            //    $height = imagesy($image);
            //    $ratio = $this->thumbWidth / $width;
            //    $newwidth = $this->thumbWidth;
            //    $newheight = round($height * $ratio); 
            //    $smallimage = imagecreatetruecolor($newwidth, $newheight);
            //    imagecopyresampled($smallimage, $image, 0, 0, 0, 0, 
            //    $newwidth, $newheight, $width, $height);
            //    $palette = imagecreatetruecolor(1, 1);
            //    $gsimage = imagecreatetruecolor($newwidth, $newheight);

            //    // Convert each pixel to greyscale, round it off, and add it to the histogram count
            //    $numpixels = $newwidth * $newheight;
            //    $histogram = array();
            //    for ($i = 0; $i < $newwidth; $i++) {
            //        for ($j = 0; $j < $newheight; $j++) {
            //            $pos = imagecolorat($smallimage, $i, $j);
            //            $cols = imagecolorsforindex($smallimage, $pos);
            //            $r = $cols['red'];
            //            $g = $cols['green'];
            //            $b = $cols['blue'];
            //            // Convert the colour to greyscale using 30% Red, 59% Blue and 11% Green
            //            $greyscale = round(($r * 0.3) + ($g * 0.59) + ($b * 0.11));                 
            //            $greyscale++;
            //            $value = (round($greyscale / 16) * 16) -1;
            //            $histogram[$value]++;
            //        }
            //    }

            //    // Normalize the histogram by dividing the total of each colour by the total number of pixels
            //    $normhist = array();
            //    foreach ($histogram as $value => $count) {
            //        $normhist[$value] = $count / $numpixels;
            //    }

            //    // Find maximum value (most frequent colour)
            //    $max = 0;
            //    for ($i=0; $i<255; $i++) {
            //        if ($normhist[$i] > $max) {
            //            $max = $normhist[$i];
            //        }
            //    }   

            //    // Create a string from the histogram (with all possible values)
            //    $histstring = "";
            //    for ($i = -1; $i <= 255; $i = $i + 16) {
            //        $h = ($normhist[$i] / $max) * $this->sensitivity;
            //        if ($i < 0) {
            //            $index = 0;
            //        } else {
            //            $index = $i;
            //        }
            //        $height = round($h);
            //        $histstring .= $height;
            //    }

            //    // Destroy all the images that we've created
            //    imagedestroy($image);
            //    imagedestroy($smallimage);
            //    imagedestroy($palette);
            //    imagedestroy($gsimage);

            //    // Generate an md5sum of the histogram values and return it
            //    $checksum = md5($histstring);
            //    return $checksum;

            //}






        }

    }
}
