using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RockApp.Models;

namespace RockApp
{
    public class RockAppContext : DbContext
    {
        public RockAppContext()
        { }

        public RockAppContext(DbContextOptions<RockAppContext> options)
            : base(options)
        { }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Album> Albums { get; set; }
    }
}