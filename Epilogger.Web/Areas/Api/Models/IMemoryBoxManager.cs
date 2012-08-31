using System;
using System.Collections.Generic;
using Epilogger.Web.Areas.Api.Models.Classes;

namespace Epilogger.Web.Areas.Api.Models
{
    public interface IMemoryBoxManager
    {
        ApiMemoryBox FindByUserId(Guid userId);
        ApiMemoryBox Save(ApiMemoryBox box);
        ApiMemoryBoxItem Save(ApiMemoryBoxItem box);
        bool RemoveMemBoxItem(int id);
        List<ApiMemoryBoxItem> MemoryBoxItemsByMemBoxIdPaged(int memBoxId, int page, int count);
        List<ApiMemoryBox> MemoryBoxByUserId(Guid userId);
        List<ApiMemoryBox> MemoryBoxByUserIdandEventId(Guid userId, int eventId);
        List<ApiMemoryBoxTweet> TweetsByMemBoxIdPaged(int memBoxId, int page, int count);
        List<ApiImage> PhotosByMemBoxIdPaged(int memBoxId, int page, int count);
    }
}