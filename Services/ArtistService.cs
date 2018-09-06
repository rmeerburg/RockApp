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
    ///     Supports all operations on <see cref="Artist"> instances
    /// </summary>  
    public class ArtistService : IArtistService
    {
        private readonly RockAppContext _context;

        public ArtistService(RockAppContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Artist>> GetArtistsAsync() => _context.Artists.Include(a => a.Albums);

        public async Task<Artist> CreateArtistAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
            return artist;
        }

        public async Task DeleteArtist(Artist artist)
        {
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArtist(Guid artistId)
        {
            _context.Artists.Remove(new Artist { ArtistId = artistId, });
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Artist>> FindArtistsAsync(Expression<Func<Artist, bool>> predicate) => _context.Artists.Where(predicate);

        public Task<Artist> GetArtistAsync(Guid id) => Task.FromResult(_context.Artists.Include(a => a.Albums).FirstOrDefault(a => a.ArtistId == id));

        public async Task SaveArtistAsync(Artist artist)
        {
            var dbArtist = _context.Artists.FirstOrDefault(a => a.ArtistId == artist.ArtistId);
            dbArtist.Name = artist.Name;
            await _context.SaveChangesAsync();
        }
    }
}