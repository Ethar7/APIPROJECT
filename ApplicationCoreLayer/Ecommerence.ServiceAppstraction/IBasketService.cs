using Ecommerence.Shared.DTOS.BasketDtos;

namespace Ecommerence.ServiceAppstraction
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string Key);
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto);

        Task<bool> DeleteBasketAsync(string Key);
    }
}