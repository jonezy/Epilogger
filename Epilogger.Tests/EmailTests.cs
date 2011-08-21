using System.Collections.Generic;
using Epilogger.Web.Core.Email;
using NUnit.Framework;

namespace Epilogger.Tests {
    [TestFixture]
    public class EmailTests {
        [Test]
        public void string_with_valid_replacements_should_return_replaced_string() {
            string messageTemplate = "Hi, [FIRST_NAME].  This is a message template";
            string expected = "Hi, Chris.  This is a message template";
            
            Dictionary<string, string> replacements = new Dictionary<string,string>();
            replacements.Add("[FIRST_NAME]", "Chris");
                
            TemplateParser parser = new TemplateParser();
            string actual = parser.Parse(messageTemplate, replacements);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void string_with_invalid_replacements_should_return_original_string() {
            string messageTemplate = "Hi, [FIRST_NAME].  This is a message template";
            string expected = "Hi, [FIRST_NAME].  This is a message template";

            Dictionary<string, string> replacements = new Dictionary<string, string>();
            replacements.Add("[LAST_NAME]", "Jones");

            TemplateParser parser = new TemplateParser();
            string actual = parser.Parse(messageTemplate, replacements);

            Assert.AreEqual(expected, actual);
        }
    }
}
