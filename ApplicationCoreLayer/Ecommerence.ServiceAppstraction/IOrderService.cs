using Ecommerence.Shared.DTOS.OrderDTOS;

namespace Ecommerence.ServiceAppstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string email);

        
    }
}