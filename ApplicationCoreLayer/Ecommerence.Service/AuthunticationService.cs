using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.IdentityDTOS;
using ECommerence.Domain.Entities.IdentityModules;
using ECommerence.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace  Ecommerence.Service
{
    public class AuthunticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : IAuthunticationService
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
                    Token= await CreateTokenAsync(user)
                };
            }else throw new UnauthorizedException();
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            #region JWT Token Payload
            var claims = new List<Claim>()
            {
                new (ClaimTypes.Email, user.Email!),
                new (ClaimTypes.Name, user.UserName!),
                new (ClaimTypes.NameIdentifier, user.Id!)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            
            #endregion
            
            var secretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var creds= new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: _configuration.GetSection("JWTOptions")["Issuer"],
            audience: _configuration.GetSection("JWTOptions")["Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
                Token= await CreateTokenAsync(appUser)
            };
            else
            {
                var errors = res.Errors.Select(e=> e.Description).ToList();
                throw new BadRequestException(errors);
            }
            
        }
    }
}