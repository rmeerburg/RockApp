using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rock_app.Models;

namespace rock_app.Services
{
    /// <summary>  
    ///     Supports all operations on <see cref="Song"> instances
    /// </summary>  
    public class SongService : ISongService
    {
        private readonly RockAppContext _context;

        public SongService(RockAppContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Song>> GetSongsAsync() => _context.Songs;

        public async Task<Song> CreateSongAsync(Song song)
        {
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();
            return song;
        }

        public async Task DeleteSong(Song song)
        {
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSong(Guid songId)
        {
            _context.Songs.Remove(new Song { SongId = songId, });
            await _context.SaveChangesAsync();
        }

        public Task<Song> GetSongAsync(Guid id) => Task.FromResult(_context.Songs.FirstOrDefault(a => a.SongId == id));

        public async Task<Song> SaveSongAsync(Song song)
        {
            var savedSong = _context.Songs.Update(song);
            await _context.SaveChangesAsync();
            return savedSong.Entity;
        }
    }
}