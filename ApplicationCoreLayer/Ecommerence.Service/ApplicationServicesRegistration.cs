using Ecommerence.Service.MappingProfiles;
using Ecommerence.ServiceAppstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerence.Service
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
           services.AddAutoMapper(X => X.AddProfile<ProductProfile>());
           services.AddScoped<IProductServices , ProductService>();
           return services;
        }
    }
}