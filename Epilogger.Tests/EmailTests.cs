using System.Collections.Generic;
using Epilogger.Web.Core.Email;
using NUnit.Framework;

namespace Epilogger.Tests {
    public class EmailTests {
        public void replaced_string_should_match() {
            string messageTemplate = "Hi, [FIRST_NAME].  This is a message template";
            string expected = "Hi, Chris.  This is a message template";
            
            Dictionary<string, string> replacements = new Dictionary<string,string>();
            replacements.Add("[FIRST_NAME]", "Chris");
                
            TemplateParser parser = new TemplateParser();
            string actual = parser.Parse(messageTemplate, replacements);

            Assert.AreEqual(expected, actual);
        }
    }
}
