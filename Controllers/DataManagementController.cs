using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rock_app.Models;
using rock_app.Services;

namespace rock_app.Controllers
{
    [Authorize]
    [Route("api/manage")]
    public class DataManagementController : Controller
    {
        private readonly IDataImportService _dataImportService;

        public DataManagementController(IDataImportService dataSeedService)
        {
            _dataImportService = dataSeedService;
        }

        [HttpPost("artists")]
        public async Task<IActionResult> ImportArtists([FromBody] IEnumerable<Artist> artists) => Ok(await _dataImportService.ImportArtists(artists));

        [HttpPost("songs")]
        public async Task<IActionResult> ImportSongs([FromBody] IEnumerable<ImportSong> songs) => Ok(await _dataImportService.ImportSongs(songs));
    }
}
