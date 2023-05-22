﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Dtos.RegionsDtos;
using NewZealandWalks.API.Helpers;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;

        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {

            _regionRepository = regionRepository;

            _mapper = mapper;
        }
        //GET:https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //get data from database
            //map the domain models to display dtos
            //in controller level it is destination then source
            //in mapping profile it is source to destination
            //return dtos back to the client
            var regions = await _regionRepository.GetAllAsync();

            return Ok(_mapper.Map<List<RegionToDisplayDto>>(regions));

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

            return Ok(_mapper.Map<RegionToDisplayDto>(region));
        }

        [HttpPost]
        [ValidateModel]
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
