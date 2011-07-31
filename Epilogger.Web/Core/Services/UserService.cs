using System.Linq;
using Epilogger.Data;
using System;

namespace Epilogger.Web {
    public class UserService : ServiceBase<User>{

        protected override string CacheKey {
            get { return "Epilogger.Web.Users"; }
        }

        public object Save(User entity) {
            if (entity.ID != Guid.Empty)
                return base.GetRepository<User>(db).Update(entity);

            return base.GetRepository<User>(db).Add(entity);
        }

        public User GetUserByID(Guid id) {
            return GetData().Where(u => u.ID == id).FirstOrDefault();
        }
        public User GetUserByUsername(string username) {
            return GetData().Where(u => u.Username == username).FirstOrDefault();
        }

        public User GetUserByEmail(string emailAddress) {
            return GetData().Where(u => u.EmailAddress == emailAddress).FirstOrDefault();
        }

        public User ValidateLogin(string username, string password) {
            return GetData().Where(u => u.Username == username && u.Password == password).FirstOrDefault();
        }
    }
}