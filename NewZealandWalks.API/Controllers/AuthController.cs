using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Core.Repository;
using NewZealandWalks.API.Dtos.AuthDto;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        //post method to register user
        //POST:/api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Username

            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerDto.Password);

            if (identityResult.Succeeded)
            {
                //add roles to user 
                if (registerDto.Roles != null && registerDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User Was Registered Please Login");
                    }
                }

            }

            return BadRequest("Something Went  Wrong");
        }

        [HttpPost]
        [Route("Login")] //POST:api/Auth/Login

        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Username);
            if (user != null)
            {
                var checkpasswordresult = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if (checkpasswordresult)
                {

                    //get roles
                    var roles = await _userManager.GetRolesAsync(user);
                    //create token

                    var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());

                    //res
                    var res = new LoginResponseDto() { Token = jwtToken };

                    return Ok(res);
                }
            }
            return BadRequest("incorrect username ");

        }

    }
}
