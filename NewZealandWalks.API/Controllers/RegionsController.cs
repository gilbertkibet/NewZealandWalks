using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Dtos.RegionsDtos;
using NewZealandWalks.API.Helpers;
using System.Text.Json;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper,ILogger <RegionsController> logger)
        {

            _regionRepository = regionRepository;

            _mapper = mapper;
            _logger = logger;
        }
        //GET:https://localhost:portnumber/api/regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("The get all action method was invoked");
            //get data from database
            //map the domain models to display dtos
            //in controller level it is destination then source
            //in mapping profile it is source to destination
            //return dtos back to the client
            var regions = await _regionRepository.GetAllAsync();

            _logger.LogInformation($"Finished get all get all with {JsonSerializer.Serialize(regions)}");

            return Ok(_mapper.Map<List<RegionToDisplayDto>>(regions));

        }
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionToDisplayDto>(region));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        //POST:https://localhost:portnumber/api/regions

        public async Task<IActionResult> Create([FromBody] RegionToCreateDto regionToCreateDto)
        {

            //map convert dto to domain model

            var regionDomainModel = _mapper.Map<Region>(regionToCreateDto);

            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);
            //map domain to regiontodisplay dto
            var regionToDisplay = _mapper.Map<RegionToDisplayDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionToDisplay);


        }

        [HttpPut]   //PUT:https://localhost:portnumber/api/region/{id}
        [ValidateModel]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RegionToUpdateDto regionToUpdateDto)
        {
            //map dto to domain model
            var regionDomainModel = _mapper.Map<Region>(regionToUpdateDto);

            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionToDisplayDto>(regionDomainModel));


        }


        [HttpDelete]  //DELETE:https:portnumber/api/region
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute,] Guid id)
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
