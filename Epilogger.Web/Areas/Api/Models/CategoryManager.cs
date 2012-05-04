using System.Collections.Generic;
using AutoMapper;
using Epilogger.Data;

namespace Epilogger.Web.Areas.Api.Models
{
    public class CategoryManager : ICategoryManager
    {
        readonly CategoryService _catS = new CategoryService();

        public CategoryManager()
        {
            if (_catS == null) _catS = new CategoryService();
        }

        public ApiCategory GetById(int id)
        {
            return new ApiCategory();
        }

        public List<ApiCategory> GetCategories()
        {
            return Mapper.Map<List<EventCategory>, List<ApiCategory>>(_catS.AllCategories());
            //return _catS.AllCategories();
        }

    }
 
}