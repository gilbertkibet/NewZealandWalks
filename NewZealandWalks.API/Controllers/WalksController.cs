using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Core.Entities;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Dtos.WalksDtos;
using NewZealandWalks.API.Helpers;

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
        [ValidateModel]
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

        [HttpGet] //\?FilterOn=Name&&filterQuery=Track&pageNumber=1&pageSize=10
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var walksDomain = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);


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
        [ValidateModel]
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
