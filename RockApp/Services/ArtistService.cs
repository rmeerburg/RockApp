using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RockApp.Models;

namespace RockApp.Services
{
    /// <summary>  
    ///     Supports all operations on <see cref="Artist"> instances
    /// </summary>  
    public class ArtistService : DataServiceBase<Artist, Guid>, IArtistService
    {
        protected override DbSet<Artist> DataSet => Context.Artists;

        public ArtistService(RockAppContext context)
            : base(context)
        { }
        
        public override IEnumerable<Artist> GetAll() => DataSet.Include(a => a.Albums);

        public override async Task Delete(Guid artistId)
        {
            DataSet.Remove(new Artist { ArtistId = artistId, });
            await Context.SaveChangesAsync();
        }

        public override Task<Artist> GetAsync(Guid id) => Task.FromResult(DataSet.Include(a => a.Albums).FirstOrDefault(a => a.ArtistId == id));
    }
}