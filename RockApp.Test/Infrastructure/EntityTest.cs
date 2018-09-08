using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RockApp;
using RockApp.Models;
using RockApp.Services;
using Xunit;

namespace RockApp.Test.Infrastructure
{
    public class EntityTest
    {
        protected RockAppContext CreateTestDbContext(string name = null) => new RockAppContext(new DbContextOptionsBuilder<RockAppContext>()
                .UseInMemoryDatabase(databaseName: name ?? Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options);
    }
}
