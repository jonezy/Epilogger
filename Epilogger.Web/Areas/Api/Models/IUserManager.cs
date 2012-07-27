using System;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface IUserManager
    {
        ApiUser GetUserByID(Guid userId);
        ApiUser GetUserByUsername(string userName);
        ApiUser GetUserByEmail(string email);
        User GetFullUserByUsername(string userName);
        ApiUserFollowsEvent SaveUserFollowsEvent(ApiUserFollowsEvent model);
        bool DeleteEventSubscription(Guid userId, int eventId);
        User LoginWithTwitter(string userName, string token, string tokenSecret);
    }
}