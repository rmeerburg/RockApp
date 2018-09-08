using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RockApp;
using RockApp.Models;
using RockApp.Services;
using RockApp.Test.Infrastructure;
using Xunit;

namespace RockApp.Test
{
    public class ArtistServiceTest : EntityTest
    {
        private ArtistService CreateSystemUnderTest(RockAppContext context) => new ArtistService(context);
        private readonly Guid _existingArtistId = new Guid("12676815-0300-46ef-8153-900a12c3528a");

        private RockAppContext CreateContextWithData()
        {
            var contextId = Guid.NewGuid().ToString();
            var context = CreateTestDbContext(contextId);
            context.Artists.Add(new Artist { Name = "TestArtist", ArtistId = _existingArtistId, });
            context.Albums.Add(new Album { ArtistId = _existingArtistId, Name = "TestAlbum" });

            context.SaveChanges();
            return CreateTestDbContext(contextId);
        }

        [Fact]
        public async Task ArtistServiceCanCreateNewArtist()
        {
            using (var context = CreateTestDbContext())
            {
                var service = CreateSystemUnderTest(context);
                var newArtist = new Artist { Name = "foo" };
                var savedArtist = await service.CreateAsync(newArtist);

                Assert.NotNull(service.GetAsync(savedArtist.ArtistId));
            }
        }

        [Fact]
        public async Task ArtistServiceCanRetrieveArtists()
        {
            using (var context = CreateContextWithData())
            {
                var service = CreateSystemUnderTest(context);
                var retrievedArtist = await service.GetAsync(_existingArtistId);

                Assert.NotNull(retrievedArtist);
                Assert.Equal("TestArtist", retrievedArtist.Name);
                Assert.NotEmpty(retrievedArtist.Albums);
                Assert.Equal("TestAlbum", retrievedArtist.Albums.First().Name);
            }
        }

        [Fact]
        public async Task ArtistServiceCanDeleteArtistById()
        {
            using (var context = CreateContextWithData())
            {
                var service = CreateSystemUnderTest(context);
                await service.Delete(_existingArtistId);

                Assert.Null(await service.GetAsync(_existingArtistId));
            }
        }
    }
}
