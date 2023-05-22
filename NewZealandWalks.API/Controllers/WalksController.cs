﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Dtos.WalksDtos;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;

            _walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalksDto addWalksDto)
        {
            //map dto to domain
            var walksDomain = _mapper.Map<Walk>(addWalksDto);
            //add to the database using repository
            await _walkRepository.CreateAsync(walksDomain);
            //map domain model to dto
            var walkToDisplay = _mapper.Map<WalkToDisplayDto>(walksDomain);

            return Ok(walkToDisplay);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomain = await _walkRepository.GetAllAsync();

            //map

            return Ok(_mapper.Map<List<WalkToDisplayDto>>(walksDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            var walkDomain = await _walkRepository.GetByIdAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkToDisplayDto>(walkDomain));

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkDto updateWalkDto)
        {
            //map dto to domain model  

            var walkDomain = _mapper.Map<Walk>(updateWalkDto);

            walkDomain = await _walkRepository.UpdateAsync(id, walkDomain);

            if (walkDomain == null)
            {
                return NotFound();
            }
            //map domain todto

            return Ok(_mapper.Map<WalkToDisplayDto>(walkDomain));

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalk = await _walkRepository.DeleteAsync(id);

            if (deletedWalk == null)
            {
                return NotFound();

            }

            return Ok(_mapper.Map<WalkToDisplayDto>(deletedWalk));

        }
    }
}
