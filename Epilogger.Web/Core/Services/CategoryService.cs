using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;

namespace Epilogger.Web
{
    public class CategoryService : ServiceBase<EventCategory>
    {
        protected override string CacheKey
        {
            get { return "Epilogger.Web.EventCategory"; }
        }

        public List<EventCategory> AllCategories()
        {
            return base.GetData();
        }

        public EventCategory GetCategoryBySlug(string CategorySlug)
        {
            return db.EventCategories.Where(c => c.URLStub == CategorySlug).FirstOrDefault();
        }

    }
}