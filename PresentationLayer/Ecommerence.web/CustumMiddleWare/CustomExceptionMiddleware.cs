using System.Net;
using System.Text.Json;
using Ecommerence.Shared.ErrorModule;
using ECommerence.Domain.Exceptions;

namespace Ecommerence.web.CustomMiddleWare
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate Next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleNotFoundEndpointAsync(httpContext);

                
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "SomethingWentWrong");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message
            };

            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badReqEx=> GetBadRequestErrors(response, badReqEx),
                _ => StatusCodes.Status500InternalServerError
            };

            // httpContext.Response.ContentType="application/json";


            // var responseToReturn = JsonSerializer.Serialize(response);

            await httpContext.Response.WriteAsJsonAsync(response);
        }


        private static async Task HandleNotFoundEndpointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"EndPoint {httpContext.Request.Path} Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
        private static int GetBadRequestErrors(ErrorToReturn response, BadRequestException exception)
        {
           response.Errors= exception.Errors;
           return StatusCodes.Status400BadRequest;
        }
    }
}