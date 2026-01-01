using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.OrderDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerence.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController(IServiceManager _serviceManager) : APIBaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
           var order = await _serviceManager.orderService.CreateOrder(orderDto, GetEmailFromToken());

           return Ok(order);
        }
    }
}