// using ECommerence.Domain.Entities.IdentityModules;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;

// namespace Ecommerence.Persistence.Data.Identity
// {
//     public class storeIdentityDbContext(DbContextOptions<storeIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
//     {
//         protected override void OnModelCreating(ModelBuilder builder)
//         {
//             base.OnModelCreating(builder);
//             builder.Entity<Address>().ToTable("Addresses");
//             builder.Entity<ApplicationUser>().ToTable("Users");
//             builder.Entity<IdentityRole>().ToTable("Roles");
//             builder.Entity<IdentityUserRole<string>>().ToTable("UserRoless");

//             //-----------------------------------------------------

//             builder.Ignore<IdentityUserClaim<string>>();
//             builder.Ignore<IdentityUserToken<string>>();
//             builder.Ignore<IdentityUserLogin<string>>();
//             builder.Ignore<IdentityRoleClaim<string>>();
//         }
//     }
// }

using ECommerence.Domain.Entities.IdentityModules;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerence.Persistence.Data.Identity
{
    public class storeIdentityDbContext 
        : IdentityDbContext<ApplicationUser>
    {
        public storeIdentityDbContext(
            DbContextOptions<storeIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>().ToTable("Addresses");
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            // Ignore unused Identity tables
            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserToken<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();
        }
    }
}
