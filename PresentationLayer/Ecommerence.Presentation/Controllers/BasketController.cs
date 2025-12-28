using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.BasketDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerence.Presentation.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]

    public class BasketController(IServiceManager _serviceManager) : ControllerBase

    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdate(BasketDto basket)
        {
            var result = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var res = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(res);
        }
    }
}