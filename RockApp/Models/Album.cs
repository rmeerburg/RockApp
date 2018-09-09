using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RockApp.Models
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
        [Required]
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}