using System.Collections.Generic;
using Epilogger.Web.Core.Email;
using NUnit.Framework;
using System;
using RichmondDay.Helpers;

namespace Epilogger.Tests {

    [TestFixture]
    public class TemplateParserTests {

        string messageTemplate = "Hi, [FIRST_NAME].  This is a message template";
        string multipleMessageTemplate = "Hi, [FIRST_NAME] [LAST_NAME].  This is a message template";

        TemplateParser parser;
        Dictionary<string, string> validReplacements;
        Dictionary<string, string> invalidReplacements;

        [TestFixtureSetUp]
        public void Setup() {
            parser = new TemplateParser();

            validReplacements = new Dictionary<string, string>();
            validReplacements.Add("[FIRST_NAME]", "Chris");

            invalidReplacements = new Dictionary<string, string>();
            invalidReplacements.Add("[LAST_NAME]", "Jones");
        }

        [Test]
        public void string_with_valid_replacements_should_return_replaced_string() {
            string expected = "Hi, Chris.  This is a message template";
            string actual = parser.Replace(messageTemplate, validReplacements);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void string_with_invalid_replacements_should_return_original_string() {
            string expected = "Hi, [FIRST_NAME].  This is a message template";
            string actual = parser.Replace(messageTemplate, invalidReplacements);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void string_with_invlaid_replacesments_should_log_unmatched_replacement() {
            string output = parser.Replace(messageTemplate, invalidReplacements);

            Assert.GreaterOrEqual(parser.UnMatchedReplacements.Count, 1);
        }

        [Test]
        public void string_with_invlaid_replacements_should_log_correct_unmatched_replacement() {
            string output = parser.Replace(messageTemplate, invalidReplacements);

            Assert.IsTrue(parser.UnMatchedReplacements.ContainsKey("[LAST_NAME]"));
        }

        [Test]
        public void string_with_tokens_should_generate_replacement_dictionary_with_single() {
            Dictionary<string, string> replacements = parser.ParseTokens(messageTemplate);

            Assert.AreEqual(replacements.Count, 1);
        }

        [Test]
        public void string_with_multiple_tokens_should_generate_replacement_dictionary_with_multiple() {
            Dictionary<string, string> replacements = parser.ParseTokens(multipleMessageTemplate);

            Assert.AreEqual(replacements.Count, 2);
        }

    }

    [TestFixture]
    public class EmailTests {

        [Test]
        public void sending_email_with_empty_config_should_throw_error() {
            IEmailSender sender = new EmailSender();

            try {
                sender.Send(null, "", "", "", "");
            } catch (ArgumentException ex) {
                Assert.AreEqual(typeof(System.ArgumentException), ex);
            }
        }
    }
}
