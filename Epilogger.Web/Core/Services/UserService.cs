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

        public int DeleteUser(Guid userId) {
            return base.GetRepository<User>().Delete(userId);
        }

        public UserFollowsEvent SaveUserFollowsEvent(UserFollowsEvent entity)
        {
            var thereturn = GetRepository<UserFollowsEvent>().Add(entity);
            entity.ID = int.Parse(thereturn.ToString());
            return entity;
        }

        public UserRatesEvent SaveUserRatesEvent(UserRatesEvent entity)
        {
            var theresult = GetRepository<UserRatesEvent>().Add(entity);
            entity.ID = int.Parse(theresult.ToString());
            return entity;
        }

        public List<UserRatesEvent> GetUserEventRatings(Guid id)
        {
            return db.UserRatesEvents.Where(u => u.UserID == id).ToList();            
        }


        public User GetUserByID(Guid id) {
            return GetData().FirstOrDefault(u => u.ID == id);
        }

        public User GetUserByUsername(string username) {
            //This was case-sensative for usernames - CB
            return GetData().FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.CurrentCultureIgnoreCase));
        }

        public User GetUserByEmail(string emailAddress) {
            return GetData().FirstOrDefault(u => u.EmailAddress == emailAddress);
        }

        public User GetUserByResetHash(Guid passwordResetHash) {
            return GetData().FirstOrDefault(u => u.ForgotPasswordHash == passwordResetHash);
        }

        public User ValidateLogin(string username, string password) {
            return GetData().FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public void DeleteEventSubscription(Guid userId, int eventId) {
            UserFollowsEvent followsEvent = db.UserFollowsEvents.FirstOrDefault(ufe => ufe.EventID == eventId && ufe.UserID == userId);
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

        public List<Epilogger.Data.Event> GetUserSubscribedAndCreatedEvents(Guid userID, int? limit)
        {
            List<Event> usersSubscribedAndCreatedEvents = new List<Event>();
            foreach (var item in base.db.Events.Where(e=>e.UserID == userID).OrderByDescending(d=>d.CreatedDateTime)) {
                usersSubscribedAndCreatedEvents.Add(item);
            }

            foreach (var item in base.db.UserFollowsEvents.Where(ufe => ufe.UserID == userID).OrderByDescending(d=>d.Timestamp)) {
                usersSubscribedAndCreatedEvents.Add(item.Events.FirstOrDefault());
            }

            if (limit.HasValue) {
                return usersSubscribedAndCreatedEvents.Take(limit.Value).ToList();
            }

            return usersSubscribedAndCreatedEvents;

            //StoredProcedure sp = db.GetUserDashboardActivity(userID.ToString());
            //List<DashboardActivityModel> DAM = new List<DashboardActivityModel>();
            //if (limit.HasValue) {
            //    DAM = sp.ExecuteTypedList<DashboardActivityModel>().Where(d => d.ActivityType == ActivityType.FOLLOW_EVENT || d.ActivityType == ActivityType.EVENT_CREATION).OrderByDescending(d => d.Date).Take(limit.Value).ToList();
            //} else {
            //    DAM = sp.ExecuteTypedList<DashboardActivityModel>().Where(d => d.ActivityType == ActivityType.FOLLOW_EVENT || d.ActivityType == ActivityType.EVENT_CREATION).OrderByDescending(d => d.Date).ToList();
            //}
            
            //if (limit.HasValue) {
            //    DAM = DAM.Take(limit.Value).ToList();
            //}
            //List<Epilogger.Data.Event> MyEvents = new List<Epilogger.Data.Event>();
            //Epilogger.Data.Event MyEvent;
            //EventService ES = new EventService();
            //foreach (DashboardActivityModel item in DAM)
            //{
            //    MyEvent = ES.FindByID(item.EventID);
            //    MyEvents.Add(MyEvent);
            //}

            //return MyEvents.OrderByDescending(e => e.StartDateTime).ToList();
        }


        public IEnumerable<Event> GetUserSubscribedAndCreatedEvents(Guid userID, int page, int count)
        {
            var usersSubscribedAndCreatedEvents = base.db.Events.Where(e => e.UserID == userID).OrderByDescending(d => d.CreatedDateTime).ToList();
            usersSubscribedAndCreatedEvents.AddRange(base.db.UserFollowsEvents.Where(ufe => ufe.UserID == userID).OrderByDescending(d => d.Timestamp).Select(item => item.Events.FirstOrDefault()));

            return usersSubscribedAndCreatedEvents.Skip(page * count).Take(count);
        }


        public bool IsBetaUser(string emailAddress) {
            return base.db.BetaSignups.Where(b => b.EmailAddress == emailAddress).Count() > 0;
        }

        public object SaveBetaSignup(BetaSignup entity) {
            return base.GetRepository<BetaSignup>().Add(entity);
        }

        public int GetActiveUserCount()
        {
            return base.db.Users.Count(u => u.IsActive);
        }

        public bool IsUsernameAvailable(string username)
        {
            return db.Users.Count(e => e.Username==username) == 0;
        }

        public bool IsEmailAddressAvailable(string emailaddress)
        {
            return db.Users.Count(e => e.EmailAddress == emailaddress) == 0;
        }

    }
}