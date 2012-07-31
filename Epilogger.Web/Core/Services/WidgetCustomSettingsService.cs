using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;
using SubSonic.Schema;
using System.Data;

namespace Epilogger.Web {
    public class WidgetCustomSettingsService : ServiceBase<WidgetCustomSetting>
    {
        protected override string CacheKey {
            get { return "Epilogger.WidgetCustomSetting"; }
        }


        public object Save(WidgetCustomSetting entity)
        {
            if (entity.Id == Guid.Empty)
                return base.GetRepository<WidgetCustomSetting>().Update(entity);

            return base.GetRepository<WidgetCustomSetting>().Add(entity);
        }

        public WidgetCustomSetting FindByEventID(int eventId)
        {
            var settings = GetData(v => v.EventId == eventId).FirstOrDefault();
            if (settings != null)
            {
                return settings;
            }

            return new WidgetCustomSetting()
                       {
                           Logo = "",
                           HeaderBGColor = "#636161",
                           HeaderTextColor = "#fff",
                           HeaderLinkColor = "#fff",
                           ContentBGColor = "#fff",
                           ContentTextColor = "#6e6e6e",
                           ContentLinkColor = "#0099cc",
                           SpriteColor = "#636161"
                       };
        }


    }
}