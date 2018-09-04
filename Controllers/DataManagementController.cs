using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rock_app.Models;
using rock_app.Services;

namespace rock_app.Controllers
{
    [Route("api/manage")]
    public class DataManagementController : Controller
    {
        private readonly IDataSeedService _dataSeedService;

        public DataManagementController(IDataSeedService dataSeedService)
        {
            _dataSeedService = dataSeedService;
        }

        [HttpPost("artists")]
        public Task ImportArtists([FromBody] IEnumerable<Artist> artists) => _dataSeedService.ImportArtists(artists);

        [HttpPost("songs")]
        public Task ImportSongs([FromBody] IEnumerable<ImportSong> songs) => _dataSeedService.ImportSongs(songs);
    }
}
