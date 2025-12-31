using System.Net;
using Ecommerence.Shared.DTOS.IdentityDTOS;

namespace Ecommerence.ServiceAppstraction
{
    public interface IAuthunticationService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        Task <bool> CheckEmailAsync(string email);

        Task<AddressDto> GetCurrentUserAddressAsync(string email);

        Task<UserDto> GetCurrentUserAsync(string email);

        Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto);
    }
}