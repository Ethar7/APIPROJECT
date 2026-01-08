namespace ECommerence.Domain.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; } 
        public List<BasketItem> Items{get; set;} = new();

        public decimal ShippingPrice { get; set; } = 0; 
        public int DeliveryMethodId { get; set; } = 0;
    }
}