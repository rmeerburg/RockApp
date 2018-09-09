using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using RockApp.Models;
using RockApp.Services;

namespace RockApp.Controllers
{
    [Route("api/[controller]")]
    public class ArtistsController : EntityController
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [EnableQuery]
        public IQueryable<Artist> Get() => _artistService.GetAll().AsQueryable();

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var artist = await _artistService.GetAsync(id);
            if (artist == null)
                return NotFound();
            return Ok(artist);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Artist artist) => Ok(await _artistService.CreateAsync(artist));

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody]Artist artist) => Ok(await _artistService.SaveAsync(artist));

        [HttpDelete("{id}")]
        public async Task Delete(Guid id) => await _artistService.Delete(id);
    }
}
