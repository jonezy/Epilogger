using System;
using System.ComponentModel;
using System.Linq;

namespace Epilogger.Data
{
    public interface IMemoryBox
    {
        int ID { get; set; }
        Guid UserId { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        DateTime CreatedDateTime { get; set; }
        bool IsActive { get; set; }
        IQueryable<User> Users { get; }
        IQueryable<MemoryBoxItem> MemoryBoxItems { get; }
        event PropertyChangingEventHandler PropertyChanging;
        event PropertyChangedEventHandler PropertyChanged;
    }
}