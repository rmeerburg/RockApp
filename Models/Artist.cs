using System;
using System.Collections.Generic;

namespace rock_app
{
    class Artist
    {
        public Guid ArtistId { get; set; }
        public string Name { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}