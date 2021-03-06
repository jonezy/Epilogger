﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Epilogger.Data;
using Epilogger.Web.Areas.Api.Models.Classes;

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
            currentPage = currentPage - 1;
            var mi = db.MemoryBoxItems.Where(e => e.MemboxId == memBoxId).OrderByDescending(e => e.ID);
            return mi.Skip(currentPage * recordsPerPage).Take(recordsPerPage).ToList();
        }

        //public List<Tweet> MemoryBoxTweetsByMemBoxIdPaged(int memBoxId, int currentPage, int recordsPerPage)
        //{
        //    currentPage = currentPage - 1;

        //    return Mapper.Map<List<Tweet>, List<ApiTweet>>(_ts.GetTweetsInMemoryBoxPaged(memBoxId, currentPage, recordsPerPage).ToList());


        //    //var mi = from mbis in db.MemoryBoxItems
        //    //         where mbis.MemboxId == memBoxId && mbis.ItemType == "Tweet"
        //    //         orderby mbis.ID descending
        //    //         select mbis;

        //    //var tweets = from twts in db.Tweets
        //    //             where twts.ID = mi;




        //    //var mi = db.MemoryBoxItems.Where(e => e.MemboxId == memBoxId && e.ItemType=="Tweet").OrderByDescending(e => e.ID).Select();
        //    //return mi.Skip(currentPage * recordsPerPage).Take(recordsPerPage).ToList();
        //}


        public List<MemoryBox> MemoryBoxByUserId(Guid userId)
        {
            return db.MemoryBoxes.Where(e => e.UserId == userId).ToList();
        }

        public List<MemoryBox> MemoryBoxByUserIdandEventId(Guid userId, int eventId)
        {
            return db.MemoryBoxes.Where(e => e.UserId == userId && e.EventId == eventId).ToList();
        }


    }
}