using NewZealandWalks.API.Core.Entities;

namespace NewZealandWalks.API.Core.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateRegionAsync(Region region);

        Task<Region?> UpdateRegionAsync(Guid id, Region region);

        Task<Region?> DeleteAsync(Guid id);

    }
}
