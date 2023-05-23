using AutoMapper;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Dtos.Difficulty;
using NewZealandWalks.API.Dtos.RegionsDtos;
using NewZealandWalks.API.Dtos.WalksDtos;

namespace NewZealandWalks.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Region, RegionToDisplayDto>().ReverseMap();//for get method source is db and  dest is display dto
            CreateMap<RegionToCreateDto, Region>().ReverseMap();//for post source to destination
            CreateMap<RegionToUpdateDto, Region>().ReverseMap();//for put

            //walks

            CreateMap<AddWalksDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkToDisplayDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyToDisplayDto>().ReverseMap();
            CreateMap<UpdateWalkDto, Walk>().ReverseMap();
        }
    }
}
