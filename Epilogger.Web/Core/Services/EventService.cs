using System.Collections.Generic;

using System;
using System.Linq;
using Epilogger.Data;
using System.Data;
using SubSonic.Schema;
using Epilogger.Web.Models;

namespace Epilogger.Web {
    public class EventService : ServiceBase<Event> {
        protected override string CacheKey {
            get { return "Epilogger.Web.Events"; }
        }

        public List<Event> AllEvents() {
            return base.GetData();
        }

        public List<Event> AllEventsDescPaged(int currentPage, int recordsPerPage)
        {
            var es = db.Events.OrderByDescending(t => t.CreatedDateTime);
            var recordsToSkip = currentPage == 1 ? 1 : currentPage * recordsPerPage;
            return es.Skip(recordsToSkip).Take(recordsPerPage).ToList();
        } 


        public List<Event> Get50Events()
        {
            return db.Events.OrderByDescending(t => t.ID).Take(50).ToList();
        }

        public Event RandomUpcomingEvent() {
            List<Event> events = base.db.Events.Where(e => e.StartDateTime > DateTime.UtcNow).ToList();
            Random rnd = new Random();
            int i = rnd.Next(events.Count());

            return events[i];

                
            //int lowerbound = 1;
            //int upperbound = this.GetHighestEventID();

            //System.Random r = new System.Random();
            //int RandomNumber = r.Next(lowerbound, upperbound);
            
            //do
            //{
            //    Event EE = this.FindByID(RandomNumber);
            //    if (EE == null || EE.StartDateTime < DateTime.UtcNow)
            //    {
            //        RandomNumber = Helpers.RandomInt(r, lowerbound, upperbound);
            //    }
            //    else
            //    {
            //        return EE;
            //    }
            //} while (true);
        }

        /*Featured Events*/
        public IEnumerable<Event> GetFeaturedEvents()
        {
            return db.Events.Where(e => e.IsFeatured == true && DateTime.UtcNow >= e.FeaturedStartDateTime && DateTime.UtcNow <= e.FeaturedEndDateTime);
        }


        /* Upcoming events */
        public List<Event> UpcomingEvents()
        {
            return db.Events.Where(e => e.StartDateTime > DateTime.UtcNow).OrderByDescending(c => c.StartDateTime).ToList();
        }
        public int UpcomingEventCount()
        {
            return db.Events.Where(e => e.StartDateTime > DateTime.UtcNow).Count();
        }
        public List<Event> UpcomingEventsPaged(int currentPage, int recordsPerPage)
        {
            var es = db.Events.Where(e => e.StartDateTime > DateTime.UtcNow);
            return es.Skip(currentPage * recordsPerPage).Take(recordsPerPage).OrderBy(c => c.StartDateTime).ToList();
        }


        /* Past events */
        public List<Event> PastEvents()
        {
            return GetData(e => e.EndDateTime < DateTime.UtcNow).ToList();
        }
        public int PastEventCount()
        {
            return db.Events.Count(e => e.EndDateTime < DateTime.UtcNow);
        }
        public List<Event> PastEventsPaged(int currentPage, int recordsPerPage)
        {
            var es = db.Events.Where(e => e.EndDateTime < DateTime.UtcNow);
            return es.Skip(currentPage * recordsPerPage).Take(recordsPerPage).OrderByDescending(c => c.StartDateTime).ToList();
        }

        /* Now events */
        public List<Event> GoingOnNowEvents()
        {
            IEnumerable<Event> neverEndingEvents = from e in GetData()
                                                   where e.EndDateTime == null
                                                   select e;

            IEnumerable<Event> happeningNow = from e in GetData()
                                              where (e.StartDateTime <= DateTime.UtcNow && (e.EndDateTime != null && e.EndDateTime >= DateTime.UtcNow))
                                              select e;

            return neverEndingEvents.Concat(happeningNow).OrderBy(e => e.StartDateTime).ToList();
        }
        public int GoingOnNowEventsCount()
        {
            IEnumerable<Event> neverEndingEvents = from e in GetData()
                                                   where e.EndDateTime == null
                                                   select e;

            IEnumerable<Event> happeningNow = from e in GetData()
                                              where (e.StartDateTime <= DateTime.UtcNow && (e.EndDateTime != null && e.EndDateTime >= DateTime.UtcNow) && e.IsActive)
                                              select e;

            return neverEndingEvents.Concat(happeningNow).Count();
        }

        public int GetNumberOfCollectingEvents()
        {

            IEnumerable<Event> collecting = from e in GetData()
                                            where (e.CollectionStartDateTime <= DateTime.UtcNow && e.CollectionEndDateTime >= DateTime.UtcNow && e.IsActive)
                                              select e;

            return collecting.Count();
        }

        public List<Event> GoingOnNowEventsPaged(int currentPage, int recordsPerPage)
        {
            IEnumerable<Event> neverEndingEvents = from e in GetData()
                                                   where e.StartDateTime <= DateTime.UtcNow && e.EndDateTime == null
                                                   select e;

            IEnumerable<Event> happeningNow = from e in GetData()
                                              where (e.StartDateTime <= DateTime.UtcNow && (e.EndDateTime != null && e.EndDateTime >= DateTime.UtcNow))
                                              select e;

            neverEndingEvents = neverEndingEvents.Concat(happeningNow).OrderByDescending(e => e.StartDateTime);
            var recordsToSkip = currentPage == 1 ? 1 : currentPage * recordsPerPage;
            return neverEndingEvents.Skip(recordsToSkip).Take(recordsPerPage).ToList();
        }






