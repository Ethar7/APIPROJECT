using System.ComponentModel.DataAnnotations.Schema;
using Ecommerence.Shared.DTOS.IdentityDTOS;

namespace Ecommerence.Shared.DTOS.OrderDTOS
{
    public class OrderToReturnDto
    {
        public Guid Id{get; set;}

        public string UserEmail {get; set;} = null!;

        public DateTimeOffset OrderDate {get; set;}

        public AddressDto Address {get; set;} = null!;

        public string DeliveryMethod {get; set;} = null!;

        public string OrderStatus {get; set;}

        public ICollection<OrderItemDto> Items {get; set;} = [];

        public decimal SubTotal {get; set;}


        public decimal Total {get; set;}
    }
}