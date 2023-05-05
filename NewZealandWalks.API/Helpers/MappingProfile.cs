using AutoMapper;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Dtos;

namespace NewZealandWalks.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Region, RegionToDisplayDto>().ReverseMap();
            CreateMap<RegionToCreateDto, Region>().ReverseMap();
            CreateMap<RegionToUpdateDto, Region>().ReverseMap();
        }
    }
}
