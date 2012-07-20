using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;
using Epilogger.Web.Core.Stats;
using Epilogger.Web.Models;


namespace Epilogger.Web.Areas.Api.Models
{
    public class UserManager : IUserManager
    {
        UserService _us = new UserService();

        public UserManager()
        {
            if (_us == null) _us = new UserService();    
        }

        //****************

        public ApiUser GetUserByID(Guid userId)
        {
            return Mapper.Map<User, ApiUser>(_us.GetUserByID(userId));
        }

        public ApiUser GetUserByUsername(string userName)
        {
            return Mapper.Map<User, ApiUser>(_us.GetUserByUsername(userName));
        }

        public User GetFullUserByUsername(string userName)
        {
            return _us.GetUserByUsername(userName);
        }
        
        public ApiUser GetUserByEmail(string email)
        {
            return Mapper.Map<User, ApiUser>(_us.GetUserByEmail(email));
        }


        public ApiUserFollowsEvent SaveUserFollowsEvent(ApiUserFollowsEvent model)
        {
            var newmodel = Mapper.Map<ApiUserFollowsEvent, UserFollowsEvent>(model);
            return Mapper.Map<UserFollowsEvent, ApiUserFollowsEvent>(_us.SaveUserFollowsEvent(newmodel));
        }

        public bool DeleteEventSubscription(Guid userId, int eventId)
        {
            try
            {
                _us.DeleteEventSubscription(userId, eventId);
                return true;
            }
            catch (Exception)
            {
                return true;
            }
            
            
        }

    }
 
}