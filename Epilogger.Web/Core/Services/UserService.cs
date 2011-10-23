using System;
using System.Linq;

using Epilogger.Data;
using System.Collections.Generic;
using Epilogger.Web.Models;
using SubSonic.Schema;

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

        public object SaveUserRatesEvent(UserRatesEvent entity)
        {
            return base.GetRepository<UserRatesEvent>().Add(entity);
        }

        public List<UserRatesEvent> GetUserEventRatings(Guid id)
        {
            return db.UserRatesEvents.Where(u => u.UserID == id).ToList();            
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

        public IEnumerable<DashboardActivityModel> GetUserDashboardActivity(Guid userID) {
            StoredProcedure sp = db.GetUserDashboardActivity(userID.ToString());
            return sp.ExecuteTypedList<DashboardActivityModel>();
        }
        public IEnumerable<DashboardActivityModel> GetUsersEventActivity(Guid userID) {
            StoredProcedure sp = db.GetUsersEventActivity(userID.ToString());
            return sp.ExecuteTypedList<DashboardActivityModel>();
        }
        public List<Epilogger.Data.Event> GetUserSubscribedAndCreatedEvents(Guid userID)
        {
            StoredProcedure sp = db.GetUserDashboardActivity(userID.ToString());
            List<DashboardActivityModel> DAM = sp.ExecuteTypedList<DashboardActivityModel>();

            List<Epilogger.Data.Event> MyEvents = new List<Epilogger.Data.Event>();
            Epilogger.Data.Event MyEvent;
            EventService ES = new EventService();
            foreach (DashboardActivityModel item in DAM.Where(d => d.ActivityType == ActivityType.FOLLOW_EVENT || d.ActivityType == ActivityType.EVENT_CREATION))
            {
                MyEvent = ES.FindByID(item.EventID);
                MyEvents.Add(MyEvent);
            }

            return MyEvents.OrderByDescending(e => e.StartDateTime).ToList();
        }

        public bool IsBetaUser(string emailAddress) {
            return base.db.BetaSignups.Where(b => b.EmailAddress == emailAddress).Count() > 0;
        }

        public object SaveBetaSignup(BetaSignup entity) {
            return base.GetRepository<BetaSignup>().Add(entity);
        }
    }
}