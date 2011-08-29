using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;

namespace Epilogger.Web.Model
{
    public class AzureTweetModel : TableServiceEntity
    {
        
        public AzureTweetModel() : base() { }
        public AzureTweetModel(string partitionKey, string rowKey) : base(partitionKey, rowKey) { }

	    public long TwitterID { get; set; }
	    public long EventID { get; set; }
	    public string Text { get; set; }
	    public string TextAsHTML { get; set; }
	    public string Source { get; set; }
	    public DateTime CreatedDate { get; set; }
	    public string FromUserScreenName { get; set; }
	    public string ToUserScreenName { get; set; }
	    public string IsoLanguageCode { get; set; }
	    public string ProfileImageURL { get; set; }
	    public long SinceID { get; set; }
	    public string Location { get; set; }
	    public string RawSource { get; set; }

    }
}