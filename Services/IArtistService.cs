using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using rock_app.Models;

namespace rock_app.Services
{
    /// <summary>  
    ///     Supports all operations on <see cref="Artist"> instances
    /// </summary>  
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetArtistsAsync();
        Task<Artist> GetArtistAsync(Guid id);
        Task<IEnumerable<Artist>> FindArtistsAsync(Expression<Func<Artist, bool>> predicate);
        Task<Artist> CreateArtistAsync(Artist artist);
        Task SaveArtistAsync(Artist artist);
        Task DeleteArtist(Artist artist);
        Task DeleteArtist(Guid artistId);
    }
}