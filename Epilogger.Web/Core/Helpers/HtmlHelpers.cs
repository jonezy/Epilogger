﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epilogger.Web.Models;
using TagCloud;

namespace System.Web.Mvc {
    public static class HtmlHelpers {
        public static MvcHtmlString RenderConnectSocialNetworkUI(this HtmlHelper helper, string network, List<ConnectedNetworksViewModel> connectedNetworks) {

            ConnectedNetworksViewModel desiredNetwork = connectedNetworks.Where(cn => cn.Service.ToLower() == network.ToLower()).FirstOrDefault();

            bool connect = desiredNetwork == null || string.IsNullOrEmpty(desiredNetwork.ServiceUsername);
            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string url = urlHelper.Action(connect ? "ConnectRequest" : "Disconnect", network, new { area = "Authentication" });

            StringBuilder returnValue = new StringBuilder();
            returnValue.Append("<div class='social_connections'>");
            returnValue.AppendFormat("<p class='remove-bottom'><strong>{0}</strong></p>", network);
            
            if (!connect)
                returnValue.AppendFormat("<p class='remove-bottom'>Connected as {0}</p>", desiredNetwork.ServiceUsername);

                returnValue.AppendFormat("<p class='remove-bottom'><a href='{0}' class='button'>{1}</a></p>", url, connect ? "Connect" : "Disconnect");

            returnValue.Append("</div>");

            return MvcHtmlString.Create(returnValue.ToString());
        }


        /// <summary>
        /// Creates tag cloud html from a provided list of string tags. 
        /// The more times a tag occures in the list, the larger weight it will get in the tag cloud.
        /// </summary>
        /// <param name="tags">A string list of tags</param>
        /// <param name="generationRules">A TagCloudGenerationRules object to decide how the cloud is generated.</param>
        /// <returns>A string containing the html markup of the tag cloud.</returns>
        public static string TagCloud(this HtmlHelper htmlHelper, IEnumerable<string> tags, TagCloudGenerationRules generationRules)
        {
            return new TagCloud.TagCloud(tags, generationRules).ToString();
        }

        /// <summary>
        /// Creates tag cloud html from a provided dictionary of string tags along with integer values indicating the weight of each tag. 
        /// This overload is suitable when you have a list of already weighted tags, i.e. from a database query result.
        /// </summary>
        /// <param name="weightedTags">A dictionary that takes a string for the tag text (as the dictionary key) and an integer for the tag weight (as the dictionary value).</param>
        /// <param name="generationRules">A TagCloudGenerationRules object to decide how the cloud is generated.</param>
        /// <returns>A string containing the html markup of the tag cloud.</returns>
        public static string TagCloud(this HtmlHelper htmlHelper, IDictionary<string, int> weightedTags, TagCloudGenerationRules generationRules)
        {
            return new TagCloud.TagCloud(weightedTags, generationRules).ToString();
        }

        /// <summary>
        /// Creates tag cloud html from a provided list of string tags along with integer values indicating the weight of each tag. 
        /// This overload is suitable when you have a list of already weighted tags, i.e. from a database query result.
        /// </summary>
        /// <param name="weightedTags">A list of KeyValuePair objects that take a string for the tag text and an integer for the weight of the tag.</param>
        /// <param name="generationRules">A TagCloudGenerationRules object to decide how the cloud is generated.</param>
        /// <returns>A string containing the html markup of the tag cloud.</returns>
        public static string TagCloud(this HtmlHelper htmlHelper, IEnumerable<KeyValuePair<string, int>> weightedTags, TagCloudGenerationRules generationRules)
        {
            return new TagCloud.TagCloud(weightedTags, generationRules).ToString();
        }


        /// <summary>
        /// Creates tag cloud html from a provided list of Tag objects.
        /// Use this overload to have full control over the content in the cloud.
        /// </summary>
        /// <param name="tags">Tag objects used to generate the tag cloud.</param>
        /// <returns>A string containing the html markup of the tag cloud.</returns>
        public static string TagCloud(this HtmlHelper htmlHelper, IEnumerable<Tag> tags)
        {
            return new TagCloud.TagCloud(tags).ToString();
        }

    }
}