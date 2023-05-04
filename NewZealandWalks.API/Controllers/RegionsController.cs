using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Dtos;
using NewZealandWalks.API.Infrastructure.Data;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NewZealandWalksDbContext _context;

        public RegionsController(NewZealandWalksDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllRegions()
        {

            var regions = _context.Regions.ToList();

            var regionsToDisplay = new List<RegionToDisplayDto>();

            foreach (var region in regions)
            {
                regionsToDisplay.Add(new RegionToDisplayDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }


            return Ok(regionsToDisplay);

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = _context.Regions.Find(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionToDisplay = new RegionToDisplayDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };

            return Ok(regionToDisplay);
        }

        [HttpPost]
        public IActionResult CreateRegion([FromBody] RegionToCreateDto regionToCreateDto)
        {
            var regionDomainModel = new Region()
            {
                Code = regionToCreateDto.Code,
                Name = regionToCreateDto.Name,
                RegionImageUrl = regionToCreateDto.RegionImageUrl,

            };

            _context.Regions.Add(regionDomainModel);

            _context.SaveChanges();

            var regionToDisplay = new RegionToDisplayDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionToDisplay);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] RegionToUpdateDto regionToUpdateDto)
        {
            var existingRegion = _context.Regions.Find(id);

            if (existingRegion == null)
            {
                return NotFound();
            }

            existingRegion.Code = regionToUpdateDto.Code;
            existingRegion.Name = regionToUpdateDto.Name;
            existingRegion.RegionImageUrl = regionToUpdateDto.RegionImageUrl;

            _context.SaveChanges();

            var regionToDisplay = new RegionToDisplayDto()
            {
                Id = existingRegion.Id,
                Code = existingRegion.Code,
                Name = existingRegion.Name,
                RegionImageUrl = existingRegion.RegionImageUrl

            };
            return Ok(regionToDisplay);

        }


        [HttpDelete]
        [Route("{id:Guid}")]

        public IActionResult DeleteRegion([FromRoute,] Guid id)
        {
            var regionToDelete = _context.Regions.Find(id);

            if (regionToDelete == null)
            {
                return NotFound();
            }

            _context.Regions.Remove(regionToDelete);


            return Ok();
        }
    }
}
