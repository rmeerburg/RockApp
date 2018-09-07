using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RockApp.Models;

namespace RockApp.Services
{
    /// <summary>  
    ///     Supports all operations on <see cref="Song"> instances
    /// </summary>  
    public interface ISongService : IDataServiceBase<Song, Guid>
    {
    }
}