        public List<Event> TodaysEvents()
        {
            return GetData(IsSameDate<Event>(e => e.StartDateTime, DateTime.UtcNow)).ToList();
        }

        public IEnumerable<HomepageActivityModel> GetHomepageActivity() {
            StoredProcedure sp = db.GetHomePageActivity();
            return sp.ExecuteTypedList<HomepageActivityModel>();
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



        public IEnumerable<Event> GetHottestEvents(int itemsToReturn)
        {
            return db.Events.Where(e => e.StartDateTime > DateTime.UtcNow.AddDays(-14) && e.StartDateTime < DateTime.UtcNow).OrderByDescending(e => e.Tweets.Where(f => f.EventID == e.ID).Count() + (e.Images.Where(f => f.EventID == e.ID).Count() * 4)).Take(itemsToReturn);
        }


        public Event FindByID(int eventID)
        {
            return GetData().FirstOrDefault(e => e.ID == eventID);
        }

        public Event FindBySlug(String slug)
        {
            //A quick little hack to let OLD URLs work
            int eventID;
            if (int.TryParse(slug, out eventID))
            {
                return GetData().FirstOrDefault(e => e.ID == eventID);
            }

            return GetData().FirstOrDefault(e => e.EventSlug == slug);
        }


        public List<Event> GetEventsByCategoryID(int categoryID)
        {
            return GetData(e => e.CategoryID == categoryID);
        }


        public List<Event> GetEventsByCategoryIDDescPaged(int categoryID, int currentPage, int recordsPerPage)
        {
            var ec = db.Events.Where(e => e.CategoryID == categoryID).OrderByDescending(e => e.StartDateTime);
            return ec.Skip(currentPage * recordsPerPage).Take(recordsPerPage).ToList();
        }


        public List<Event> GetEventsByCategorySlug(string categorySlug)
        {
            CategoryService CS = new CategoryService();
            Epilogger.Data.EventCategory TheCat = CS.GetCategoryBySlug(categorySlug);
            return GetData(e => e.CategoryID == TheCat.ID);
        }

        public List<Event> FindByUserID(Guid userID) {
            return GetData().Where(e => e.UserID == userID).OrderByDescending(e=>e.StartDateTime).ToList();
        }

        public List<Event> FindByUserIDPaged(Guid userID, int currentPage, int recordsPerPage)
        {
            var es = db.Events.Where(e => e.UserID == userID);
            return es.Skip(currentPage * recordsPerPage).Take(recordsPerPage).OrderByDescending(c => c.StartDateTime).ToList();
        }
        public int FindCountByUserID(Guid userID)
        {
            return db.Events.Where(e => e.UserID == userID).Count();
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

        public List<SearchInEventModel> SearchInEvent(int eventID, string searchTerm, DateTime fromDateTime, DateTime toDateTime)
        {
            var sproc = db.SearchInEvent(eventID, searchTerm, fromDateTime, toDateTime);
            var searchResults = sproc.ExecuteDataSet();

            var  resultsList = new List<SearchInEventModel>();
            foreach (DataRow row in searchResults.Tables[0].Rows)
            {
                var searchItem = new SearchInEventModel
                                     {
                                         Tweet =
                                             new Tweet
                                                 {
                                                     CreatedDate = DateTime.Parse(row["CreatedDate"].ToString()),
                                                     EventID = int.Parse(row["EventID"].ToString()),
                                                     FromUserScreenName = row["FromUserScreenName"].ToString(),
                                                     ID = int.Parse(row["ID"].ToString()),
                                                     IsoLanguageCode = row["IsoLanguageCode"].ToString(),
                                                     Location = row["Location"].ToString(),
                                                     ProfileImageURL = row["ProfileImageURL"].ToString(),
                                                     RawSource = row["RawSource"].ToString(),
                                                     SinceID = int.Parse(row["SinceID"].ToString()),
                                                     Source = row["Source"].ToString(),
                                                     Text = row["Text"].ToString(),
                                                     TextAsHTML = row["TextAsHTML"].ToString(),
                                                     ToUserScreenName = row["ToUserScreenName"].ToString(),
                                                     TwitterID = long.Parse(row["TwitterID"].ToString())
                                                 }
                                     };


                if (row["Fullsize"].ToString() != string.Empty)
                {
                    searchItem.Image = new Image
                                           {
                                               AzureContainerPrefix = row["AzureContainerPrefix"].ToString(),
                                               DateTime = DateTime.Parse(row["DateTime1"].ToString()),
                                               EventID = int.Parse(row["EventID"].ToString()),
                                               Fullsize = row["Fullsize"].ToString(),
                                               ID = int.Parse(row["ID2"].ToString()),
                                               OriginalImageLink = row["OriginalImageLink"].ToString(),
                                               Thumb = row["Thumb"].ToString()
                                           };
                }

                resultsList.Add(searchItem);
            }

            return resultsList;
        }


        public IEnumerable<Event> GetEventsBySearchTerm(string SearchTerm)
        {

            var EVs = from e in db.Events
                      where e.Name.Contains(SearchTerm) ||
                            e.SearchTerms.Contains(SearchTerm) || e.SubTitle.Contains(SearchTerm) || e.Description.Contains(SearchTerm)
                      orderby e.CreatedDateTime
                      select e;

            return EVs;

        }


        public int Count() {
            return base.db.Events.Count();
        }



        public List<Event> GetTrendingEvents()
        {
            return db.GetTrendingEventsByActivity().ExecuteTypedList<Event>();
        }


    }
}