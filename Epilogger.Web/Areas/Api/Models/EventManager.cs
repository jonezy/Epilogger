using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epilogger.Data;

namespace Epilogger.Web.Areas.Api.Models
{
    public class EventManager : IEventManager
    {

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

        public Event GetById(int id)
        {
            return new Event
            {
                //Id = id,
                //Subject = "Loaded Subject",
                //Body = "Loaded Body",
                //AuthorName = "Loaded Author"
            };
        }

        public List<Event> GetEvents(int? page, int? count)
        {
            var event1 = new Event
            {
                //Id = 1,
                //Subject = "First Subject",
                //Body = "First Body",
                //AuthorName = "First Author"
            };
            var event2 = new Event
            {
                //Id = 2,
                //Subject = "Second Subject",
                //Body = "Second Body",
                //AuthorName = "Second Author"
            };
            var items = new List<Event> { event1, event2 };
            return items;
        }

        public bool Delete(int id) { return true; }
    }
 
}