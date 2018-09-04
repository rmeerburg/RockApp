using System;

namespace rock_app.Models
{
    public class Song
    {
        public Guid SongId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Shortname { get; set; }
        public int? Bpm { get; set; }
        public int Duration { get; set; }
        public string SpotifyId { get; set; }

        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
        
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        public Guid? AlbumId { get; set; }
        public Album Album { get; set; }
    }
}