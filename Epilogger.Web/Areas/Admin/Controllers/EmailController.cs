using System.Collections.Generic;
using System.Web.Mvc;

using Epilogger.Web.Areas.Admin.Models;
using Epilogger.Web.Controllers;
using Epilogger.Web.Core.Email;
using Epilogger.Web.Views.Emails;
using Epilogger.Data;

namespace Epilogger.Web.Areas.Admin.Controllers {
    [RequiresAuthentication(ValidUserRole = UserRoleType.Administrator, AccessDeniedMessage = "You must be an administrator to access this part of the site.")]
    public partial class EmailController : BaseController
    {
        UserService service;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (service == null) service = new UserService();

            base.Initialize(requestContext);
        }

        public virtual ActionResult Index()
        {
            return View(new InviteUsersViewModel());
        }
        [HttpPost]
        public virtual ActionResult Index(InviteUsersViewModel model)
        {
            if (ModelState.IsValid) {
                string[] emailAddresses = model.EmailAddresses.Split(',');
                if (emailAddresses.Length == 0) {
                    this.StoreError("There are no email addresses to send the invite to, please enter at least one.");
                    return View(model);
                }

                int count = 0;
                foreach (var email in emailAddresses) {
                    // if the address isn't in the beta signup table, add it so they will pass the temp check on the create screen.
                    if (!service.IsBetaUser(email)) {
                        BetaSignup signup = new BetaSignup();
                        signup.EmailAddress = email;

                        service.SaveBetaSignup(signup);
                    }
                    TemplateParser parser = new TemplateParser();
                    Dictionary<string, string> replacements = new Dictionary<string, string>();
                    replacements.Add("[BASE_URL]", App.BaseUrl);
                    replacements.Add("[CREATE_ACCOUNT_URL]", string.Format("{0}account/create", App.BaseUrl));

                    string message = parser.Replace(AccountEmails.OldBetaInvite, replacements);

                    EmailSender sender = new EmailSender();
                    sender.Send(App.MailConfiguration, email, "", "You're invited to try the Epilogger alpha!", message);
                    count++;
                }

                this.StoreSuccess("You successfully invited " + count + " people to try out the epilogger alpha!");
            }

            return View(model);
        }
    }
}
