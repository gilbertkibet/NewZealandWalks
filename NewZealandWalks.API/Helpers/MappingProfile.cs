using AutoMapper;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Dtos.RegionsDtos;

namespace NewZealandWalks.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Region, RegionToDisplayDto>().ReverseMap();//for get method source is db and  dest is display dto
            CreateMap<RegionToCreateDto, Region>().ReverseMap();//for post source to destination
            CreateMap<RegionToUpdateDto, Region>().ReverseMap();//for put
        }
    }
}
