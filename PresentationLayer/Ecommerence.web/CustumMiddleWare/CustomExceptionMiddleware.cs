using System.Net;
using System.Text.Json;
using Ecommerence.Shared.ErrorModule;

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
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "SomethingWentWrong");
                httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

                httpContext.Response.ContentType="application/json";

                var response = new ErrorToReturn()
                {
                    StatusCode= StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };

                // var responseToReturn = JsonSerializer.Serialize(response);

                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}