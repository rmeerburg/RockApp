using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RockApp.Models
{
    public class Artist : IIdentifyable<Guid>
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public Guid ArtistId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Album> Albums { get; set; }

        Guid IIdentifyable<Guid>.Id => ArtistId;
    }
}