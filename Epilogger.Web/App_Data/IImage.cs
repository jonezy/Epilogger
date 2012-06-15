using System;
using System.ComponentModel;
using System.Linq;

namespace Epilogger.Data
{
    public interface IImage
    {
        int ID { get; set; }
        int EventID { get; set; }
        string AzureContainerPrefix { get; set; }
        string Fullsize { get; set; }
        string Thumb { get; set; }
        string OriginalImageLink { get; set; }
        DateTime DateTime { get; set; }
        int? DeleteVoteCount { get; set; }
        bool Deleted { get; set; }
        string ImageFingerPrint { get; set; }
        IQueryable<ImageMetaDatum> ImageMetaData { get; }
        IQueryable<Event> Events { get; }
        event PropertyChangingEventHandler PropertyChanging;
        event PropertyChangedEventHandler PropertyChanged;
    }
}