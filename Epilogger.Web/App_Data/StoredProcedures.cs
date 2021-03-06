﻿


  
using System;
using SubSonic;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Epilogger.Data{
	public partial class EpiloggerDB{

        public StoredProcedure aspnet_Membership_CreateUser(string ApplicationName,string UserName,string Password,string PasswordSalt,string Email,string PasswordQuestion,string PasswordAnswer,bool IsApproved,DateTime CurrentTimeUtc,DateTime CreateDate,int UniqueEmail,int PasswordFormat,Guid UserId){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_CreateUser",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("Password",Password,DbType.String);
            sp.Command.AddParameter("PasswordSalt",PasswordSalt,DbType.String);
            sp.Command.AddParameter("Email",Email,DbType.String);
            sp.Command.AddParameter("PasswordQuestion",PasswordQuestion,DbType.String);
            sp.Command.AddParameter("PasswordAnswer",PasswordAnswer,DbType.String);
            sp.Command.AddParameter("IsApproved",IsApproved,DbType.Boolean);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            sp.Command.AddParameter("CreateDate",CreateDate,DbType.DateTime);
            sp.Command.AddParameter("UniqueEmail",UniqueEmail,DbType.Int32);
            sp.Command.AddParameter("PasswordFormat",PasswordFormat,DbType.Int32);
            sp.Command.AddParameter("UserId",UserId,DbType.Guid);
            return sp;
        }
        public StoredProcedure aspnet_Membership_FindUsersByEmail(string ApplicationName,string EmailToMatch,int PageIndex,int PageSize){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_FindUsersByEmail",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("EmailToMatch",EmailToMatch,DbType.String);
            sp.Command.AddParameter("PageIndex",PageIndex,DbType.Int32);
            sp.Command.AddParameter("PageSize",PageSize,DbType.Int32);
            return sp;
        }
        public StoredProcedure aspnet_Membership_FindUsersByName(string ApplicationName,string UserNameToMatch,int PageIndex,int PageSize){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_FindUsersByName",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserNameToMatch",UserNameToMatch,DbType.String);
            sp.Command.AddParameter("PageIndex",PageIndex,DbType.Int32);
            sp.Command.AddParameter("PageSize",PageSize,DbType.Int32);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetAllUsers(string ApplicationName,int PageIndex,int PageSize){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetAllUsers",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("PageIndex",PageIndex,DbType.Int32);
            sp.Command.AddParameter("PageSize",PageSize,DbType.Int32);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetNumberOfUsersOnline(string ApplicationName,int MinutesSinceLastInActive,DateTime CurrentTimeUtc){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetNumberOfUsersOnline",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("MinutesSinceLastInActive",MinutesSinceLastInActive,DbType.Int32);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetPassword(string ApplicationName,string UserName,int MaxInvalidPasswordAttempts,int PasswordAttemptWindow,DateTime CurrentTimeUtc,string PasswordAnswer){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetPassword",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("MaxInvalidPasswordAttempts",MaxInvalidPasswordAttempts,DbType.Int32);
            sp.Command.AddParameter("PasswordAttemptWindow",PasswordAttemptWindow,DbType.Int32);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            sp.Command.AddParameter("PasswordAnswer",PasswordAnswer,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetPasswordWithFormat(string ApplicationName,string UserName,bool UpdateLastLoginActivityDate,DateTime CurrentTimeUtc){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetPasswordWithFormat",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("UpdateLastLoginActivityDate",UpdateLastLoginActivityDate,DbType.Boolean);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetUserByEmail(string ApplicationName,string Email){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetUserByEmail",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("Email",Email,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetUserByName(string ApplicationName,string UserName,DateTime CurrentTimeUtc,bool UpdateLastActivity){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetUserByName",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            sp.Command.AddParameter("UpdateLastActivity",UpdateLastActivity,DbType.Boolean);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetUserByUserId(Guid UserId,DateTime CurrentTimeUtc,bool UpdateLastActivity){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetUserByUserId",this.Provider);
            sp.Command.AddParameter("UserId",UserId,DbType.Guid);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            sp.Command.AddParameter("UpdateLastActivity",UpdateLastActivity,DbType.Boolean);
            return sp;
        }
        public StoredProcedure aspnet_Membership_ResetPassword(string ApplicationName,string UserName,string NewPassword,int MaxInvalidPasswordAttempts,int PasswordAttemptWindow,string PasswordSalt,DateTime CurrentTimeUtc,int PasswordFormat,string PasswordAnswer){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_ResetPassword",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("NewPassword",NewPassword,DbType.String);
            sp.Command.AddParameter("MaxInvalidPasswordAttempts",MaxInvalidPasswordAttempts,DbType.Int32);
            sp.Command.AddParameter("PasswordAttemptWindow",PasswordAttemptWindow,DbType.Int32);
            sp.Command.AddParameter("PasswordSalt",PasswordSalt,DbType.String);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            sp.Command.AddParameter("PasswordFormat",PasswordFormat,DbType.Int32);
            sp.Command.AddParameter("PasswordAnswer",PasswordAnswer,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_Membership_SetPassword(string ApplicationName,string UserName,string NewPassword,string PasswordSalt,DateTime CurrentTimeUtc,int PasswordFormat){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_SetPassword",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("NewPassword",NewPassword,DbType.String);
            sp.Command.AddParameter("PasswordSalt",PasswordSalt,DbType.String);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            sp.Command.AddParameter("PasswordFormat",PasswordFormat,DbType.Int32);
            return sp;
        }
        public StoredProcedure aspnet_Membership_UnlockUser(string ApplicationName,string UserName){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_UnlockUser",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_Membership_UpdateUser(string ApplicationName,string UserName,string Email,string Comment,bool IsApproved,DateTime LastLoginDate,DateTime LastActivityDate,int UniqueEmail,DateTime CurrentTimeUtc){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_UpdateUser",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("Email",Email,DbType.String);
            sp.Command.AddParameter("Comment",Comment,DbType.String);
            sp.Command.AddParameter("IsApproved",IsApproved,DbType.Boolean);
            sp.Command.AddParameter("LastLoginDate",LastLoginDate,DbType.DateTime);
            sp.Command.AddParameter("LastActivityDate",LastActivityDate,DbType.DateTime);
            sp.Command.AddParameter("UniqueEmail",UniqueEmail,DbType.Int32);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_Membership_UpdateUserInfo(string ApplicationName,string UserName,bool IsPasswordCorrect,bool UpdateLastLoginActivityDate,int MaxInvalidPasswordAttempts,int PasswordAttemptWindow,DateTime CurrentTimeUtc,DateTime LastLoginDate,DateTime LastActivityDate){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_UpdateUserInfo",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("IsPasswordCorrect",IsPasswordCorrect,DbType.Boolean);
            sp.Command.AddParameter("UpdateLastLoginActivityDate",UpdateLastLoginActivityDate,DbType.Boolean);
            sp.Command.AddParameter("MaxInvalidPasswordAttempts",MaxInvalidPasswordAttempts,DbType.Int32);
            sp.Command.AddParameter("PasswordAttemptWindow",PasswordAttemptWindow,DbType.Int32);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            sp.Command.AddParameter("LastLoginDate",LastLoginDate,DbType.DateTime);
            sp.Command.AddParameter("LastActivityDate",LastActivityDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_Paths_CreatePath(Guid ApplicationId,string Path,Guid PathId){
            StoredProcedure sp=new StoredProcedure("aspnet_Paths_CreatePath",this.Provider);
            sp.Command.AddParameter("ApplicationId",ApplicationId,DbType.Guid);
            sp.Command.AddParameter("Path",Path,DbType.String);
            sp.Command.AddParameter("PathId",PathId,DbType.Guid);
            return sp;
        }
        public StoredProcedure aspnet_Personalization_GetApplicationId(string ApplicationName,Guid ApplicationId){
            StoredProcedure sp=new StoredProcedure("aspnet_Personalization_GetApplicationId",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("ApplicationId",ApplicationId,DbType.Guid);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAdministration_DeleteAllState(bool AllUsersScope,string ApplicationName,int Count){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAdministration_DeleteAllState",this.Provider);
            sp.Command.AddParameter("AllUsersScope",AllUsersScope,DbType.Boolean);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("Count",Count,DbType.Int32);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAdministration_FindState(bool AllUsersScope,string ApplicationName,int PageIndex,int PageSize,string Path,string UserName,DateTime InactiveSinceDate){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAdministration_FindState",this.Provider);
            sp.Command.AddParameter("AllUsersScope",AllUsersScope,DbType.Boolean);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("PageIndex",PageIndex,DbType.Int32);
            sp.Command.AddParameter("PageSize",PageSize,DbType.Int32);
            sp.Command.AddParameter("Path",Path,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("InactiveSinceDate",InactiveSinceDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAdministration_GetCountOfState(int Count,bool AllUsersScope,string ApplicationName,string Path,string UserName,DateTime InactiveSinceDate){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAdministration_GetCountOfState",this.Provider);
            sp.Command.AddParameter("Count",Count,DbType.Int32);
            sp.Command.AddParameter("AllUsersScope",AllUsersScope,DbType.Boolean);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("Path",Path,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("InactiveSinceDate",InactiveSinceDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAdministration_ResetSharedState(int Count,string ApplicationName,string Path){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAdministration_ResetSharedState",this.Provider);
            sp.Command.AddParameter("Count",Count,DbType.Int32);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("Path",Path,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAdministration_ResetUserState(int Count,string ApplicationName,DateTime InactiveSinceDate,string UserName,string Path){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAdministration_ResetUserState",this.Provider);
            sp.Command.AddParameter("Count",Count,DbType.Int32);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("InactiveSinceDate",InactiveSinceDate,DbType.DateTime);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("Path",Path,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAllUsers_GetPageSettings(string ApplicationName,string Path){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAllUsers_GetPageSettings",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("Path",Path,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAllUsers_ResetPageSettings(string ApplicationName,string Path){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAllUsers_ResetPageSettings",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("Path",Path,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAllUsers_SetPageSettings(string ApplicationName,string Path,byte[] PageSettings,DateTime CurrentTimeUtc){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAllUsers_SetPageSettings",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("Path",Path,DbType.String);
            sp.Command.AddParameter("PageSettings",PageSettings,DbType.Binary);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationPerUser_GetPageSettings(string ApplicationName,string UserName,string Path,DateTime CurrentTimeUtc){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationPerUser_GetPageSettings",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("Path",Path,DbType.String);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationPerUser_ResetPageSettings(string ApplicationName,string UserName,string Path,DateTime CurrentTimeUtc){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationPerUser_ResetPageSettings",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("Path",Path,DbType.String);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationPerUser_SetPageSettings(string ApplicationName,string UserName,string Path,byte[] PageSettings,DateTime CurrentTimeUtc){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationPerUser_SetPageSettings",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("Path",Path,DbType.String);
            sp.Command.AddParameter("PageSettings",PageSettings,DbType.Binary);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_Profile_DeleteInactiveProfiles(string ApplicationName,int ProfileAuthOptions,DateTime InactiveSinceDate){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_DeleteInactiveProfiles",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("ProfileAuthOptions",ProfileAuthOptions,DbType.Int32);
            sp.Command.AddParameter("InactiveSinceDate",InactiveSinceDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_Profile_DeleteProfiles(string ApplicationName,string UserNames){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_DeleteProfiles",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserNames",UserNames,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_Profile_GetNumberOfInactiveProfiles(string ApplicationName,int ProfileAuthOptions,DateTime InactiveSinceDate){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_GetNumberOfInactiveProfiles",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("ProfileAuthOptions",ProfileAuthOptions,DbType.Int32);
            sp.Command.AddParameter("InactiveSinceDate",InactiveSinceDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_Profile_GetProfiles(string ApplicationName,int ProfileAuthOptions,int PageIndex,int PageSize,string UserNameToMatch,DateTime InactiveSinceDate){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_GetProfiles",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("ProfileAuthOptions",ProfileAuthOptions,DbType.Int32);
            sp.Command.AddParameter("PageIndex",PageIndex,DbType.Int32);
            sp.Command.AddParameter("PageSize",PageSize,DbType.Int32);
            sp.Command.AddParameter("UserNameToMatch",UserNameToMatch,DbType.String);
            sp.Command.AddParameter("InactiveSinceDate",InactiveSinceDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_Profile_GetProperties(string ApplicationName,string UserName,DateTime CurrentTimeUtc){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_GetProperties",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_Profile_SetProperties(string ApplicationName,string PropertyNames,string PropertyValuesString,byte[] PropertyValuesBinary,string UserName,bool IsUserAnonymous,DateTime CurrentTimeUtc){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_SetProperties",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("PropertyNames",PropertyNames,DbType.String);
            sp.Command.AddParameter("PropertyValuesString",PropertyValuesString,DbType.String);
            sp.Command.AddParameter("PropertyValuesBinary",PropertyValuesBinary,DbType.Binary);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("IsUserAnonymous",IsUserAnonymous,DbType.Boolean);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_RegisterSchemaVersion(string Feature,string CompatibleSchemaVersion,bool IsCurrentVersion,bool RemoveIncompatibleSchema){
            StoredProcedure sp=new StoredProcedure("aspnet_RegisterSchemaVersion",this.Provider);
            sp.Command.AddParameter("Feature",Feature,DbType.String);
            sp.Command.AddParameter("CompatibleSchemaVersion",CompatibleSchemaVersion,DbType.String);
            sp.Command.AddParameter("IsCurrentVersion",IsCurrentVersion,DbType.Boolean);
            sp.Command.AddParameter("RemoveIncompatibleSchema",RemoveIncompatibleSchema,DbType.Boolean);
            return sp;
        }
        public StoredProcedure aspnet_Roles_CreateRole(string ApplicationName,string RoleName){
            StoredProcedure sp=new StoredProcedure("aspnet_Roles_CreateRole",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("RoleName",RoleName,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_Roles_DeleteRole(string ApplicationName,string RoleName,bool DeleteOnlyIfRoleIsEmpty){
            StoredProcedure sp=new StoredProcedure("aspnet_Roles_DeleteRole",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("RoleName",RoleName,DbType.String);
            sp.Command.AddParameter("DeleteOnlyIfRoleIsEmpty",DeleteOnlyIfRoleIsEmpty,DbType.Boolean);
            return sp;
        }
        public StoredProcedure aspnet_Roles_GetAllRoles(string ApplicationName){
            StoredProcedure sp=new StoredProcedure("aspnet_Roles_GetAllRoles",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_Roles_RoleExists(string ApplicationName,string RoleName){
            StoredProcedure sp=new StoredProcedure("aspnet_Roles_RoleExists",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("RoleName",RoleName,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_Setup_RemoveAllRoleMembers(string name){
            StoredProcedure sp=new StoredProcedure("aspnet_Setup_RemoveAllRoleMembers",this.Provider);
            sp.Command.AddParameter("name",name,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_Setup_RestorePermissions(string name){
            StoredProcedure sp=new StoredProcedure("aspnet_Setup_RestorePermissions",this.Provider);
            sp.Command.AddParameter("name",name,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_UnRegisterSchemaVersion(string Feature,string CompatibleSchemaVersion){
            StoredProcedure sp=new StoredProcedure("aspnet_UnRegisterSchemaVersion",this.Provider);
            sp.Command.AddParameter("Feature",Feature,DbType.String);
            sp.Command.AddParameter("CompatibleSchemaVersion",CompatibleSchemaVersion,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_Users_CreateUser(Guid ApplicationId,string UserName,bool IsUserAnonymous,DateTime LastActivityDate,Guid UserId){
            StoredProcedure sp=new StoredProcedure("aspnet_Users_CreateUser",this.Provider);
            sp.Command.AddParameter("ApplicationId",ApplicationId,DbType.Guid);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("IsUserAnonymous",IsUserAnonymous,DbType.Boolean);
            sp.Command.AddParameter("LastActivityDate",LastActivityDate,DbType.DateTime);
            sp.Command.AddParameter("UserId",UserId,DbType.Guid);
            return sp;
        }
        public StoredProcedure aspnet_Users_DeleteUser(string ApplicationName,string UserName,int TablesToDeleteFrom,int NumTablesDeletedFrom){
            StoredProcedure sp=new StoredProcedure("aspnet_Users_DeleteUser",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("TablesToDeleteFrom",TablesToDeleteFrom,DbType.Int32);
            sp.Command.AddParameter("NumTablesDeletedFrom",NumTablesDeletedFrom,DbType.Int32);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_AddUsersToRoles(string ApplicationName,string UserNames,string RoleNames,DateTime CurrentTimeUtc){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_AddUsersToRoles",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserNames",UserNames,DbType.String);
            sp.Command.AddParameter("RoleNames",RoleNames,DbType.String);
            sp.Command.AddParameter("CurrentTimeUtc",CurrentTimeUtc,DbType.DateTime);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_FindUsersInRole(string ApplicationName,string RoleName,string UserNameToMatch){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_FindUsersInRole",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("RoleName",RoleName,DbType.String);
            sp.Command.AddParameter("UserNameToMatch",UserNameToMatch,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_GetRolesForUser(string ApplicationName,string UserName){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_GetRolesForUser",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_GetUsersInRoles(string ApplicationName,string RoleName){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_GetUsersInRoles",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("RoleName",RoleName,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_IsUserInRole(string ApplicationName,string UserName,string RoleName){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_IsUserInRole",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserName",UserName,DbType.String);
            sp.Command.AddParameter("RoleName",RoleName,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_RemoveUsersFromRoles(string ApplicationName,string UserNames,string RoleNames){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_RemoveUsersFromRoles",this.Provider);
            sp.Command.AddParameter("ApplicationName",ApplicationName,DbType.String);
            sp.Command.AddParameter("UserNames",UserNames,DbType.String);
            sp.Command.AddParameter("RoleNames",RoleNames,DbType.String);
            return sp;
        }
        public StoredProcedure aspnet_WebEvent_LogEvent(string EventId,DateTime EventTimeUtc,DateTime EventTime,string EventType,decimal EventSequence,decimal EventOccurrence,int EventCode,int EventDetailCode,string Message,string ApplicationPath,string ApplicationVirtualPath,string MachineName,string RequestUrl,string ExceptionType,string Details){
            StoredProcedure sp=new StoredProcedure("aspnet_WebEvent_LogEvent",this.Provider);
            sp.Command.AddParameter("EventId",EventId,DbType.AnsiStringFixedLength);
            sp.Command.AddParameter("EventTimeUtc",EventTimeUtc,DbType.DateTime);
            sp.Command.AddParameter("EventTime",EventTime,DbType.DateTime);
            sp.Command.AddParameter("EventType",EventType,DbType.String);
            sp.Command.AddParameter("EventSequence",EventSequence,DbType.Decimal);
            sp.Command.AddParameter("EventOccurrence",EventOccurrence,DbType.Decimal);
            sp.Command.AddParameter("EventCode",EventCode,DbType.Int32);
            sp.Command.AddParameter("EventDetailCode",EventDetailCode,DbType.Int32);
            sp.Command.AddParameter("Message",Message,DbType.String);
            sp.Command.AddParameter("ApplicationPath",ApplicationPath,DbType.String);
            sp.Command.AddParameter("ApplicationVirtualPath",ApplicationVirtualPath,DbType.String);
            sp.Command.AddParameter("MachineName",MachineName,DbType.String);
            sp.Command.AddParameter("RequestUrl",RequestUrl,DbType.String);
            sp.Command.AddParameter("ExceptionType",ExceptionType,DbType.String);
            sp.Command.AddParameter("Details",Details,DbType.String);
            return sp;
        }
        public StoredProcedure CheckDeleteEvent(int EventID){
            StoredProcedure sp=new StoredProcedure("CheckDeleteEvent",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEvent(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEvent",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventBlogPostsBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventBlogPostsBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventCheckinsBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventCheckinsBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventData(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventData",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventImageMetaDataBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventImageMetaDataBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventImagesBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventImagesBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventTweetsBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventTweetsBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventTweetsBatch2(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventTweetsBatch2",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventURLsBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventURLsBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteTweet(int ID){
            StoredProcedure sp=new StoredProcedure("DeleteTweet",this.Provider);
            sp.Command.AddParameter("ID",ID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteUser(string UserId){
            StoredProcedure sp=new StoredProcedure("DeleteUser",this.Provider);
            sp.Command.AddParameter("UserId",UserId,DbType.String);
            return sp;
        }
        public StoredProcedure GetCurrentActiveUsers(){
            StoredProcedure sp=new StoredProcedure("GetCurrentActiveUsers",this.Provider);
            return sp;
        }
        public StoredProcedure GetEventGrowthStats(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetEventGrowthStats",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetHomePageActivity(){
            StoredProcedure sp=new StoredProcedure("GetHomePageActivity",this.Provider);
            return sp;
        }
        public StoredProcedure GetLastTweetIDByEventID(int EventID){
            StoredProcedure sp=new StoredProcedure("GetLastTweetIDByEventID",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure GetNumberOfImageRecordsByURL(string TheURL){
            StoredProcedure sp=new StoredProcedure("GetNumberOfImageRecordsByURL",this.Provider);
            sp.Command.AddParameter("TheURL",TheURL,DbType.AnsiString);
            return sp;
        }
        public StoredProcedure GetNumberOfNewUsersPerDay(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetNumberOfNewUsersPerDay",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetNumberOfRecordsByTwitterID(long TwitterID,long EventID){
            StoredProcedure sp=new StoredProcedure("GetNumberOfRecordsByTwitterID",this.Provider);
            sp.Command.AddParameter("TwitterID",TwitterID,DbType.Int64);
            sp.Command.AddParameter("EventID",EventID,DbType.Int64);
            return sp;
        }
        public StoredProcedure GetPhotosFromMemoryBox(int MemBoxId,int skip,int take){
            StoredProcedure sp=new StoredProcedure("GetPhotosFromMemoryBox",this.Provider);
            sp.Command.AddParameter("MemBoxId",MemBoxId,DbType.Int32);
            sp.Command.AddParameter("skip",skip,DbType.Int32);
            sp.Command.AddParameter("take",take,DbType.Int32);
            return sp;
        }
        public StoredProcedure GetRandomImagesByEventID(int EventID,int RecordsToReturn){
            StoredProcedure sp=new StoredProcedure("GetRandomImagesByEventID",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("RecordsToReturn",RecordsToReturn,DbType.Int32);
            return sp;
        }
        public StoredProcedure GetTop10TweetersByEventID(int EventID,DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetTop10TweetersByEventID",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetTopEventByUserActivity(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetTopEventByUserActivity",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetTopPhotosByEventID(int EventID,int ItemToReturn,DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetTopPhotosByEventID",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("ItemToReturn",ItemToReturn,DbType.Int32);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetTopURLsByEventID(int EventID,int ItemToReturn,DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetTopURLsByEventID",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("ItemToReturn",ItemToReturn,DbType.Int32);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetTrendingEventsByActivity(){
            StoredProcedure sp=new StoredProcedure("GetTrendingEventsByActivity",this.Provider);
            return sp;
        }
        public StoredProcedure GetTweetsFromMemoryBox(int MemBoxId,int skip,int take){
            StoredProcedure sp=new StoredProcedure("GetTweetsFromMemoryBox",this.Provider);
            sp.Command.AddParameter("MemBoxId",MemBoxId,DbType.Int32);
            sp.Command.AddParameter("skip",skip,DbType.Int32);
            sp.Command.AddParameter("take",take,DbType.Int32);
            return sp;
        }
        public StoredProcedure GetTweetsPerHourStats(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetTweetsPerHourStats",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetTwitterSearchesPerHour(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetTwitterSearchesPerHour",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetUserDashboardActivity(string UserID){
            StoredProcedure sp=new StoredProcedure("GetUserDashboardActivity",this.Provider);
            sp.Command.AddParameter("UserID",UserID,DbType.AnsiString);
            return sp;
        }
        public StoredProcedure GetUserGrowthStats(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetUserGrowthStats",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetUsersEventActivity(string UserID){
            StoredProcedure sp=new StoredProcedure("GetUsersEventActivity",this.Provider);
            sp.Command.AddParameter("UserID",UserID,DbType.AnsiString);
            return sp;
        }
        public StoredProcedure GetWeightedKeyWords(int EventID){
            StoredProcedure sp=new StoredProcedure("GetWeightedKeyWords",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure SearchEvents(string SearchTerm){
            StoredProcedure sp=new StoredProcedure("SearchEvents",this.Provider);
            sp.Command.AddParameter("SearchTerm",SearchTerm,DbType.String);
            return sp;
        }
        public StoredProcedure SearchInEvent(int EventID,string SearchTerm,DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("SearchInEvent",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("SearchTerm",SearchTerm,DbType.String);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure TweetsAndImageByEventIDPaged(int EventID,int skip,int take){
            StoredProcedure sp=new StoredProcedure("TweetsAndImageByEventIDPaged",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("skip",skip,DbType.Int32);
            sp.Command.AddParameter("take",take,DbType.Int32);
            return sp;
        }
	
	}
	
}
 