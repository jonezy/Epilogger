﻿using System;
using System.Collections.Generic;

namespace Epilogger.Web.Areas.Api.Models
{
    public class ApiEvent
    {
        private string mDescription = string.Empty;

        public int ID { get; set; }
        public string EventSlug { get; set; }
        //public Guid UserID { get; set; }
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string Description
        {
            get { return mDescription; }
            set
            {
                if (value != null)
                {
                    mDescription = value;
                }
                else
                {
                    mDescription = string.Empty;
                }
            }
        }
        public int CategoryID { get; set; }
        public string WebsiteURL { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime CollectionStartDateTime { get; set; }
        public DateTime CollectionEndDateTime { get; set; }
        
        public string SearchTerms { get; set; }
        public bool CanDelete { get; set; }

        public int TimeZoneOffSet { get; set; }
        public string Cost { get; set; }

        public string FacebookPageURL { get; set; }
        public string TwitterAccount { get; set; }

        public int TweetCount { get; set; }
        public int ImageCount { get; set; }
        public int CheckInCount { get; set; }

        //public bool HasSubscribed { get; set; }
        //public bool HasUserRated { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }

        //public List<Epilogger.Data.UserRatesEvent> EventRatings { get; set; }
        //public Epilogger.Data.Venue Venue { get; set; }

    }
}