using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Models;

namespace Epilogger.Web.Areas.Api.Models
{
    public class EventManager : IEventManager
    {
        EventService _es = new EventService();

        public EventManager()
        {
            if (_es == null) _es = new EventService();    
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

        
    }
 
}