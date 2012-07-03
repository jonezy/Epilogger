using System;
using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Twitterizer;

namespace Epilogger.Web {
    public class UserProfilePictureResolver : ValueResolver<CheckIn, string>
    {
        protected override string ResolveCore(CheckIn source)
        {
            if (source.UserID == null)
                return string.Empty;

            try
            {
                var us = new UserService();
                var theUser = us.GetUserByID(Guid.Parse(source.UserID.ToString()));

                return theUser == null ? string.Empty : theUser.ProfilePicture;

            }
            catch (Exception)
            {
                return string.Empty;
            }

            
        }
    }
}