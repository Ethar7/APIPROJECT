using Ecommerence.web.CustomMiddleWare.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Ecommerence.web.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection AddWebAppServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>((options)=>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
            });
            return services;
        }

    
    }
}