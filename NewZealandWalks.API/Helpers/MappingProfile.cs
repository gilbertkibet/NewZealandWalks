using AutoMapper;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Dtos.RegionsDtos;

namespace NewZealandWalks.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Region, RegionToDisplayDto>().ReverseMap();//for get
            CreateMap<RegionToCreateDto, Region>().ReverseMap();//for post
            CreateMap<RegionToUpdateDto, Region>().ReverseMap();//for put
        }
    }
}
