using System;
using System.Globalization;
using System.Linq;
using Epilogger.Data;

namespace Epilogger.Web {
    public class MemoryBoxService : ServiceBase<MemoryBox> {
        protected override string CacheKey {
            get { return "Epilogger.MemoryBoxes"; }
        }

        public MemoryBox Save(MemoryBox entity)
        {
            if (entity.ID > 0)
            {
                var thereturn = GetRepository<MemoryBox>().Update(entity);
                return GetData(e => e.ID == int.Parse(thereturn.ToString(CultureInfo.InvariantCulture))).FirstOrDefault();
            }
            else
            {
                var thereturn = GetRepository<MemoryBox>().Add(entity);
                return GetData(e => e.ID == int.Parse(thereturn.ToString())).FirstOrDefault();
            }

        }

        public MemoryBox FindByID(int id)
        {
            return GetData(v => v.ID == id).FirstOrDefault();
        }

        public MemoryBox FindByUserId(Guid userId)
        {
            return GetData(v => v.UserId == userId).FirstOrDefault();
        }

    }
}