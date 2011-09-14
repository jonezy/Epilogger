using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epilogger.Web.Models;

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
    }
}