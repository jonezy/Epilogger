﻿using System;
using System.Collections.Generic;

namespace Epilogger.Web.Models {
    public class EventDisplayViewModel {

        private string mDescription = string.Empty;
        
        public int ID { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string Description {
            get { return mDescription; }
            set { if (value != null)
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
        public string SearchTerms { get; set; }
        public int TimeZoneOffSet { get; set; }
        public string Cost { get; set; }
        public string FacebookPageURL { get; set; }
        public string TwitterAccount { get; set; }

        public int TweetCount { get; set; }
        public int ImageCount { get; set; }

        public IEnumerable<Epilogger.Data.Tweet> Tweets { get; set; }
        public IEnumerable<Epilogger.Data.Image> Images { get; set;}
        public IEnumerable<Epilogger.Data.CheckIn> CheckIns { get; set; }
        public IEnumerable<Epilogger.Data.URL> ExternalLinks { get; set; }
        public IEnumerable<Epilogger.Data.BlogPost> BlogPosts { get; set; }
        
    }
}