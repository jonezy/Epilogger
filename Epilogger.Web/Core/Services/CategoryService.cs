﻿using System.Collections.Generic;
using System.Linq;
using Epilogger.Data;

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
            return base.GetData().OrderBy(c => c.CategoryName).ToList();
        }

        public EventCategory GetCategoryBySlug(string categorySlug)
        {
            return db.EventCategories.FirstOrDefault(c => c.URLStub == categorySlug);
        }

    }
}