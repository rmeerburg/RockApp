using System;
using System.Collections.Generic;

namespace rock_app.Models
{
    public class Album
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }

        public Guid AlbumId { get; set; }

        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}