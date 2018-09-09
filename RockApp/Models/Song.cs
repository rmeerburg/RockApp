using System;
using System.ComponentModel.DataAnnotations;

namespace RockApp.Models
{
    public class Song : IIdentifyable<Guid>
    {
        public Guid SongId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Year { get; set; }
        public string Shortname { get; set; }
        public int? Bpm { get; set; }
        [Required]
        public int Duration { get; set; }
        public string SpotifyId { get; set; }

        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
        
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        public Guid? AlbumId { get; set; }
        public Album Album { get; set; }

        Guid IIdentifyable<Guid>.Id => SongId;
    }
}