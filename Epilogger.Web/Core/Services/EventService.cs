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
            return base.GetData().Where(e => e.IsPrivate == false) as List<Event>;
        }

        public List<Event> AllEventsDescPaged(int currentPage, int recordsPerPage)
        {
            var es = db.Events.Where(e => e.IsPrivate==false).OrderByDescending(t => t.CreatedDateTime);
            var recordsToSkip = currentPage == 1 ? 1 : currentPage * recordsPerPage;
            return es.Skip(recordsToSkip).Take(recordsPerPage).ToList();
        } 


        public List<Event> Get50Events()
        {
            return db.Events.Where(e => e.IsPrivate == false).OrderByDescending(t => t.ID).Take(50).ToList();
        }

        public Event RandomUpcomingEvent() {
            var events = base.db.Events.Where(e => e.StartDateTime > DateTime.UtcNow && e.IsPrivate==false).ToList();
            var rnd = new Random();
            var i = rnd.Next(events.Count());

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
        public Event GetFeaturedEvents()
        {
            var doIt = true;
            Event choseEvent = null;
            

            //If an event is explicitly defined
            var fEvents = db.Events.Where(e => e.IsFeatured == true && DateTime.UtcNow >= e.FeaturedStartDateTime && DateTime.UtcNow <= e.FeaturedEndDateTime && e.IsPrivate==false);
            if (fEvents.Any())
            {
                for (var i = 0; i < fEvents.Count(); i++)
                {
                    var e = GetRandomEvent(fEvents);
                    if (ContainsEnoughImages(e))
                        return e;
                }
            }

            //Find a short current event
            var f2Events = FindAllActiveEvents().Where(e => e.EndDateTime != null && ((DateTime)e.EndDateTime - e.StartDateTime).TotalHours <= 8 && e.StartDateTime < DateTime.UtcNow && e.EndDateTime > DateTime.UtcNow);
            var featuredEvents = f2Events as List<Event> ?? f2Events.ToList();
            if (featuredEvents.Any())
            {
                for (var i = 0; i < featuredEvents.Count(); i++)
                {
                    var e = GetRandomEvent(featuredEvents);
                    if (ContainsEnoughImages(e))
                        return e;
                }
            }

            //Ended in the last 12 hours
            f2Events = FindAllActiveEvents().Where(e => e.EndDateTime != null && ((DateTime.UtcNow - ((DateTime)e.EndDateTime)).TotalHours < 12 && (DateTime.UtcNow - ((DateTime)e.EndDateTime)).TotalHours >= 0));
            featuredEvents = f2Events as List<Event> ?? f2Events.ToList();
            if (featuredEvents.Any())
            {
                for (var i = 0; i < featuredEvents.Count(); i++)
                {
                    var e = GetRandomEvent(featuredEvents);
                    if (ContainsEnoughImages(e))
                        return e;
                }
            }

            //Check events starting soon
            f2Events = FindAllActiveEvents().Where(e => (e.StartDateTime - DateTime.UtcNow).TotalHours < 12 && (e.StartDateTime - DateTime.UtcNow).TotalHours >= 0);
            featuredEvents = f2Events as List<Event> ?? f2Events.ToList();
            if (featuredEvents.Any())
            {
                for (var i = 0; i < featuredEvents.Count(); i++)
                {
                    var e = GetRandomEvent(featuredEvents);
                    if (ContainsEnoughImages(e))
                        return e;
                }
            }

            //Started in the last 72 hours
            f2Events = FindAllActiveEvents().Where(e => (e.StartDateTime - DateTime.UtcNow).TotalHours < 72 && (e.StartDateTime - DateTime.UtcNow).TotalHours >= 0);
            featuredEvents = f2Events as List<Event> ?? f2Events.ToList();
            if (featuredEvents.Any())
            {
                for (var i = 0; i < featuredEvents.Count(); i++)
                {
                    var e = GetRandomEvent(featuredEvents);
                    if (ContainsEnoughImages(e))
                        return e;
                }
            }

            //Started in the week
            f2Events = FindAllActiveEvents().Where(e => (e.StartDateTime - DateTime.UtcNow).TotalHours < 168 && (e.StartDateTime - DateTime.UtcNow).TotalHours >= 0);
            featuredEvents = f2Events as List<Event> ?? f2Events.ToList();
            if (featuredEvents.Any())
                if (featuredEvents.Any())
                {
                    for (var i = 0; i < featuredEvents.Count(); i++)
                    {
                        var e = GetRandomEvent(featuredEvents);
                        if (ContainsEnoughImages(e))
                            return e;
                    }
                }

            //Started in the month
            f2Events = FindAllActiveEvents().Where(e => (e.StartDateTime - DateTime.UtcNow).TotalHours < 1080 && (e.StartDateTime - DateTime.UtcNow).TotalHours >= 0);
            featuredEvents = f2Events as List<Event> ?? f2Events.ToList();
            if (featuredEvents.Any())
            {
                //foreach (var e in featuredEvents)
                //{
                //    if (ContainsEnoughImages(e))
                //        return e;
                //}
                
                ////If nothing return a ramdom event
                //var ev = GetRandomEvent(featuredEvents);
                //return ev;



                for (var i = 0; i < featuredEvents.Count(); i++)
                {
                    var e = GetRandomEvent(featuredEvents);
                    if (ContainsEnoughImages(e))
                        return e;

                    if (i == featuredEvents.Count() - 1)
                        return e;
                }
            }

            
            return null;
        }

        private static bool ContainsEnoughImages(Event e)
        {
            return e.Images.Count() > 5;
        }

        /* Upcoming events */
        public List<Event> UpcomingEvents()
        {
            return db.Events.Where(e => e.StartDateTime > DateTime.UtcNow && e.IsPrivate==false).OrderByDescending(c => c.StartDateTime).ToList();
        }
        public int UpcomingEventCount()
        {
            return db.Events.Count(e => e.StartDateTime > DateTime.UtcNow && e.IsPrivate==false);
        }
        public List<Event> UpcomingEventsPaged(int currentPage, int recordsPerPage)
        {
            var es = db.Events.Where(e => e.StartDateTime > DateTime.UtcNow && e.IsPrivate==false);
            return es.Skip(currentPage * recordsPerPage).Take(recordsPerPage).OrderBy(c => c.StartDateTime).ToList();
        }


        /* Past events */
        public List<Event> PastEvents()
        {
            return GetData(e => e.EndDateTime < DateTime.UtcNow && e.IsPrivate==false).ToList();
        }
        public int PastEventCount()
        {
            return db.Events.Count(e => e.EndDateTime < DateTime.UtcNow && e.IsPrivate == false);
        }
        public List<Event> PastEventsPaged(int currentPage, int recordsPerPage)
        {
            var es = db.Events.Where(e => e.EndDateTime < DateTime.UtcNow && e.IsPrivate == false);
            return es.Skip(currentPage * recordsPerPage).Take(recordsPerPage).OrderByDescending(c => c.StartDateTime).ToList();
        }

        /* Now events */
        public List<Event> GoingOnNowEvents()
        {
            var neverEndingEvents = from e in GetData()
                                    where e.EndDateTime == null && e.IsPrivate == false
                                    select e;

            var happeningNow = from e in GetData()
                               where (e.StartDateTime <= DateTime.UtcNow && (e.EndDateTime != null && e.EndDateTime >= DateTime.UtcNow) && e.IsPrivate == false)
                                select e;

            return neverEndingEvents.Concat(happeningNow).OrderBy(e => e.StartDateTime).ToList();
        }
        public int GoingOnNowEventsCount()
        {
            IEnumerable<Event> neverEndingEvents = from e in GetData()
                                                   where e.EndDateTime == null && e.IsPrivate == false
                                                   select e;

            IEnumerable<Event> happeningNow = from e in GetData()
                                              where (e.StartDateTime <= DateTime.UtcNow && (e.EndDateTime != null && e.EndDateTime >= DateTime.UtcNow) && e.IsActive && e.IsPrivate == false)
                                              select e;

            return neverEndingEvents.Concat(happeningNow).Count();
        }

        public int GetNumberOfCollectingEvents()
        {

            IEnumerable<Event> collecting = from e in GetData()
                                            where (e.CollectionStartDateTime <= DateTime.UtcNow && e.CollectionEndDateTime >= DateTime.UtcNow && e.IsActive && e.IsPrivate == false)
                                            select e;

            return collecting.Count();
        }

        public List<Event> GoingOnNowEventsPaged(int currentPage, int recordsPerPage)
        {
            IEnumerable<Event> neverEndingEvents = from e in GetData()
                                                   where e.StartDateTime <= DateTime.UtcNow && e.EndDateTime == null && e.IsPrivate == false
                                                   select e;

            IEnumerable<Event> happeningNow = from e in GetData()
                                              where (e.StartDateTime <= DateTime.UtcNow && (e.EndDateTime != null && e.EndDateTime >= DateTime.UtcNow) && e.IsPrivate == false)
                                              select e;

            neverEndingEvents = neverEndingEvents.Concat(happeningNow).OrderByDescending(e => e.StartDateTime);
            var recordsToSkip = currentPage == 1 ? 1 : currentPage * recordsPerPage;
            return neverEndingEvents.Skip(recordsToSkip).Take(recordsPerPage).ToList();
        }






        public List<Event> TodaysEvents()
        {
            var d = GetData(IsSameDate<Event>(f => f.StartDateTime, DateTime.UtcNow));
            return d.Where(e => e.IsPrivate == false).ToList();
        }

        public IEnumerable<HomepageActivityModel> GetHomepageActivity() {
            StoredProcedure sp = db.GetHomePageActivity();
            return sp.ExecuteTypedList<HomepageActivityModel>();
        }
      

        public Event GetRandomEvent()
        {
            const int lowerbound = 1;
            var upperbound = this.GetHighestEventId();

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


        public Event GetRandomEvent(IEnumerable<Event> events)
        {
            const int lowerbound = 0;
            var enumerable = events as List<Event> ?? events.ToList();
            
            var upperbound = enumerable.Count() - 1;

            var r = new Random();
            var randomNumber = r.Next(lowerbound, upperbound);

            return enumerable.ToList()[randomNumber];

        }


        public int GetHighestEventId()
        {
            return db.Events.Where(e => e.IsPrivate==false).OrderByDescending(e => e.ID).First().ID;
        }



        public IEnumerable<Event> GetHottestEvents(int itemsToReturn)
        {
            return db.Events.Where(e => e.IsPrivate==false && e.StartDateTime > DateTime.UtcNow.AddDays(-14) && e.StartDateTime < DateTime.UtcNow).OrderByDescending(e => e.Tweets.Count(f => f.EventID == e.ID) + (e.Images.Where(f => f.EventID == e.ID).Count() * 4)).Take(itemsToReturn);
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
            return GetData(e => e.CategoryID == categoryID && e.IsPrivate==false);
        }


        public List<Event> GetEventsByCategoryIDDescPaged(int categoryID, int currentPage, int recordsPerPage)
        {
            var ec = db.Events.Where(e => e.CategoryID == categoryID && e.IsPrivate==false).OrderByDescending(e => e.StartDateTime);
            return ec.Skip(currentPage * recordsPerPage).Take(recordsPerPage).ToList();
        }


        public List<Event> GetEventsByCategorySlug(string categorySlug)
        {
            CategoryService CS = new CategoryService();
            Epilogger.Data.EventCategory TheCat = CS.GetCategoryBySlug(categorySlug);
            return GetData(e => e.CategoryID == TheCat.ID && e.IsPrivate==false);
        }

        public List<Event> FindByUserID(Guid userID) {
            return GetData().Where(e => e.UserID == userID && e.IsPrivate==false).OrderByDescending(e=>e.StartDateTime).ToList();
        }

        public List<Event> FindByUserIdPaged(Guid userId, int currentPage, int recordsPerPage)
        {
            var es = db.Events.Where(e => e.UserID == userId);
            return es.Skip(currentPage * recordsPerPage).Take(recordsPerPage).OrderByDescending(c => c.StartDateTime).ToList();
        }

        public int FindCountByUserID(Guid userID)
        {
            return db.Events.Count(e => e.UserID == userID && e.IsPrivate==false);
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
                      where (e.Name.Contains(SearchTerm) ||
                            e.SearchTerms.Contains(SearchTerm) || e.SubTitle.Contains(SearchTerm) || e.Description.Contains(SearchTerm)) && e.IsPrivate==false
                      orderby e.CreatedDateTime
                      select e;

            return EVs;

        }


        public int Count() {
            return base.db.Events.Count(e => e.IsPrivate==false);
        }



        public List<Event> GetTrendingEvents()
        {
            return db.GetTrendingEventsByActivity().ExecuteTypedList<Event>();
        }

        public IEnumerable<Event> FindAllActiveEvents()
        {
            return db.Events.Where(e => e.IsActive && e.IsPrivate==false);
        }

    }
}