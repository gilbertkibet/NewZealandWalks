using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Dtos;
using NewZealandWalks.API.Infrastructure.Data;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NewZealandWalksDbContext _context;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(NewZealandWalksDbContext context, IRegionRepository regionRepository, IMapper mapper)
        {
            _context = context;

            _regionRepository = regionRepository;

            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllAsync();

            var regionsToDisplay = _mapper.Map<List<RegionToDisplayDto>>(regions);

            return Ok(regionsToDisplay);

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionToDisplay = _mapper.Map<RegionToDisplayDto>(region);

            return Ok(regionToDisplay);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] RegionToCreateDto regionToCreateDto)
        {
            var regionDomainModel = _mapper.Map<Region>(regionToCreateDto);


            regionDomainModel = await _regionRepository.CreateRegionAsync(regionDomainModel);

            var regionToDisplay = _mapper.Map<RegionToDisplayDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionToDisplay);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] RegionToUpdateDto regionToUpdateDto)
        {
            //map dto to domain model
            var regionDomainModel = _mapper.Map<Region>(regionToUpdateDto);

            regionDomainModel = await _regionRepository.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionToDisplay = _mapper.Map<RegionToDisplayDto>(regionDomainModel);

            return Ok(regionToDisplay);

        }


        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteRegion([FromRoute,] Guid id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionToDisplayDto>(regionDomainModel));
        }
    }
}
