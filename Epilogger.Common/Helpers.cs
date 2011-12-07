using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using FlickrNet;

namespace Epilogger.Common
{
    public class Helpers
    {

        public Uri UnshortenURL(Uri ShortURL)
        {
            try 
	        {	        
		        //This will unshorten any URL
                WebRequest req = WebRequest.Create("http://api.unfwd4.me/?url=" + ShortURL.ToString());
                WebResponse resp = req.GetResponse();
                Stream receiveStream = resp.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                String ReturnValue = readStream.ReadToEnd();

                resp.Close();
                readStream.Close();
                
                if (ReturnValue != "ERROR:1" && ReturnValue != "ERROR:2")
                {
                    return new Uri(ReturnValue);
                }
                else
                {
                    return ShortURL;
                }
	        }
	        catch (Exception)
	        {
		        return ShortURL;
	        }

        }



        public Uri GetImageServiceImageURLs(Uri TheURL)
        {
            Uri FullImageURL = null;

            //Get the Image URLs
            switch (TheURL.Host)
            {    
                case "twitpic.com":
                    FullImageURL = new Uri("http://twitpic.com/show/large" + TheURL.LocalPath);
                    break;
                case "yfrog.com":
                    FullImageURL = new Uri("http://yfrog.com" + TheURL.LocalPath + ":iphone");
                    break;
                case "plixi.com":
                    FullImageURL = new Uri("http://api.plixi.com/api/tpapi.svc/imagefromurl?size=medium&url=" + TheURL.AbsoluteUri);
                    break;
                case "lockerz.com":
                    FullImageURL = new Uri("http://api.plixi.com/api/tpapi.svc/imagefromurl?size=medium&url=" + TheURL.AbsoluteUri);
                    break;
                case "instagr.am":
                    FullImageURL = new Uri(TheURL.AbsoluteUri + "media/?size=l");
                    break;
                case "flic.kr":
                case "flickr.com":
                case "www.flickr.com":
                    try
                    {
                        //Flickr is a little more complicated, need to manually unshorten the short ImageID, then we can get the URLS
                        string TheShortcode = string.Empty;
                        //TheShortcode = TheURL.Query.Substring(InStr(TheURL.Query, "="));
                        TheShortcode = TheURL.Query.Substring(TheURL.Query.IndexOf("=", 0));

                        Flickr FL = new Flickr("332533462d321fbb78ae8e04c691cea7");
                        PhotoInfo FPI = FL.PhotosGetInfo(Functions.DecodeBase58(TheShortcode).ToString());

                        if (FPI != null)
                        {
                            //Now build the URL from the parts.
                            //    http://farm{farm-id}.static.flickr.com/{server-id}/{id}_{secret}_[mstzb].jpg
                            FullImageURL = new Uri("http://farm" + FPI.Farm + ".static.flickr.com/" + FPI.Server + "/" + FPI.PhotoId + "_" + FPI.Secret + "_b.jpg");
                        }

                    }
                    catch (Exception)
                    {
                    }
                    break;

                default:
                    FullImageURL = null;
                    break;
            }
            return FullImageURL;

        }

    }
}
