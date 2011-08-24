using RichmondDay.Helpers;

namespace Epilogger.Web.Core.Email {
    public interface IEmailSender {
        void Send(SmtpConfiguration smtpConfig, string toAddress, string fromAddress, string subject, string message);
    }
}
