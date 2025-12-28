using Ecommerence.Shared.DTOS.IdentityDTOS;

namespace Ecommerence.ServiceAppstraction
{
    public interface IAuthunticationService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
    }
}