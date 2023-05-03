using Microsoft.AspNetCore.Mvc;
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

            return Ok(regions);

        }
    }
}
