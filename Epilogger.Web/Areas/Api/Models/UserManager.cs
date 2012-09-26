using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Web.ApplicationServices;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;
using Epilogger.Web.Controllers;
using Epilogger.Web.Core.Stats;
using Epilogger.Web.Models;


namespace Epilogger.Web.Areas.Api.Models
{
    public class UserManager : IUserManager
    {
        UserService _us = new UserService();
        UserAuthenticationProfileService _userAuthService = new UserAuthenticationProfileService();

        public UserManager()
        {
            if (_us == null) _us = new UserService();
            if (_userAuthService == null) _userAuthService = new UserAuthenticationProfileService();
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

        public User LoginWithTwitter(string userName, string token, string tokenSecret)
        {

            // check to see if we already have the screen name in the userauth table.
            var userAuthService = new UserAuthenticationProfileService();
            var userAuth = userAuthService.UserAuthorizationByServiceScreenNameAndPlatform(userName, "Mobile", AuthenticationServices.TWITTER);

            //Yes, there is a Mobile Auth record
            if (userAuth != null)
            {
                userAuth.Token = token;
                userAuth.TokenSecret = tokenSecret;
                userAuthService.Save(userAuth);

                var user = userAuth.Users.FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
            }
            else
            {
                //There is no auth record, this must be a login from Twitter or a new twitter connection to an existing user or an existing mobile user with no Mobile account.

                //Before we create a new user, make sure there isn't a Web token.
                userAuth = userAuthService.UserAuthorizationByServiceScreenNameAndPlatform(userName, "Web", AuthenticationServices.TWITTER);
                if (userAuth == null)
                {
                    //This is a new user, create an account.
                    var us = new UserService();
                    
                    //This is a new login, create the user
                    var theNewUser = new User()
                    {
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true,
                        RoleID = 2,
                        Username = userName + "twitter",
                        Password = " ",
                        EmailAddress = " "
                    };
                    us.Save(theNewUser);
                    userAuth = new UserAuthenticationProfile
                    {
                        UserID = theNewUser.ID,
                        Platform = "Mobile",
                        Service = AuthenticationServices.TWITTER.ToString(),
                        ServiceUsername = userName,
                        Token = token,
                        TokenSecret = tokenSecret
                    };
                    userAuthService.Save(userAuth);

                    return theNewUser;
                }
                
                
                //They have an account already, it was create on Web. Now add the Mobile token.
                var userAuth2 = new UserAuthenticationProfile
                {
                    UserID = userAuth.UserID,
                    Platform = "Mobile",
                    Service = AuthenticationServices.TWITTER.ToString(),
                    ServiceUsername = userName,
                    Token = token,
                    TokenSecret = tokenSecret
                };
                userAuthService.Save(userAuth2);

                var user = userAuth.Users.FirstOrDefault();
                return user;

            }

            return null;
            
        }


        public User LoginWithFacebook(string userName, string token)
        {

            // check to see if we already have the screen name in the userauth table.
            var userAuthService = new UserAuthenticationProfileService();
            var userAuth = userAuthService.UserAuthorizationByServiceScreenNameAndPlatform(userName, "Mobile", AuthenticationServices.FACEBOOK);

            //Yes, there is a Mobile Auth record
            if (userAuth != null)
            {
                userAuth.Token = token;
                userAuth.TokenSecret = null;
                userAuthService.Save(userAuth);

                var user = userAuth.Users.FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
            }
            else
            {
                //There is no auth record, this must be a login from Twitter or a new twitter connection to an existing user or an existing mobile user with no Mobile account.

                //Before we create a new user, make sure there isn't a Web token.
                userAuth = userAuthService.UserAuthorizationByServiceScreenNameAndPlatform(userName, "Web", AuthenticationServices.FACEBOOK);
                if (userAuth == null)
                {
                    //This is a new user, create an account.
                    var us = new UserService();

                    //This is a new login, create the user
                    var theNewUser = new User()
                    {
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true,
                        RoleID = 2,
                        Username = userName + "facebook",
                        Password = " ",
                        EmailAddress = " "
                    };
                    us.Save(theNewUser);
                    userAuth = new UserAuthenticationProfile
                    {
                        UserID = theNewUser.ID,
                        Platform = "Mobile",
                        Service = AuthenticationServices.TWITTER.ToString(),
                        ServiceUsername = userName,
                        Token = token,
                        TokenSecret = null
                    };
                    userAuthService.Save(userAuth);

                    return theNewUser;
                }


                //They have an account already, it was create on Web. Now add the Mobile token.
                var userAuth2 = new UserAuthenticationProfile
                {
                    UserID = userAuth.UserID,
                    Platform = "Mobile",
                    Service = AuthenticationServices.TWITTER.ToString(),
                    ServiceUsername = userName,
                    Token = token,
                    TokenSecret = null
                };
                userAuthService.Save(userAuth2);

                var user = userAuth.Users.FirstOrDefault();
                return user;

            }

            return null;

        }

        public bool ConnectAuthAccountToUser(Guid userId, string authService, string authScreenName, string authToken, string authTokenSecret, string platform)
        {
            return AccountController.ConnectAuthAccountToUser(userId, authService, authScreenName, authToken, authTokenSecret,
                                                       platform);

        }


        public bool DisconnectAuthAccount(AuthenticationServices authService, Guid userId)
        {
            try
            {
                new UserAuthenticationProfileService().DisconnectService(authService, userId, "Mobile");
                return true;
            }
            catch(Exception)
            {
                return false;    
            }

        }



    }
 
}