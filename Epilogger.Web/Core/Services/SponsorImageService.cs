using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epilogger.Data;

namespace Epilogger.Web.Core.Services
{
    public class SponsorImageService: ServiceBase<SponsorImage>
    {

        protected override string CacheKey {
            get { return "Epilogger.SponsorImage"; }
        }


        public object Save(SponsorImage entity)
        {
            if (entity.ID > 0)
                return base.GetRepository<SponsorImage>().Update(entity);

            return base.GetRepository<SponsorImage>().Add(entity);
        }

        public List<SponsorImage> FindByLiveID(int liveId)
        {
            var settings = GetData(v => v.LiveModeID == liveId).ToList();
            if (settings != null)
            {
                return settings;
            }

            SponsorImage si = new SponsorImage();
           List<SponsorImage> nullList = new List<SponsorImage>()
                       {
                          si
                       };
           return nullList;
        }
        public int FindIDByEventID(int liveId)
        {
            var settings = GetData(v => v.LiveModeID == liveId).FirstOrDefault();
            if (settings != null)
            {
                return settings.ID;
            }

            return 0;
        }

        public bool DeleteSponsor(int liveModeID, string url)
        {
            try
            {
                var sponsor = db.SponsorImages.Where(d => d.LiveModeID == liveModeID && d.SponsorURL == url).FirstOrDefault();
                if (sponsor != null)
                {
                    base.GetRepository<SponsorImage>().Delete(sponsor);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}