using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;
using Epilogger.Web.Core.Stats;
using Epilogger.Web.Models;


namespace Epilogger.Web.Areas.Api.Models
{
    public class MemoryBoxManager : IMemoryBoxManager
    {
        readonly MemoryBoxService _ms = new MemoryBoxService();
        readonly MemoryBoxItemService _msi = new MemoryBoxItemService();

        public MemoryBoxManager()
        {
            if (_ms == null) _ms = new MemoryBoxService();
            if (_msi == null) _msi = new MemoryBoxItemService();
        }

        //****************


        public ApiMemoryBox FindByUserId(Guid userId)
        {
            return Mapper.Map<MemoryBox, ApiMemoryBox>(_ms.FindByUserId(userId));
        }

        public ApiMemoryBox Save(ApiMemoryBox box)
        {
            return Mapper.Map<MemoryBox, ApiMemoryBox>(_ms.Save(Mapper.Map<ApiMemoryBox, MemoryBox>(box)));
        }


        public ApiMemoryBoxItem Save(ApiMemoryBoxItem box)
        {
            if (box.MemboxId == 0)
            {
                //Create the Mobile Membox
                var memBox = new MemoryBox
                                 {
                                     CreatedDateTime = DateTime.UtcNow,
                                     Name = "Mobile Memory Box",
                                     UserId = box.UserId,
                                     Type = "",
                                     IsActive = true
                                 };
                memBox = _ms.Save(memBox);
                box.MemboxId = memBox.ID;
            }

            return Mapper.Map<MemoryBoxItem, ApiMemoryBoxItem>(_msi.Save(Mapper.Map<ApiMemoryBoxItem, MemoryBoxItem>(box)));
        }


        public bool RemoveMemBoxItem(int id)
        {
            try
            {
                _msi.DeleteMemBoxItem(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ApiMemoryBoxItem> MemoryBoxItemsByMemBoxIdPaged(int memBoxId, int page, int count)
        {
            return Mapper.Map<List<MemoryBoxItem>, List<ApiMemoryBoxItem>>(_msi.MemoryBoxItemsByMemBoxIdPaged(memBoxId, page, count));
        }



    }
 
}