﻿using Microsoft.EntityFrameworkCore;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Infrastructure.Data;

namespace NewZealandWalks.API.Infrastructure.Implementations
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NewZealandWalksDbContext _context;

        public RegionRepository(NewZealandWalksDbContext context)
        {
            _context = context;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _context.Regions.AddAsync(region);

            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (existingRegion == null) { return null; }

            _context.Regions.Remove(existingRegion);

            await _context.SaveChangesAsync();

            return existingRegion;

        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await _context.SaveChangesAsync();

            return existingRegion;
        }
    }
}
