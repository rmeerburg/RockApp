using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using rock_app.Models;

namespace rock_app.Services
{
    public class DataSeedService : IDataSeedService
    {
        private readonly RockAppContext _context;
        private readonly ILogger _logger;

        public DataSeedService(RockAppContext context, ILogger<DataSeedService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task ImportArtists(IEnumerable<Artist> artists)
        {
            foreach (var artist in artists)
                await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
        }

        public async Task ImportSongs(IEnumerable<ImportSong> songs)
        {
            foreach (var song in songs)
                await ImportSong(song);
            await _context.SaveChangesAsync();
        }

        private async Task ImportSong(ImportSong song)
        {
            if (_context.Songs.Any(s => s.Name == song.Name))
                throw new Exception("song already exists");

            var artist = _context.Artists.FirstOrDefault(a => a.Name == song.Artist);
            if (artist == null)
            {
                _logger.LogError($"Unable to import album '{song.Album}' for artist '{song.Artist}'. No artist found with that name");
                return;
            }

            Genre genre = null;
            if ((genre = _context.Genres.Local.Concat(_context.Genres).FirstOrDefault(g => g.Name == song.Genre)) == null)
                genre = ImportGenre(song.Genre);

            Album album = null;
            if (!string.IsNullOrEmpty(song.Album) && (album = GetAlbum(song.Album, song.Artist)) == null)
                album = ImportAlbum(song.Album, _context.Artists.First(a => a.Name == song.Artist));

            await _context.Songs.AddAsync(new Song { Bpm = song.Bpm, Duration = song.Duration, Name = song.Name, Shortname = song.Shortname, SpotifyId = song.SpotifyId, Year = song.Year, AlbumId = album.AlbumId, ArtistId = artist.ArtistId, GenreId = genre.GenreId, });
        }

        private Genre ImportGenre(string genreName)
        {
            var newGenre = new Genre { Name = genreName, };
            _context.Genres.Add(newGenre);
            return newGenre;
        }

        private Album ImportAlbum(string albumName, Artist artist)
        {
            var newAlbum = new Album { Name = albumName, ArtistId = artist.ArtistId, };
            _context.Albums.Add(newAlbum);
            return newAlbum;
        }

        private Album GetAlbum(string albumName, string artistName) => _context.Albums.Local.Concat(_context.Albums).FirstOrDefault(a => a.Name == albumName && a.Artist.Name == artistName);
    }

    public interface IDataSeedService
    {
        Task ImportArtists(IEnumerable<Artist> artists);
        Task ImportSongs(IEnumerable<ImportSong> songs);
    }

    public class ImportSong
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Artist { get; set; }
        public string Shortname { get; set; }
        public int? Bpm { get; set; }
        public int Duration { get; set; }
        public string Genre { get; set; }
        public string SpotifyId { get; set; }
        public string Album { get; set; }
    }
}