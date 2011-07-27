using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

public static class UrlHelpers {
    public static string CreateUrlSlug(this string value) {
        if (String.IsNullOrEmpty(value)) return "";

        value = Regex.Replace(value, @"&\w+;", "");             // remove entities
        value = Regex.Replace(value, @"[^A-Za-z0-9\-\s]", "");  // remove anything that is not letters, numbers, dash, or space
        value = value.Trim();                                   // remove any leading or trailing spaces left over
        value = Regex.Replace(value, @"\s+", "-");              // replace spaces with single dash
        value = Regex.Replace(value, @"\-{2,}", "-");           // if we end up with multiple dashes, collapse to single dash
        value = value.ToLower();                                // make it all lower case

        if (value.Length > 80)                                  // if it's too long, clip it
            value = value.Substring(0, 79);

        if (value.EndsWith("-"))                                // remove trailing dash, if there is one
            value = value.Substring(0, value.Length - 1);

        return value;
    }
}
