using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RockApp.Models;

namespace RockApp.Services
{
    /// <summary>  
    ///     Supports all operations on <see cref="Artist"> instances
    /// </summary>  
    public interface IArtistService : IDataServiceBase<Artist, Guid>
    {
    }
}