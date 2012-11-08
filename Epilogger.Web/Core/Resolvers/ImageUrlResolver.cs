using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Epilogger.Data;
using Twitterizer;

namespace Epilogger.Web
{
    public class ImageUrlResolver : ValueResolver<Image, string>
    {
        protected override string ResolveCore(Image source)
        {
            var imageUrl = new Uri(string.Format("http://epilogger.com/events/PhotoDetails/{0}/{1}", source.EventID, source.ID));
            return imageUrl.ToString();
        }
    }
}