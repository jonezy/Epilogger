using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epilogger.Data;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface IEventManager
    {
        Event Create(Event item);
        List<int> CreateEvents(List<Event> items);
        Event Update(Event item);
        Event GetById(int id);
        List<Event> GetEvents(int? page, int? count);
        bool Delete(int id);
    }
}



