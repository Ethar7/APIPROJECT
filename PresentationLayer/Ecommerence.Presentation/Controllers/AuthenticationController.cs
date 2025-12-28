using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.IdentityDTOS;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerence.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
           var user = await _serviceManager.AuthunticationService.LoginAsync(loginDto);
           return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
           var user = await _serviceManager.AuthunticationService.RegisterAsync(registerDto);
           return Ok(user);
        }
    }
}