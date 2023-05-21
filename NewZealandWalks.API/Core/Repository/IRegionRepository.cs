using NewZealandWalks.API.Core.Entities;

namespace NewZealandWalks.API.Core.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();    //method for get all and wrap inside task for it to be async

        Task<Region?> GetByIdAsync(Guid id); // takes parameter of type Guid name is id,return task of type region which can be null or not null put it null

        Task<Region> CreateAsync(Region region);  //takes a type of domain model return task of type region

        Task<Region?> UpdateAsync(Guid id, Region region); //takes two paramaters which is the id of the resource we want to update and  actual region domain model.return task of type reqion and can be nullable

        Task<Region?> DeleteAsync(Guid id);  // takes id of type guid can be nullable 

    }
}
