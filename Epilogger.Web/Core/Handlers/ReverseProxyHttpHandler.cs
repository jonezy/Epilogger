using System;
using System.Configuration;
using System.Web;
using System.Net;
using System.Text;
using System.IO;

namespace Epilogger.Web.Core.Handlers
{
    /// <summary>
    /// Handler that intercepts Client's request and delivers the web site
    /// </summary>
    /// <remarks>Inspired by </remarks>

    public class ReverseProxyHttpHandler : IHttpHandler
    {
        /// <summary>
        /// Method calls when client request the server
        /// </summary>
        /// &;lt;param name="context">HTTP context for client</param>

        public void ProcessRequest(HttpContext context)
        {
            var remoteUrl = ParseURL(context.Request.Url.AbsoluteUri);
            
            //create the web request to get the remote stream
            var request = (HttpWebRequest)WebRequest.Create(remoteUrl);
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (System.Net.WebException we)
            {
                //remote url not found, send 404 to client 
                context.Response.StatusCode = 404;
                context.Response.StatusDescription = "Not Found";
                context.Response.Write("<h2>Page not found</h2>");
                context.Response.End();
                return;
            }
            var responseStream = response.GetResponseStream();

            if ((response.ContentType.ToLower().IndexOf("html") >= 0)
              || (response.ContentType.ToLower().IndexOf("javascript") >= 0))
            {
                //this response is HTML Content, so we must parse it
                var readStream = new StreamReader(responseStream, Encoding.UTF8);
                var remoteUri = new Uri(remoteUrl);

                var content = ParseHtmlResponse(readStream.ReadToEnd(), context.Request.ApplicationPath + "/http//" + remoteUri.Host);
                
                //write the updated HTML to the client
                context.Response.Write(content);
                //close streamsreadStream.Close();
                response.Close();
                context.Response.End();
            }
            else
            {
                //the response is not HTML 
                var buff = new byte[1024];
                var bytes = 0;
                while ((bytes = responseStream.Read(buff, 0, 1024)) > 0)
                {
                    //Write the stream directly to the client 
                    context.Response.OutputStream.Write(buff, 0, bytes);
                }
                //close streams
                response.Close();
                context.Response.End();
            }
        }

        /// <summary>
        /// Get the remote URL to call
        /// </summary>
        /// <param name="url">URL get by client</param>
        /// <returns>Remote URL to return to the client</returns>

        public string ParseURL(string url)
        {
            if (url.IndexOf("http/") >= 0)
            {
                string externalUrl = url.Substring(url.IndexOf("http/"));
                return externalUrl.Replace("http/", "http://").Replace("/reverseproxy.axd", String.Empty);
            }
            else
                return url;
        }

        /// <summary>
        /// Parse HTML response for update links and images sources
        /// </summary>
        /// <param name="html">HTML response</param>
        /// <param name="appPath">Path of application for replacement</param>
        /// <returns>HTML updated</returns>
        public string ParseHtmlResponse(string html, string appPath)
        {
            html = html.Replace("\"/", "\"" + appPath + "/");
            html = html.Replace("'/", "'" + appPath + "/");
            html = html.Replace("=/", "=" + appPath + "/");
            return html;
        }
        ///
        /// Specifies whether this instance is reusable by other Http requests
        ///
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}