using System.Text;
using Ecommerence.ServiceAppstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerence.Presentation.Attributes
{
    public class CacheAttribute(int durationInSec = 100) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheKey = CreateCacheKey(context.HttpContext.Request);

            var cacheService = context.HttpContext.RequestServices
            .GetRequiredService<ICacheService>();

            var cacheValue = await cacheService.GetAsync(cacheKey);

            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            var executedContext = await next.Invoke();
            if (executedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(cacheKey, result.Value, TimeSpan.FromSeconds(durationInSec));
            }
            
        }
        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path+"?");
            foreach (var item in request.Query.OrderBy(q=>q.Key))
            {
                Key.Append($"{item.Key}={item.Equals}&");
            }
            return Key.ToString();
        }
    }
}