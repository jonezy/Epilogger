using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using RichmondDay.Helpers;
using SubSonic.Schema;

namespace Epilogger.Web.Core.Stats
{
    public class EventStats
    {
        private const string CacheKey = "EventStats";
        protected virtual double CacheExpiry
        {
            get { return 1; }
        }

        public int AllEventCount()
        {
            return new EventService().Count();
        }

        public int ActiveEventCount()
        {
            return new EventService().GoingOnNowEventsCount();
        }

        public int CollectingEventCount()
        {
            return new EventService().GetNumberOfCollectingEvents();
        }



    }

}