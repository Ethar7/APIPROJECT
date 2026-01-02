

using Ecommerence.web.Extensions;

using Ecommerence.ServiceAppstraction;
using Ecommerence.Service;

using Ecommerence.Persistence;

using Microsoft.AspNetCore.Authentication;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);
// Add services
builder.Services.AddControllers();

builder.Services.AddSwaggerServices();


builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddWebAppServices();
builder.Services.AddJWTServices(builder.Configuration);

// builder.Services.AddScoped<IServiceManager, ServiceManager>();

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
    app.UseSwaggerUI(options=>
    {
        options.ConfigObject = new Swashbuckle.AspNetCore.SwaggerUI.ConfigObject()
        {
            DisplayRequestDuration = true
        };
        // options.DocumentTitle="My Ecommerce API Project";
        options.DocExpansion(DocExpansion.None);
        options.EnableFilter();
        options.EnablePersistAuthorization();
    });
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
app.UseAuthentication();   
app.UseAuthorization(); 

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
