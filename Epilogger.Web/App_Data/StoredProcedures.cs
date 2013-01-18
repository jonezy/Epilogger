


  
using System;
using SubSonic;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Epilogger.Data{
	public partial class EpiloggerDB{

        public StoredProcedure CheckDeleteEvent(int EventID){
            StoredProcedure sp=new StoredProcedure("CheckDeleteEvent",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEvent(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEvent",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventBlogPostsBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventBlogPostsBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventCheckinsBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventCheckinsBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventData(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventData",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventImageMetaDataBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventImageMetaDataBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventImagesBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventImagesBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventTweetsBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventTweetsBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventTweetsBatch2(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventTweetsBatch2",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteEventURLsBatch(int EventID){
            StoredProcedure sp=new StoredProcedure("DeleteEventURLsBatch",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteTweet(int ID){
            StoredProcedure sp=new StoredProcedure("DeleteTweet",this.Provider);
            sp.Command.AddParameter("ID",ID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteUser(string UserId){
            StoredProcedure sp=new StoredProcedure("DeleteUser",this.Provider);
            sp.Command.AddParameter("UserId",UserId,DbType.String);
            return sp;
        }
        public StoredProcedure GetCurrentActiveUsers(){
            StoredProcedure sp=new StoredProcedure("GetCurrentActiveUsers",this.Provider);
            return sp;
        }
        public StoredProcedure GetEventGrowthStats(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetEventGrowthStats",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetHomePageActivity(){
            StoredProcedure sp=new StoredProcedure("GetHomePageActivity",this.Provider);
            return sp;
        }
        public StoredProcedure GetImagesWithMemboxIDPaged(int EventID,int skip,int take,bool IncludeVideos){
            StoredProcedure sp=new StoredProcedure("GetImagesWithMemboxIDPaged",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("skip",skip,DbType.Int32);
            sp.Command.AddParameter("take",take,DbType.Int32);
            sp.Command.AddParameter("IncludeVideos",IncludeVideos,DbType.Boolean);
            return sp;
        }
        public StoredProcedure GetLastTweetIDByEventID(int EventID){
            StoredProcedure sp=new StoredProcedure("GetLastTweetIDByEventID",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure GetNumberOfImageRecordsByURL(string TheURL){
            StoredProcedure sp=new StoredProcedure("GetNumberOfImageRecordsByURL",this.Provider);
            sp.Command.AddParameter("TheURL",TheURL,DbType.AnsiString);
            return sp;
        }
        public StoredProcedure GetNumberOfNewUsersPerDay(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetNumberOfNewUsersPerDay",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetNumberOfRecordsByTwitterID(long TwitterID,long EventID){
            StoredProcedure sp=new StoredProcedure("GetNumberOfRecordsByTwitterID",this.Provider);
            sp.Command.AddParameter("TwitterID",TwitterID,DbType.Int64);
            sp.Command.AddParameter("EventID",EventID,DbType.Int64);
            return sp;
        }
        public StoredProcedure GetPhotosFromMemoryBox(int MemBoxId,int skip,int take){
            StoredProcedure sp=new StoredProcedure("GetPhotosFromMemoryBox",this.Provider);
            sp.Command.AddParameter("MemBoxId",MemBoxId,DbType.Int32);
            sp.Command.AddParameter("skip",skip,DbType.Int32);
            sp.Command.AddParameter("take",take,DbType.Int32);
            return sp;
        }
        public StoredProcedure GetRandomImagesByEventID(int EventID,int RecordsToReturn){
            StoredProcedure sp=new StoredProcedure("GetRandomImagesByEventID",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("RecordsToReturn",RecordsToReturn,DbType.Int32);
            return sp;
        }
        public StoredProcedure GetTop10TweetersByEventID(int EventID,DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetTop10TweetersByEventID",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetTopEventByUserActivity(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetTopEventByUserActivity",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetTopPhotosByEventID(int EventID,int ItemToReturn,DateTime FromDate,DateTime ToDate,bool IncludeVideos){
            StoredProcedure sp=new StoredProcedure("GetTopPhotosByEventID",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("ItemToReturn",ItemToReturn,DbType.Int32);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            sp.Command.AddParameter("IncludeVideos",IncludeVideos,DbType.Boolean);
            return sp;
        }
        public StoredProcedure GetTopURLsByEventID(int EventID,int ItemToReturn,DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetTopURLsByEventID",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("ItemToReturn",ItemToReturn,DbType.Int32);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetTrendingEventsByActivity(){
            StoredProcedure sp=new StoredProcedure("GetTrendingEventsByActivity",this.Provider);
            return sp;
        }
        public StoredProcedure GetTweetsFromMemoryBox(int MemBoxId,int skip,int take){
            StoredProcedure sp=new StoredProcedure("GetTweetsFromMemoryBox",this.Provider);
            sp.Command.AddParameter("MemBoxId",MemBoxId,DbType.Int32);
            sp.Command.AddParameter("skip",skip,DbType.Int32);
            sp.Command.AddParameter("take",take,DbType.Int32);
            return sp;
        }
        public StoredProcedure GetTweetsPerHourStats(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetTweetsPerHourStats",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetTwitterSearchesPerHour(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetTwitterSearchesPerHour",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetUserDashboardActivity(string UserID){
            StoredProcedure sp=new StoredProcedure("GetUserDashboardActivity",this.Provider);
            sp.Command.AddParameter("UserID",UserID,DbType.AnsiString);
            return sp;
        }
        public StoredProcedure GetUserGrowthStats(DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("GetUserGrowthStats",this.Provider);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure GetUsersEventActivity(string UserID){
            StoredProcedure sp=new StoredProcedure("GetUsersEventActivity",this.Provider);
            sp.Command.AddParameter("UserID",UserID,DbType.AnsiString);
            return sp;
        }
        public StoredProcedure GetWeightedKeyWords(int EventID){
            StoredProcedure sp=new StoredProcedure("GetWeightedKeyWords",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            return sp;
        }
        public StoredProcedure SearchEvents(string SearchTerm){
            StoredProcedure sp=new StoredProcedure("SearchEvents",this.Provider);
            sp.Command.AddParameter("SearchTerm",SearchTerm,DbType.String);
            return sp;
        }
        public StoredProcedure SearchInEvent(int EventID,string SearchTerm,DateTime FromDate,DateTime ToDate){
            StoredProcedure sp=new StoredProcedure("SearchInEvent",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("SearchTerm",SearchTerm,DbType.String);
            sp.Command.AddParameter("FromDate",FromDate,DbType.DateTime);
            sp.Command.AddParameter("ToDate",ToDate,DbType.DateTime);
            return sp;
        }
        public StoredProcedure TweetsAndImageByEventIDPaged(int EventID,int skip,int take,bool IncludeVideos){
            StoredProcedure sp=new StoredProcedure("TweetsAndImageByEventIDPaged",this.Provider);
            sp.Command.AddParameter("EventID",EventID,DbType.Int32);
            sp.Command.AddParameter("skip",skip,DbType.Int32);
            sp.Command.AddParameter("take",take,DbType.Int32);
            sp.Command.AddParameter("IncludeVideos",IncludeVideos,DbType.Boolean);
            return sp;
        }
	
	}
	
}
 