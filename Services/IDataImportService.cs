using System.Collections.Generic;
using System.Threading.Tasks;
using rock_app.Models;

namespace rock_app.Services
{
    public interface IDataImportService
    {
        Task<ImportResult> ImportArtists(IEnumerable<Artist> artists);
        Task<ImportResult> ImportSongs(IEnumerable<ImportSong> songs);
    }
}