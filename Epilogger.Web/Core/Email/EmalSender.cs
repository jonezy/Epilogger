using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RichmondDay.Helpers;

namespace Epilogger.Web.Core.Email {
    public class EmailSender : IEmailSender {
        public void Send(SmtpConfiguration smtpConfig, string toAddress, string fromAddress, string subject, string message) {
            // have to add this check to RichmondDay.Helpers
            //if(smtpConfig.IsEmpty())
            if (string.IsNullOrEmpty(smtpConfig.Server) || string.IsNullOrEmpty(smtpConfig.Port.ToString())) {
                throw new ArgumentException("smtpConfig");
            }
            
        }
    }
}