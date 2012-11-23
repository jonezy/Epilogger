using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;
using Twitterizer;

namespace Epilogger.Web
{
    public class ApiImageUrlResolver : ValueResolver<ApiImage, string>
    {
        protected override string ResolveCore(ApiImage source)
        {
            var imageUrl = new Uri(string.Format("http://epilogger.com/events/PhotoDetails/{0}/{1}", source.EventID, source.ID));
            return imageUrl.ToString();
        }
    }
}