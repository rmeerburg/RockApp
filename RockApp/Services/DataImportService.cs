using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RockApp.Models;

namespace RockApp.Services
{
    /// <summary>  
    ///     Imports artists or songs supplied in json format into the database
    /// </summary>  
    public class DataImportService : IDataImportService
    {
        private readonly RockAppContext _context;
        private readonly ILogger _logger;
        private readonly ImportFilterService _importFilter;

        public DataImportService(RockAppContext context, ILogger<DataImportService> logger, ImportFilterService importFilter)
        {
            _context = context;
            _logger = logger;
            _importFilter = importFilter;
        }

        public async Task<ImportResult> ImportArtists(IEnumerable<Artist> artists)
        {
            // log skipped count?
            // var partitionedByShouldImport = artists.GroupBy(_importFilter.ShouldImport);
            // var result = new ImportResult { SkippedCount = partitionedByShouldImport.Where(group => !group.Key).Count(), };

            var result = new ImportResult();
            foreach (var artist in artists.Where(_importFilter.ShouldImport))
            {
                await _context.Artists.AddAsync(artist);
                result.ImportedCount++;
            }
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<ImportResult> ImportSongs(IEnumerable<ImportSong> songs)
        {
            var result = new ImportResult();
            foreach (var song in songs.Where(_importFilter.ShouldImport))
            {
                if (await ImportSong(song))
                    result.ImportedCount++;
            }
            await _context.SaveChangesAsync();
            return result;
        }

        private async Task<bool> ImportSong(ImportSong song)
        {
            if (_context.Songs.Any(s => s.Name == song.Name))
                throw new Exception($"Unable to import song: '{song.Name}' already exists");

            var artist = _context.Artists.FirstOrDefault(a => a.Name == song.Artist);
            if (artist == null)
            {
                _logger.LogError($"Unable to import album: '{song.Album}' for artist '{song.Artist}'. No artist found with that name");
                return false;
            }

            Genre genre = null;
            if ((genre = _context.Genres.Local.Concat(_context.Genres).FirstOrDefault(g => g.Name == song.Genre)) == null)
                genre = CreateGenre(song.Genre);

            Album album = null;
            if (!string.IsNullOrEmpty(song.Album) && (album = GetAlbum(song.Album, song.Artist)) == null)
                album = CreateAlbum(song.Album, _context.Artists.First(a => a.Name == song.Artist));

            await _context.Songs.AddAsync(new Song { Bpm = song.Bpm, Duration = song.Duration, Name = song.Name, Shortname = song.Shortname, SpotifyId = song.SpotifyId, Year = song.Year, AlbumId = album?.AlbumId, ArtistId = artist.ArtistId, GenreId = genre.GenreId, });
            return true;
        }

        private Genre CreateGenre(string genreName)
        {
            var newGenre = new Genre { Name = genreName, };
            _context.Genres.Add(newGenre);
            return newGenre;
        }

        private Album CreateAlbum(string albumName, Artist artist)
        {
            var newAlbum = new Album { Name = albumName, ArtistId = artist.ArtistId, };
            _context.Albums.Add(newAlbum);
            return newAlbum;
        }

        private Album GetAlbum(string albumName, string artistName) => _context.Albums.Local.Concat(_context.Albums).FirstOrDefault(a => a.Name == albumName && a.Artist.Name == artistName);
    }

    public class ImportResult
    {
        public int ImportedCount { get; set; }
    }
}