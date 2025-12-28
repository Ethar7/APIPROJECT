using AutoMapper;
using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.IdentityDTOS;
using ECommerence.Domain.Entities.IdentityModules;
using ECommerence.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace  Ecommerence.Service
{
    public class AuthunticationService(UserManager<ApplicationUser> _userManager) : IAuthunticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);
            var isPassValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (isPassValid)
            {
                return new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token= "TODO"
                };
            }else throw new UnauthorizedException();
        }
        private static string CreateTokenAsync()
        {
            return "TODO";
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var appUser=new ApplicationUser()
            {
                DisplayName=registerDto.DisplayName,
                Email=registerDto.Email,
                PhoneNumber=registerDto.PhoneNumber,
                UserName=registerDto.UserName
            };
            var res = await _userManager.CreateAsync(appUser, registerDto.Password);
        
            if (res.Succeeded) return new UserDto()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                Token= CreateTokenAsync()
            };
            else
            {
                var errors = res.Errors.Select(e=> e.Description).ToList();
                throw new BadRequestException(errors);
            }
            
        }
    }
}