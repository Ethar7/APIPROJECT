using System.Text;
using Ecommerence.web.CustomMiddleWare.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Ecommerce API Project", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In=ParameterLocation.Header,
                    Name="Authorization",
                    Type=SecuritySchemeType.ApiKey,
                    Scheme="Bearer",
                    Description="Enter 'Bearer' Followed By Space and Then Enter Your Token"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type=ReferenceType.SecurityScheme
                            }
                        }
                        ,
                        new string[]{}
                    }
                });
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

         

        public static IServiceCollection AddJWTServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetSection("JWTOptions")["Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration.GetSection("JWTOptions")["Audience"],

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            configuration.GetSection("JWTOptions")["SecretKey"]!
                        )
                    )
                };
            });

            return services;
        }


    
    }
}