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
           services.AddScoped<IServiceManager , ServiceManagerWithFactoryDelegate>();



           services.AddScoped<IProductServices, ProductService>();
           services.AddScoped<Func<IProductServices>>(provider =>
                    ()=> provider.GetRequiredService<IProductServices>());


           services.AddScoped<IOrderService, OrderService>();
           services.AddScoped<Func<IOrderService>>(provider =>
                    ()=> provider.GetRequiredService<IOrderService>());

            
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<Func<IBasketService>>(provider =>
                    ()=> provider.GetRequiredService<IBasketService>());

            
            services.AddScoped<IAuthunticationService, AuthunticationService>();
            services.AddScoped<Func<IAuthunticationService>>(provider =>
         
                    ()=> provider.GetRequiredService<IAuthunticationService>());
           
           return services;
        }
    }
}