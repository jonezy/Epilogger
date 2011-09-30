using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Web.Script.Serialization;

    public class FoursquareVenueClient {
        string GetJSON(string url, string postData, string method) {
            string returnValue = string.Empty;
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(method)) {
                webRequest.Method = method;

                if (!string.IsNullOrEmpty(postData)) {
                    // posting data to a url
                    byte[] byteSend = Encoding.ASCII.GetBytes(postData);
                    webRequest.ContentLength = byteSend.Length;

                    using (Stream streamOut = webRequest.GetRequestStream())
                        streamOut.Write(byteSend, 0, byteSend.Length);
                }
            } else
                webRequest.Method = "GET";

            WebResponse webResponse = webRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                if (streamReader.Peek() > -1) returnValue = streamReader.ReadToEnd();

            return returnValue;
        }

        public FoursquareVenueClient() { }

        public dynamic Execute(string url) {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(GetJSON(url, "", ""));
            var meta = JsonConvert.DeserializeObject<JObject>(dictionary["meta"].ToString());
            var response = JsonConvert.DeserializeObject<JObject>(dictionary["response"].ToString());

            var result = new ExpandoObject();
            var d = result as IDictionary<string, object>;
            d.Add("meta", meta.Values().First());
            d.Add("response", response.Values().Children());

            return result;
        }
    }