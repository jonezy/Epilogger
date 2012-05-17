using System.Collections.Generic;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface ICategoryManager
    {
        //Event Create(Event item);
        //List<int> CreateEvents(List<Event> items);
        //Event Update(Event item);
        ApiCategory GetById(int id);
        List<ApiCategory> GetCategories();
        //bool Delete(int id);
    }
}