using System.Collections.Generic;
using System.Threading.Tasks;
using rock_app.Models;

namespace rock_app.Services
{
    public class ImportFilterService
    {
        public bool ShouldImport(ImportSong song) => song.Genre.ToLowerInvariant().Contains("metal");

        public bool ShouldImport(Artist artist) => true; // how to determine the starting year of a band?
    }
}