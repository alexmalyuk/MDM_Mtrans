using System;
using System.Collections.Generic;

namespace Data.Models.Core
{
    public interface ISubject
    {
        ICollection<HistoryEntry> Histories { get; set; }
        Guid Id { get; set; }
        ICollection<Link> Links { get; set; }
        string Name { get; set; }

        string Serialize();
    }
}