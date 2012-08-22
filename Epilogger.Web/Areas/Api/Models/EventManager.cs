using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;
using Epilogger.Web.Models;

namespace Epilogger.Web.Areas.Api.Models
{
    public class EventManager : IEventManager
    {
        EventService _es = new EventService();
        UserService _us = new UserService();

        public EventManager()
        {
            if (_es == null) _es = new EventService();
            if (_us == null) _us = new UserService();    
        }

        public Event Create(Event item)
        {
            item.ID = 1;
            return item;
        }
        public List<int> CreateEvents(List<Event> items)
        {
            return new List<int> { 1, 2, 3 };
        }
        public Event Update(Event item) { return item; }
        public bool Delete(int id) { return true; }




        //****************
        public ApiEvent GetById(int id)
        {
            return Mapper.Map<Event, ApiEvent>(_es.FindByID(id));
        }

        public List<ApiEvent> GetEvents(int? page, int? count)
        {
            return Mapper.Map<List<Event>, List<ApiEvent>>(_es.AllEventsDescPaged((int)page, (int)count));
        }

        public List<ApiEvent> GetTrendingEvents()
        {
            var eplRedis = new Common.Redis();
            var trendingEventId = eplRedis.GetTrendingEvents();
            var trendingEvents = new List<Event>();
            foreach (var e in trendingEventId.Take(10).Select(eventId => _es.FindByID((int.Parse(eventId)))))
            {
                if (e != null)
                {
                    e.Name = e.Name;
                    trendingEvents.Add(e);
                }
            }

            return Mapper.Map<List<Event>, List<ApiEvent>>(trendingEvents);
        }


        public List<ApiEvent> SearchEvents(string searchTerm)
        {
            return Mapper.Map<List<Event>, List<ApiEvent>>(_es.GetEventsBySearchTerm(searchTerm).ToList());
        }

        public List<ApiEvent> GetFeaturedEvents()
        {
            return Mapper.Map<List<Event>, List<ApiEvent>>(_es.GetFeaturedEvents().ToList());
        }

        public List<ApiEvent> EventsByCategoyIdPaged(int categoryId, int page, int count)
        {
            return Mapper.Map<List<Event>, List<ApiEvent>>(_es.GetEventsByCategoryIDDescPaged(categoryId, page, count));
        }

        public List<ApiSearchInEvent> SearchInEvent(int eventId, string searchTerm)
        {

            return Mapper.Map<List<SearchInEventModel>, List<ApiSearchInEvent>>(_es.SearchInEvent(eventId, searchTerm, (DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue));
        }
        //SearchInEvent(int EventID, string SearchTerm, DateTime FromDateTime, DateTime ToDateTime)


        public List<ApiEvent> GetUserSubscribedAndCreatedEvents(Guid userID, int page, int count)
        {
            return Mapper.Map<List<Event>, List<ApiEvent>>(_us.GetUserSubscribedAndCreatedEvents(userID, page, count).ToList());
        }

        
    }
 
}