using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
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

        [EnableQuery]
        public async Task<IQueryable<Artist>> Get() => (await _artistService.GetArtistsAsync()).AsQueryable();

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
        public async Task<ActionResult> Put(int id, [FromBody]Artist artist) => Ok(await _artistService.SaveArtistAsync(artist));

        [HttpDelete("{id}")]
        public async Task Delete(Guid id) => await _artistService.DeleteArtist(id);
    }
}
