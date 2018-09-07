using System.Collections.Generic;
using System.Threading.Tasks;
using RockApp.Models;

namespace RockApp.Services
{
    /// <summary>  
    ///     Imports artists or songs supplied in json format into the database
    /// </summary> 
    public interface IDataImportService
    {
        Task<ImportResult> ImportArtists(IEnumerable<Artist> artists);
        Task<ImportResult> ImportSongs(IEnumerable<ImportSong> songs);
    }
}