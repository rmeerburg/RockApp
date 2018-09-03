using System;
using System.Collections.Generic;

namespace rock_app
{
    class Album
    {
        public Guid AlbumId { get; set; }
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}