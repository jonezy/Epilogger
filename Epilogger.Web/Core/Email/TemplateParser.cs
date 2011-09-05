using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        public string Replace(string messageTemplate, Dictionary<string, string> replacements) {
            foreach (KeyValuePair<string, string> replacement in replacements) {
                if (messageTemplate.IndexOf(replacement.Key) <= 0 && !UnMatchedReplacements.ContainsKey(replacement.Key))
                    UnMatchedReplacements.Add(replacement.Key, replacement.Value);

                messageTemplate = messageTemplate.Replace(replacement.Key, replacement.Value);
            }

            return messageTemplate;
        }

        public Dictionary<string, string> ParseTokens(string messageTemplate) {
            MatchCollection matches = Regex.Matches(messageTemplate, @"[[][\w]*]");
            Dictionary<string, string> replacements = new Dictionary<string,string>();
            
            foreach (Match match in matches) {
                replacements.Add(match.Value, "");
	        }

            return replacements;
        }
    }
}