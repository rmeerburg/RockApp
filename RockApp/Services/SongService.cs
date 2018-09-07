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
    ///     Supports all operations on <see cref="Song"> instances
    /// </summary>  
    public class SongService : DataServiceBase<Song, Guid>, ISongService
    {

        public SongService(RockAppContext context)
            : base(context)
        { }

        protected override DbSet<Song> DataSet => Context.Songs;

        public override async Task Delete(Guid songId)
        {
            DataSet.Remove(new Song { SongId = songId, });
            await Context.SaveChangesAsync();
        }
    }
}