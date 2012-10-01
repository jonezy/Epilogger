using System.Collections.Generic;
using System;
using System.Linq;
using Epilogger.Data;
using Epilogger.Web.Model;
using SubSonic.Schema;
using System.Data;

namespace Epilogger.Web {
    public class LiveModeCustomSettingsService : ServiceBase<LiveModeCustomSetting>
    {
        protected override string CacheKey {
            get { return "Epilogger.LiveModeCustomSetting"; }
        }


        public object Save(LiveModeCustomSetting entity)
        {
            if (entity.Id > 0)
                return base.GetRepository<LiveModeCustomSetting>().Update(entity);

            return base.GetRepository<LiveModeCustomSetting>().Add(entity);
        }

        public LiveModeCustomSetting FindByEventID(int eventId)
        {
            var settings = GetData(v => v.EventId == eventId).FirstOrDefault();
            if (settings != null)
            {
                return settings;
            }

            return new LiveModeCustomSetting()
                       {
                           Logo = "/Public/images/livemode/client/logo.png",
                           Background = "",
                           FooterTextColor = "",
                           LinkColour = "",
                           SponsorLogos = "",
                           TwitterUserNameColour = ""
                       };
        }


    }
}