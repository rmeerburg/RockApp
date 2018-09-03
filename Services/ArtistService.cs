using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using rock_app.Models;

namespace rock_app.Services
{
    public class ArtistService : IArtistService
    {
        private readonly RockAppContext _context;

        public ArtistService(RockAppContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Artist>> GetArtistsAsync() => _context.Artists;

        public Task CreateArtistAsync(Artist artist)
        {
            throw new NotImplementedException();
        }

        public Task DeleteArtist(Artist artist)
        {
            throw new NotImplementedException();
        }

        public Task DeleteArtist(Guid artistId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Artist>> FindArtistsAsync(Expression<Func<Artist, bool>> predicate) => _context.Artists.Where(predicate);

        public Task<Artist> GetArtistAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveArtistAsync(Artist artist)
        {
            throw new NotImplementedException();
        }
    }

    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetArtistsAsync();
        Task<Artist> GetArtistAsync(Guid id);
        Task<IEnumerable<Artist>> FindArtistsAsync(Expression<Func<Artist, bool>> predicate);
        Task CreateArtistAsync(Artist artist);
        Task SaveArtistAsync(Artist artist);
        Task DeleteArtist(Artist artist);
        Task DeleteArtist(Guid artistId);
    }
}