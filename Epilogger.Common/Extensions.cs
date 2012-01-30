using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;

namespace Epilogger
{
    public static class Extensions
    {
        //public static string ToJson(this object obj)
        //{
        //    DataContractJsonSerializer serializer = new DataContractJsonSerializer();
        //    return serializer.ToJson(obj);
        //}
        //public static string ToJson(this object obj, int recursionDepth)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    serializer.RecursionLimit = recursionDepth;
        //    return serializer.Serialize(obj);
        //}


        public static string TruncateAtWord(this string input, int length)
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length);
            return string.Format("{0}...", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }

    }

    
}
