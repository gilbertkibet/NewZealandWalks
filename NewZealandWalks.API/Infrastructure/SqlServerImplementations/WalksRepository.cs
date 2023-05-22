using Microsoft.EntityFrameworkCore;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Infrastructure.Data;

namespace NewZealandWalks.API.Infrastructure.SqlServerImplementations
{


    public class WalksRepository : IWalkRepository
    {
        private readonly NewZealandWalksDbContext _context;

        public WalksRepository(NewZealandWalksDbContext context)
        {
            _context = context;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _context.Walks.AddAsync(walk);

            await _context.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkToDelete = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walkToDelete == null)
            {
                return null;
            }

            _context.Walks.Remove(walkToDelete);

            await _context.SaveChangesAsync();

            return walkToDelete;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _context.Walks.Include("Difficulty").Include("Region").ToListAsync();
            //  return await _context.Walks.Include(x=>x.Difficulty).Include(x=>x.Region).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var walkDomain = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walkDomain == null)
            {
                return null;
            }

            //update exising walk domain
            walkDomain.Name = walk.Name;
            walkDomain.Description = walk.Description;
            walkDomain.LengthInKm = walk.LengthInKm;
            walkDomain.WalkImageUrl = walk.WalkImageUrl;
            walkDomain.DifficultyId = walk.DifficultyId;
            walkDomain.RegionId = walk.RegionId;

            await _context.SaveChangesAsync();

            return walkDomain;
        }
    }
}
