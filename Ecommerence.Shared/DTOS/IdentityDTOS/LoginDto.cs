using System.ComponentModel.DataAnnotations;

namespace Ecommerence.Shared.DTOS.IdentityDTOS
{
    public class LoginDto
    {
        [EmailAddress]

        public string Email {get; set;} = null!;
        public string Password {get; set;} =  null!;
    }
}