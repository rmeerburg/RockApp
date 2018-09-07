using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using rock_app.Models;

namespace rock_app.Services
{
    /// <summary>  
    ///     Supports all operations on <see cref="Song"> instances
    /// </summary>  
    public interface ISongService
    {
        Task<IEnumerable<Song>> GetSongsAsync();
        Task<Song> GetSongAsync(Guid id);
        Task<Song> CreateSongAsync(Song Song);
        Task<Song> SaveSongAsync(Song Song);
        Task DeleteSong(Song Song);
        Task DeleteSong(Guid SongId);
    }
}