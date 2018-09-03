using System;
using Microsoft.EntityFrameworkCore;
using rock_app.Models;

namespace rock_app
{
    public class RockAppContext : DbContext
    {
        public RockAppContext(DbContextOptions<RockAppContext> options)
            : base(options)
        {
        }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Album> Albums { get; set; }
    }
}