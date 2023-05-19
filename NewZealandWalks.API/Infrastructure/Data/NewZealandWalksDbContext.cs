using Microsoft.EntityFrameworkCore;
using NewZealandWalks.API.Core.Entities;

namespace NewZealandWalks.API.Infrastructure.Data
{
    public class NewZealandWalksDbContext : DbContext
    {
        public NewZealandWalksDbContext(DbContextOptions<NewZealandWalksDbContext> options) : base(options)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
