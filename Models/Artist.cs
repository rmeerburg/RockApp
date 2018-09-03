using System;
using System.Collections.Generic;

namespace rock_app.Models
{
    public class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public Guid ArtistId { get; set; }
        public string Name { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}