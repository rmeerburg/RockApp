using System.Collections.Generic;
using System.Threading.Tasks;
using RockApp.Models;

namespace RockApp.Services
{
    /// <summary>  
    ///     Decides which artists and songs should be imported
    /// </summary> 
    public class ImportFilterService
    {
        public bool ShouldImport(ImportSong song) => song.Genre.ToLowerInvariant().Contains("metal");

        public bool ShouldImport(Artist artist) => true; // how to determine the starting year of a band?
    }
}