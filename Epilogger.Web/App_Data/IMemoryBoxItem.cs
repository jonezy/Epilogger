using System;
using System.ComponentModel;
using System.Linq;

namespace Epilogger.Data
{
    public interface IMemoryBoxItem
    {
        int ID { get; set; }
        int MemboxId { get; set; }
        string ItemType { get; set; }
        int? ItemId { get; set; }
        DateTime? AddedDateTime { get; set; }
        IQueryable<MemoryBox> MemoryBoxes { get; }
        event PropertyChangingEventHandler PropertyChanging;
        event PropertyChangedEventHandler PropertyChanged;
    }
}