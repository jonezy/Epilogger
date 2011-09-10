using System;
using System.Net.Mail;

namespace Epilogger.Web.Core.Email {
    public class EmailSender : IEmailSender {
        public void Send(SmtpConfiguration smtpConfig, string toAddress, string fromAddress, string subject, string message) {
            // have to add this check to RichmondDay.Helpers
            //if(smtpConfig.IsEmpty())
            if (smtpConfig == null || string.IsNullOrEmpty(smtpConfig.Server) || string.IsNullOrEmpty(smtpConfig.Port.ToString())) {
                throw new ArgumentException("smtpConfig");
            }

            // if there is no from address specified, use the default one.
            if (string.IsNullOrEmpty(fromAddress))
                fromAddress = smtpConfig.DefaultFromAddress;

            if (string.IsNullOrEmpty(toAddress))
                toAddress = smtpConfig.DefaultToAddress;

            // setup the message
            MailMessage email = new MailMessage();
            email.Subject = subject;
            email.Body = message;
            email.From = new MailAddress(fromAddress);
            email.To.Add(new MailAddress(toAddress));
            email.IsBodyHtml = true;

            // setup the smtpclient 
            // only need to add credentials if they are present (production)
            SmtpClient client = new SmtpClient(smtpConfig.Server, smtpConfig.Port);
            if (!string.IsNullOrEmpty(smtpConfig.Username) && !string.IsNullOrEmpty(smtpConfig.Password)) {
                client.Credentials = new System.Net.NetworkCredential(smtpConfig.Username, smtpConfig.Password);
            }
            
            // send the message
            client.Send(email);
        }
    }
}