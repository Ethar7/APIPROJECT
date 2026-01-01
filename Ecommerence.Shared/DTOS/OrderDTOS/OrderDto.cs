using Ecommerence.Shared.DTOS.IdentityDTOS;

namespace Ecommerence.Shared.DTOS.OrderDTOS
{
    public class OrderDto
    {
        public string BasketId {get; set;}

        public int DeliveryMethodId {get; set;}

        public AddressDto Address{get; set;}
    }
}