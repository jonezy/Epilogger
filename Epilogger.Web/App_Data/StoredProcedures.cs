


  
using System;
using SubSonic;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Epilogger.Data{
	public partial class EpiloggerDB{

        public StoredProcedure aspnet_Membership_CreateUser(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_CreateUser",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_FindUsersByEmail(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_FindUsersByEmail",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_FindUsersByName(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_FindUsersByName",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetAllUsers(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetAllUsers",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetNumberOfUsersOnline(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetNumberOfUsersOnline",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetPassword(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetPassword",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetPasswordWithFormat(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetPasswordWithFormat",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetUserByEmail(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetUserByEmail",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetUserByName(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetUserByName",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_GetUserByUserId(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_GetUserByUserId",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_ResetPassword(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_ResetPassword",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_SetPassword(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_SetPassword",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_UnlockUser(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_UnlockUser",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_UpdateUser(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_UpdateUser",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Membership_UpdateUserInfo(){
            StoredProcedure sp=new StoredProcedure("aspnet_Membership_UpdateUserInfo",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Paths_CreatePath(){
            StoredProcedure sp=new StoredProcedure("aspnet_Paths_CreatePath",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Personalization_GetApplicationId(){
            StoredProcedure sp=new StoredProcedure("aspnet_Personalization_GetApplicationId",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAdministration_DeleteAllState(){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAdministration_DeleteAllState",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAdministration_FindState(){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAdministration_FindState",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAdministration_GetCountOfState(){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAdministration_GetCountOfState",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAdministration_ResetSharedState(){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAdministration_ResetSharedState",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAdministration_ResetUserState(){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAdministration_ResetUserState",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAllUsers_GetPageSettings(){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAllUsers_GetPageSettings",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAllUsers_ResetPageSettings(){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAllUsers_ResetPageSettings",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationAllUsers_SetPageSettings(){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationAllUsers_SetPageSettings",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationPerUser_GetPageSettings(){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationPerUser_GetPageSettings",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationPerUser_ResetPageSettings(){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationPerUser_ResetPageSettings",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_PersonalizationPerUser_SetPageSettings(){
            StoredProcedure sp=new StoredProcedure("aspnet_PersonalizationPerUser_SetPageSettings",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Profile_DeleteInactiveProfiles(){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_DeleteInactiveProfiles",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Profile_DeleteProfiles(){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_DeleteProfiles",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Profile_GetNumberOfInactiveProfiles(){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_GetNumberOfInactiveProfiles",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Profile_GetProfiles(){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_GetProfiles",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Profile_GetProperties(){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_GetProperties",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Profile_SetProperties(){
            StoredProcedure sp=new StoredProcedure("aspnet_Profile_SetProperties",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_RegisterSchemaVersion(){
            StoredProcedure sp=new StoredProcedure("aspnet_RegisterSchemaVersion",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Roles_CreateRole(){
            StoredProcedure sp=new StoredProcedure("aspnet_Roles_CreateRole",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Roles_DeleteRole(){
            StoredProcedure sp=new StoredProcedure("aspnet_Roles_DeleteRole",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Roles_GetAllRoles(){
            StoredProcedure sp=new StoredProcedure("aspnet_Roles_GetAllRoles",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Roles_RoleExists(){
            StoredProcedure sp=new StoredProcedure("aspnet_Roles_RoleExists",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Setup_RemoveAllRoleMembers(){
            StoredProcedure sp=new StoredProcedure("aspnet_Setup_RemoveAllRoleMembers",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Setup_RestorePermissions(){
            StoredProcedure sp=new StoredProcedure("aspnet_Setup_RestorePermissions",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_UnRegisterSchemaVersion(){
            StoredProcedure sp=new StoredProcedure("aspnet_UnRegisterSchemaVersion",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Users_CreateUser(){
            StoredProcedure sp=new StoredProcedure("aspnet_Users_CreateUser",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_Users_DeleteUser(){
            StoredProcedure sp=new StoredProcedure("aspnet_Users_DeleteUser",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_AddUsersToRoles(){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_AddUsersToRoles",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_FindUsersInRole(){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_FindUsersInRole",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_GetRolesForUser(){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_GetRolesForUser",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_GetUsersInRoles(){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_GetUsersInRoles",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_IsUserInRole(){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_IsUserInRole",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_UsersInRoles_RemoveUsersFromRoles(){
            StoredProcedure sp=new StoredProcedure("aspnet_UsersInRoles_RemoveUsersFromRoles",this.Provider);
            return sp;
        }
        public StoredProcedure aspnet_WebEvent_LogEvent(){
            StoredProcedure sp=new StoredProcedure("aspnet_WebEvent_LogEvent",this.Provider);
            return sp;
        }
        public StoredProcedure CheckDeleteEvent(){
            StoredProcedure sp=new StoredProcedure("CheckDeleteEvent",this.Provider);
            return sp;
        }
        public StoredProcedure DeleteEvent(){
            StoredProcedure sp=new StoredProcedure("DeleteEvent",this.Provider);
            return sp;
        }
        public StoredProcedure DeleteEventData(){
            StoredProcedure sp=new StoredProcedure("DeleteEventData",this.Provider);
            return sp;
        }
	
	}
	
}
 