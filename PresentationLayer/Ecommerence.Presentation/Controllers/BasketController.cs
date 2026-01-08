using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.BasketDtos;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerence.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        // Constructor injection بالشكل التقليدي
        public BasketController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdate(BasketDto basket)
        {
            var result = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var res = await _serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(res);
        }
    }
}
