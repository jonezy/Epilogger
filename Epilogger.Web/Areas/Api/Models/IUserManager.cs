using System;
using Epilogger.Web.Areas.Api.Models.Classes;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface IUserManager
    {
        ApiUser GetUserByID(Guid userId);
        ApiUser GetUserByUsername(string userName);
        ApiUser GetUserByEmail(string email);
    }
}