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
    public class SongServiceTest : EntityTest
    {
        private SongService CreateSystemUnderTest(RockAppContext context) => new SongService(context);

        private readonly Guid _existingArtistId = new Guid("afba7d84-9e0b-4924-9898-e61e9fb99242");
        private readonly Guid _existingSongId = new Guid("774c326f-1e29-4385-bc0f-b3646a5d47e0");
        private readonly Guid _existingSongId2 = new Guid("00f72b8a-aea2-4fab-bf07-709940b129fa");
        private readonly Guid _existingGenreId = new Guid("075e9e02-b645-4cf8-8e38-80884027a41f");
        private readonly Guid _existingAlbumId = new Guid("b0da5cc4-de8c-47a8-a19a-eb4db35a9cba");

        private RockAppContext CreateContextWithData()
        {
            var contextId = Guid.NewGuid().ToString();
            var context = CreateTestDbContext(contextId);
            context.Artists.Add(new Artist { Name = "TestArtist", ArtistId = _existingArtistId, });
            context.Songs.Add(new Song { SongId = _existingSongId, Name = "TestSong", ArtistId = _existingArtistId, AlbumId = _existingAlbumId, GenreId = _existingGenreId, Shortname = "ts", Bpm = 120, Duration = 180, Year = 2000, });
            context.Songs.Add(new Song { SongId = _existingSongId2, Name = "TestSong2", ArtistId = _existingArtistId, AlbumId = _existingAlbumId, GenreId = _existingGenreId, Shortname = "ts2", Bpm = 120, Duration = 180, Year = 2000, });
            context.Albums.Add(new Album { ArtistId = _existingSongId, Name = "TestAlbum" });
            context.Genres.Add(new Genre { GenreId = _existingGenreId, Name = "TestGenre" });

            context.SaveChanges();
            return CreateTestDbContext(contextId);
        }
        
        [Fact]
        public async Task SongServiceCanCreateNewSong()
        {
            using (var context = CreateTestDbContext())
            {
                var service = CreateSystemUnderTest(context);
                var newSong = new Song { Name = "foo" };
                var savedSong = await service.CreateAsync(newSong);

                Assert.NotNull(service.GetAsync(savedSong.SongId));
            }
        }

        [Fact]
        public void SongServiceCanGetAllSongs()
        {
            using (var context = CreateContextWithData())
            {
                var service = CreateSystemUnderTest(context);
                var songs = service.GetAll();
                Assert.NotEmpty(songs);
            }
        }

        [Fact]
        public async Task SongServiceCanGetSpecificSong()
        {
            using (var context = CreateContextWithData())
            {
                var service = CreateSystemUnderTest(context);
                var song = await service.GetAsync(_existingSongId);
                var songEntry = context.Entry(song);

                Assert.NotNull(song);
                Assert.Equal("TestSong", song.Name);
                Assert.Equal("ts", song.Shortname);
                Assert.Equal(120, song.Bpm);
                Assert.Equal(180, song.Duration);
                Assert.Equal(2000, song.Year);

                // these navigation properties are not loaded by default
                Assert.Null(song.Album);
                Assert.Null(song.Artist);
                Assert.Null(song.Genre);
            }
        }

        [Fact]
        public async Task SongServiceAcceptsNewSongWithDuplicateName()
        {
            using (var context = CreateContextWithData())
            {
                var service = CreateSystemUnderTest(context);
                var newSong = new Song { Name = "TestSong" };
                await service.CreateAsync(newSong);
            }
        }

        [Fact]
        public async Task SongServiceCanChangeSong()
        {
            using (var context = CreateContextWithData())
            {
                var service = CreateSystemUnderTest(context);
                var existingSong = await service.GetAsync(_existingSongId);
                existingSong.Bpm = 100;
                var savedSong = await service.SaveAsync(existingSong);
                
                Assert.Equal(existingSong.Bpm, savedSong.Bpm);
                var fetchedSong = await service.GetAsync(_existingSongId);
                Assert.Equal(existingSong.Bpm, fetchedSong.Bpm);
            }
        }

        [Fact]
        public async Task SongServiceCanDeleteSongByReference()
        {
            using (var context = CreateContextWithData())
            {
                var service = CreateSystemUnderTest(context);
                var existingSong = await service.GetAsync(_existingSongId);
                await service.Delete(existingSong);

                Assert.Null(await service.GetAsync(existingSong.SongId));
            }
        }

        [Fact]
        public async Task SongServiceCanDeleteSongById()
        {
            using (var context = CreateContextWithData())
            {
                var service = CreateSystemUnderTest(context);
                await service.Delete(_existingSongId2);

                Assert.Null(await service.GetAsync(_existingSongId2));
            }
        }
    }
}
