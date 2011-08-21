using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Core.Email {
    
    public class TemplateParser {
        public Dictionary<string,string> UnMatchedReplacements { get; set; }
        
        public TemplateParser() {
            if (UnMatchedReplacements == null) UnMatchedReplacements = new Dictionary<string, string>();
        }
        
        /// <summary>
        /// Matches and replaces tokens in messageTemplate with values in replacements.
        /// </summary>
        /// <param name="messageTemplate">string containing tokens to be replaced ([EX_TOKEN]).</param>
        /// <param name="replacements">Dictionary of replacements</param>
        /// <returns>messageTemplate with it's tokens replaced</returns>
        public string Parse(string messageTemplate, Dictionary<string, string> replacements) {
            string returnValue = string.Empty;
            
            foreach (KeyValuePair<string, string> replacement in replacements) {
                if (messageTemplate.IndexOf(replacement.Key) <= 0 && !UnMatchedReplacements.ContainsKey(replacement.Key))
                    UnMatchedReplacements.Add(replacement.Key, replacement.Value);

                returnValue += messageTemplate.Replace(replacement.Key, replacement.Value);
            }

            return returnValue;
        }

    }
}