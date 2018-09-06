using System.Collections.Generic;
using System.Threading.Tasks;
using rock_app.Models;

namespace rock_app.Services
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