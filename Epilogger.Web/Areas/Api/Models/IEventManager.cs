using System.Collections.Generic;
using Epilogger.Data;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface IEventManager
    {
        Event Create(Event item);
        List<int> CreateEvents(List<Event> items);
        Event Update(Event item);
        ApiEvent GetById(int id);
        List<ApiEvent> GetEvents(int? page, int? count);
        bool Delete(int id);
    }
}



