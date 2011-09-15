using System;
using System.Linq;

using Epilogger.Data;

namespace Epilogger.Web {
    public class UserService : ServiceBase<User>{

        protected override string CacheKey {
            get { return "Epilogger.Web.Users"; }
        }

        public object Save(User entity) {
            //If GUID, then update.
            if (entity.ID != Guid.Empty)
                return base.GetRepository<User>().Update(entity);

            //No Guid, create a new one and insert
            entity.ID = Guid.NewGuid();
            return base.GetRepository<User>().Add(entity);
        }

        public object SaveUserFollowsEvent(UserFollowsEvent entity) {
            return base.GetRepository<UserFollowsEvent>().Add(entity);
        }

        public User GetUserByID(Guid id) {
            return GetData().Where(u => u.ID == id).FirstOrDefault();
        }

        public User GetUserByUsername(string username) {
            //This was case-sensative for usernames - CB
            return GetData().Where(u => string.Equals(u.Username, username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }

        public User GetUserByEmail(string emailAddress) {
            return GetData().Where(u => u.EmailAddress == emailAddress).FirstOrDefault();
        }

        public User GetUserByResetHash(Guid passwordResetHash) {
            return GetData().Where(u => u.ForgotPasswordHash == passwordResetHash).FirstOrDefault();
        }

        public User ValidateLogin(string username, string password) {
            return GetData().Where(u => u.Username == username && u.Password == password).FirstOrDefault();
        }

        public void DeleteEventSubscription(Guid userId, int eventId) {
            UserFollowsEvent followsEvent = base.db.UserFollowsEvents.Where(ufe => ufe.EventID == eventId && ufe.UserID == userId).FirstOrDefault();
            
            base.GetRepository<UserFollowsEvent>().Delete(followsEvent);
        }
    }
}