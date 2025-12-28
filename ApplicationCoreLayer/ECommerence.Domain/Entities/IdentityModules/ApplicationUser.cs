using Microsoft.AspNetCore.Identity;
namespace ECommerence.Domain.Entities.IdentityModules
{
    public class ApplicationUser: IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address {get; set;}
    }
}