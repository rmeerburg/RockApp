using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rock_app.Models;
using rock_app.Services;

namespace rock_app.Controllers
{
    [Route("api/[controller]")]
    public class ArtistsController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<IEnumerable<Artist>> Get() => await _artistService.GetArtistsAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var artist = await _artistService.GetArtistAsync(id);
            if (artist == null)
                return NotFound();
            return Ok(artist);
        } 

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Artist artist) => Ok(await _artistService.CreateArtistAsync(artist));

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Artist artist) => await _artistService.SaveArtistAsync(artist);

        [HttpDelete("{id}")]
        public async Task Delete(Guid id) => await _artistService.DeleteArtist(id);
    }
}
