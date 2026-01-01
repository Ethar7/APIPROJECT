using Ecommerence.Shared.DTOS.OrderDTOS;

namespace Ecommerence.ServiceAppstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string email);

        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();

        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string emai);

        Task<OrderToReturnDto> GetOrderAsync(Guid id);


    }
}