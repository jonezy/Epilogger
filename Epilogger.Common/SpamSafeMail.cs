using System;
using System.Configuration;
using System.Net.Mail;

namespace Epilogger.Common
{
    public class SpamSafeMail
    {
        public MailAddressCollection ToEmailAddresses = new MailAddressCollection();
        public MailAddressCollection BCCEmailAddresses = new MailAddressCollection();
        private string _htmlEmail;
        private string _textEmail;

        private string _emailSubject;
        public string HtmlEmail
        {
            get { return _htmlEmail; }
            set { _htmlEmail = value; }
        }
        public string TextEmail
        {
            get { return _textEmail; }
            set { _textEmail = value; }
        }
        public string EmailSubject
        {
            get { return _emailSubject; }
            set { _emailSubject = value; }
        }

        public void SendMail()
        {
            //*************************************************
            //Chris Brooker - Anti-Spam Email Code Aug 19, 2009
            //*************************************************

            var objEMail = new MailMessage();

            var defaultFromAddress =
                ConfigurationManager.AppSettings["SiteSettings.Mail.DefaultFromAddress"] as string ?? "";

            //
            //From
            objEMail.Headers.Add("Message-ID", "<" + defaultFromAddress + ">");
            objEMail.From = new MailAddress(defaultFromAddress);
            objEMail.Sender = new MailAddress(defaultFromAddress);

            //
            //To
            foreach (MailAddress myMailAddress in ToEmailAddresses)
            {
                objEMail.To.Add(myMailAddress);
            }

            //
            //BCC
            foreach (MailAddress myMailAddress in BCCEmailAddresses)
            {
                objEMail.Bcc.Add(myMailAddress);
            }

            //
            //Properly Formatted Subject
            objEMail.SubjectEncoding = System.Text.Encoding.UTF8;
            objEMail.Subject = _emailSubject;

            //
            //Define HTML Message
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(_htmlEmail);

            htmlView.ContentType = new System.Net.Mime.ContentType("text/html");
            htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit;

            //
            //Define Txt Message
            System.Net.Mail.AlternateView textView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(_textEmail);
            textView.ContentType = new System.Net.Mime.ContentType("text/plain");
            textView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit;

            //
            //Define the Main Message Types
            objEMail.IsBodyHtml = true;
            objEMail.BodyEncoding = System.Text.Encoding.UTF8;

            objEMail.AlternateViews.Add(htmlView);
            //objEMail.AlternateViews.Add(textView);

            //
            //Define the SMTP Server Information
            var smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SiteSettings.Mail.Server"] as string ?? "") { 
                                                                                                                              DeliveryMethod = SmtpDeliveryMethod.Network, 
                                                                                                                              UseDefaultCredentials = false, 
                                                                                                                              Port = int.Parse(ConfigurationManager.AppSettings["SiteSettings.Mail.ServerPort"]),
                                                                                                                              EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SiteSettings.Mail.EnableSsl"])
                                                                                                                          };

            //
            //Set the SMPT Crudentials
            var nCrudentials = new System.Net.NetworkCredential { 
                                                                    UserName = ConfigurationManager.AppSettings["SiteSettings.Mail.Username"] as string ?? "",
                                                                    Password = ConfigurationManager.AppSettings["SiteSettings.Mail.Password"] as string ?? ""
                                                                };
            smtpClient.Credentials = nCrudentials;

            //
            //Finally, Send the Email
            smtpClient.Send(objEMail);
        }

    }

}
