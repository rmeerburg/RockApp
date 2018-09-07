using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using rock_app.Models;
using rock_app.Services;

namespace rock_app.Controllers
{
    [Route("api/[controller]")]
    public class SongsController : Controller
    {
        private readonly ISongService _songService;

        public SongsController(ISongService songService) => _songService = songService;

        [EnableQuery]
        public async Task<IQueryable<Song>> Get() => (await _songService.GetSongsAsync()).AsQueryable();

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var Song = await _songService.GetSongAsync(id);
            if (Song == null)
                return NotFound();
            return Ok(Song);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Song Song) => Ok(await _songService.CreateSongAsync(Song));

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]Song Song) => Ok(await _songService.SaveSongAsync(Song));

        [HttpDelete("{id}")]
        public async Task Delete(Guid id) => await _songService.DeleteSong(id);
    }
}
