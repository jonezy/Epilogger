using System.Collections.Generic;

using System;
using System.Linq;
using Epilogger.Data;
using System.Data;
using SubSonic.Schema;

namespace Epilogger.Web {
    public class EventService : ServiceBase<Event> {
        protected override string CacheKey {
            get { return "Epilogger.Web.Events"; }
        }

        public List<Event> AllEvents() {
            return base.GetData();
        }

        public List<Event> UpcomingEvents()
        {
            return GetData(e => e.StartDateTime > DateTime.UtcNow);
        }

        public List<Event> PastEvents()
        {
            return GetData(e => e.EndDateTime < DateTime.UtcNow).ToList();
        }

        public List<Event> TodaysEvents()
        {
            return GetData(IsSameDate<Event>(e => e.StartDateTime, DateTime.UtcNow)).ToList();
        }

        public List<Event> GoingOnNowEvents()
        {
            IEnumerable<Event> neverEndingEvents = from e in GetData()
                                                   where e.EndDateTime == null
                                                   select e;

            IEnumerable<Event> happeningNow = from e in GetData()
                                         where (e.StartDateTime <= DateTime.UtcNow && (e.EndDateTime != null && e.EndDateTime >= DateTime.UtcNow))
                                         select e;

            return neverEndingEvents.Concat(happeningNow).OrderBy(e=>e.StartDateTime).ToList();
            //return GetData(e => e.StartDateTime <= DateTime.UtcNow && e.EndDateTime >= DateTime.UtcNow).ToList();
        }

      

        public Event GetRandomEvent()
        {
            int lowerbound = 1;
            int upperbound = this.GetHighestEventID();

            System.Random r = new System.Random();
            int RandomNumber = r.Next(lowerbound, upperbound);
            
            do
            {
                Event EE = this.FindByID(RandomNumber);
                if (EE == null)
                {
                    RandomNumber = Helpers.RandomInt(r, lowerbound, upperbound);
                }
                else
                {
                    return EE;
                }
            } while (true);

        }

        public int GetHighestEventID()
        {
            return db.Events.OrderByDescending(e => e.ID).First().ID;
        }



        public IEnumerable<Event> GetHottestEvents(int ItemsToReturn)
        {
            return db.Events.Where(e => e.StartDateTime > DateTime.UtcNow.AddMonths(-1) && e.StartDateTime < DateTime.UtcNow).OrderByDescending(e => e.Tweets.Where(f => f.EventID == e.ID).Count() + (e.Images.Where(f => f.EventID == e.ID).Count() * 4)).Take(ItemsToReturn);
        }


        public Event FindByID(int EventID)
        {
            return GetData().Where(e => e.ID == EventID).FirstOrDefault();
        }

        public List<Event> GetEventsByCategoryID(int CategoryID)
        {
            return GetData(e => e.CategoryID == CategoryID);
        }

        public List<Event> GetEventsByCategorySlug(string CategorySlug)
        {
            CategoryService CS = new CategoryService();
            Epilogger.Data.EventCategory TheCat = CS.GetCategoryBySlug(CategorySlug);
            return GetData(e => e.CategoryID == TheCat.ID);
        }

        public List<Event> FindByUserID(Guid userID) {
            return GetData().Where(e => e.UserID == userID).OrderByDescending(e=>e.StartDateTime).ToList();
        }


        public List<UserRatesEvent> FindEventRatingsByID(int id, DateTime F, DateTime T)
        {
            return db.UserRatesEvents.Where(r => r.EventID == id && r.RatingDateTime >= F && r.RatingDateTime <= T).ToList();
        }


        public object Save(Event entity)
        {
            if (entity.ID > 0)
                return base.GetRepository<Event>().Update(entity);

            return base.GetRepository<Event>().Add(entity);
        }

        public List<Epilogger.Web.Models.SearchInEventModel> SearchInEvent(int EventID, string SearchTerm, DateTime FromDateTime, DateTime ToDateTime)
        {
            StoredProcedure sproc = db.SearchInEvent(EventID, SearchTerm, FromDateTime, ToDateTime);
            DataSet SearchResults = sproc.ExecuteDataSet();

            List <Epilogger.Web.Models.SearchInEventModel>  ResultsList = new List<Epilogger.Web.Models.SearchInEventModel>();
            foreach (DataRow row in SearchResults.Tables[0].Rows)
            {
                Epilogger.Web.Models.SearchInEventModel SearchItem = new Epilogger.Web.Models.SearchInEventModel();
                SearchItem.Tweet = new Epilogger.Data.Tweet();

                SearchItem.Tweet.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                SearchItem.Tweet.EventID = int.Parse(row["EventID"].ToString());
                SearchItem.Tweet.FromUserScreenName = row["FromUserScreenName"].ToString();
                SearchItem.Tweet.ID = int.Parse(row["ID"].ToString());
                SearchItem.Tweet.IsoLanguageCode = row["IsoLanguageCode"].ToString();
                SearchItem.Tweet.Location = row["Location"].ToString();
                SearchItem.Tweet.ProfileImageURL = row["ProfileImageURL"].ToString();
                SearchItem.Tweet.RawSource = row["RawSource"].ToString();
                SearchItem.Tweet.SinceID = int.Parse(row["SinceID"].ToString());
                SearchItem.Tweet.Source = row["Source"].ToString();
                SearchItem.Tweet.Text = row["Text"].ToString();
                SearchItem.Tweet.TextAsHTML = row["TextAsHTML"].ToString();
                SearchItem.Tweet.ToUserScreenName = row["ToUserScreenName"].ToString();
                SearchItem.Tweet.TwitterID = long.Parse(row["TwitterID"].ToString());

                if (row["Fullsize"].ToString() != string.Empty)
                {
                    SearchItem.Image = new Epilogger.Data.Image();
                    SearchItem.Image.AzureContainerPrefix = row["AzureContainerPrefix"].ToString();
                    SearchItem.Image.DateTime = DateTime.Parse(row["DateTime"].ToString());
                    SearchItem.Image.EventID = int.Parse(row["EventID"].ToString());
                    SearchItem.Image.Fullsize = row["Fullsize"].ToString();
                    SearchItem.Image.ID = int.Parse(row["ID2"].ToString());
                    SearchItem.Image.OriginalImageLink = row["OriginalImageLink"].ToString();
                    SearchItem.Image.Thumb = row["Thumb"].ToString();
                }

                ResultsList.Add(SearchItem);
            }

            return ResultsList;
        }


    }
}