using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Epilogger.Data;

namespace Epilogger.Web {

    public class MemoryBoxItemService : ServiceBase<MemoryBoxItem>
    {
        protected override string CacheKey {
            get { return "Epilogger.MemoryBoxItem"; }
        }

        public MemoryBoxItem Save(MemoryBoxItem entity)
        {
            if (entity.ID > 0)
            {
                var thereturn = GetRepository<MemoryBoxItem>().Update(entity);
                return GetData(e => e.ID == int.Parse(thereturn.ToString(CultureInfo.InvariantCulture))).FirstOrDefault();
            }
            else
            {
                var thereturn = GetRepository<MemoryBoxItem>().Add(entity);
                return GetData(e => e.ID == int.Parse(thereturn.ToString())).FirstOrDefault();
            }
        }

        public MemoryBoxItem FindByID(int id)
        {
            return GetData(v => v.ID == id).FirstOrDefault();
        }

        public MemoryBoxItem FindByMemoryBoxId(int memoryBoxId)
        {
            return GetData(v => v.MemboxId == memoryBoxId).FirstOrDefault();
        }

        public void DeleteMemBoxItem(int id)
        {
            var memBoxItem = db.MemoryBoxItems.FirstOrDefault(m => m.ID == id);
            GetRepository<MemoryBoxItem>().Delete(memBoxItem);
        }

        public List<MemoryBoxItem> MemoryBoxItemsByMemBoxIdPaged(int memBoxId, int currentPage, int recordsPerPage)
        {
            var mi = db.MemoryBoxItems.Where(e => e.MemboxId == memBoxId).OrderByDescending(e => e.ID);
            return mi.Skip(currentPage * recordsPerPage).Take(recordsPerPage).ToList();
        }


    }
}