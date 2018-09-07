using System;
using System.Collections.Generic;

namespace RockApp.Models
{
    public class Artist : IIdentifyable<Guid>
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public Guid ArtistId { get; set; }
        public string Name { get; set; }
        public ICollection<Album> Albums { get; set; }

        Guid IIdentifyable<Guid>.Id => ArtistId;
    }
}