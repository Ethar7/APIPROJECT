// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }


// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.OpenApi.Models;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
// });

// var app = builder.Build();

// // Swagger middleware
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// // علق HTTPS مؤقتًا
// // app.UseHttpsRedirection();

// // Minimal API endpoint
// app.MapGet("/", () => "Hello World!");

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast(
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         )).ToArray();
//     return forecast;
// });

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }

// using Microsoft.OpenApi.Models;
// using Microsoft.EntityFrameworkCore;
// using Ecommerence.Persistence.Data.DbContexts;
// using Microsoft.Extensions.DependencyInjection;
// using ECommerence.Domain.Contracts;
// using Ecommerence.Persistence.Data.DataSeed;
// using Ecommerence.web.Extensions;


// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();


// builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddDbContext<StoreDbContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
// });
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
// });

// builder.Services.AddScoped<IDataInitializer, DataInitializer>();

// var app = builder.Build();

// #region DataSeed

// app.MigrateDb();
// app.SeedDb();
// #endregion

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }


// app.MapControllers();


// app.MapGet("/", () => "Hello World!");

// // WeatherForecast endpoint
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm",
//     "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast(
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         )).ToArray();
//     return forecast;
// });

// app.Run();

// // WeatherForecast record
// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }



using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Ecommerence.Persistence.Data.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using ECommerence.Domain.Contracts;
using Ecommerence.Persistence.Data.DataSeed;
using Ecommerence.web.Extensions;
using Ecommerence.Service.MappingProfiles;
using Ecommerence.Persistence.Repositories;
using Ecommerence.ServiceAppstraction;
using Ecommerence.Service;
using System.Globalization;
using Ecommerence.web.CustomMiddleWare;
using Microsoft.AspNetCore.Mvc;
using Ecommerence.Shared.ErrorModule;
using Ecommerence.web.CustomMiddleWare.Factories;
using Ecommerence.Persistence;
using ECommerence.Domain.Entities.IdentityModules;
using Microsoft.AspNetCore.Identity;
using Ecommerence.Persistence.Data.Identity;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
// Add services
builder.Services.AddControllers();

builder.Services.AddSwaggerServices();


builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddWebAppServices();

builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// builder.Services.AddScoped<IBasketRepository, BasketRepository>();
// builder.Services.AddScoped<IBasketService, BasketService>();

// builder.Services.AddAutoMapper(typeof(BasketProfile));



var app = builder.Build();

#region Auto Migration + Seeding
app.MigrateDbAsync();   // Auto apply migrations
app.SeedDbAsync();      // Seed data
#endregion


#region Configre the HTTP request pipeline

// app.Use(async (context, next) =>
// {
//     var cultureQuery = context.Request.Query["culture"];
//     if (!string.IsNullOrWhiteSpace(cultureQuery))
//     {
//         var culture = new CultureInfo(cultureQuery);

//         CultureInfo.CurrentCulture = culture;
//         CultureInfo.CurrentUICulture = culture;
//     }

//     await next(context);
// });



app.UseCustomExceptionMiddleWare();

#endregion
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Complete MVC pipeline
app.UseRouting();
// OPTIONAL if you have it later
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

// Test endpoint
app.MapGet("/", () => "Hello World!");

// WeatherForecast
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm",
    "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )).ToArray();
    return forecast;
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
