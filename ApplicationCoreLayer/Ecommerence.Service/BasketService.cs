using AutoMapper;
using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.BasketDtos;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities;
using ECommerence.Domain.Exceptions;

namespace Ecommerence.Service
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basketDto);
            var createOrUpdateBasket =  await  _basketRepository.CreateOrUpdateBasketAsync(customerBasket);


            if (createOrUpdateBasket is not null) return await GetBasketAsync(basketDto.Id);

            else throw new Exception("Cant Create Or Update Basket Now , Try again Later");
        }

        public Task<bool> DeleteBasketAsync(string Key)
        {
            return _basketRepository.DeleteBasketAsync(Key);
        }

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var basket = await _basketRepository.GetBasketAsync(key);

            if (basket is null)
                throw new BasketNotFoundException(key);

            return _mapper.Map<BasketDto>(basket);
        }

    }
}