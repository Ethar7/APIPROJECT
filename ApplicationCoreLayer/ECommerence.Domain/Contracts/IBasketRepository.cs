using ECommerence.Domain.Entities;

namespace ECommerence.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string Key);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive=null);

        Task<bool> DeleteBasketAsync(string id);

    }
}