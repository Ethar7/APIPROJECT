using System.Security.Claims;
using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.IdentityDTOS;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("checkemail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await _serviceManager.AuthunticationService.CheckEmailAsync(email);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var users = await _serviceManager.AuthunticationService.GetCurrentUserAsync(email!);
            return Ok(users);
        }

        [Authorize]
        [HttpGet("address")]

        public async Task<ActionResult<AddressDto>> GetCurrentAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var address = await _serviceManager.AuthunticationService.GetCurrentUserAddressAsync(email!);
            return Ok(address);
        }

        [Authorize]
        [HttpPut("address")]

        public async Task<ActionResult<AddressDto>> UpdateCurrentAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var address = await _serviceManager.AuthunticationService.UpdateCurrentUserAddressAsync(email!, addressDto);
            return Ok(address);
        }
    }
}