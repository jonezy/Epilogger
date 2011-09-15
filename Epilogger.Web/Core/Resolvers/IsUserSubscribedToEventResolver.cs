using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Twitterizer;
using System;
using RichmondDay.Helpers;

namespace Epilogger.Web {
    public class IsUserSubscribedToEventResolver : ValueResolver<Event, string> {
        protected override string ResolveCore(Event source) {
            Guid userId;
            Guid.TryParse(CookieHelpers.GetCookieValue("lc", "uid").ToString(), out userId);
            UserService service = new UserService();
            User user = service.GetUserByID(userId);

            if (user.UserFollowsEvents.Where(fe => fe.EventID == source.ID).FirstOrDefault() != null)
                return "true";
            else
                return "false";
        }
    }
}