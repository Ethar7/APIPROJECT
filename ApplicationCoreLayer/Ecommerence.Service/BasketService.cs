using AutoMapper;
using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.BasketDtos;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities;
using ECommerence.Domain.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerence.Service
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        // ===============================
        // Create or Update Basket
        // ===============================
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto)
        {
            if (basketDto == null) throw new ArgumentNullException(nameof(basketDto));

            // Map DTO to domain entity
            var customerBasket = _mapper.Map<CustomerBasket>(basketDto);

            // Create or update basket in repository
            var result = await _basketRepository.CreateOrUpdateBasketAsync(customerBasket);

            if (result != null)
                return await GetBasketAsync(basketDto.Id);

            throw new Exception("Cannot create or update basket now. Try again later.");
        }

        // ===============================
        // Delete Basket
        // ===============================
        public Task<bool> DeleteBasketAsync(string basketId)
        {
            if (string.IsNullOrEmpty(basketId)) throw new ArgumentNullException(nameof(basketId));

            return _basketRepository.DeleteBasketAsync(basketId);
        }

        // ===============================
        // Get Basket by Id
        // ===============================
        public async Task<BasketDto> GetBasketAsync(string basketId)
        {
            if (string.IsNullOrEmpty(basketId)) throw new ArgumentNullException(nameof(basketId));

            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket == null)
                throw new BasketNotFoundException(basketId);

            return _mapper.Map<BasketDto>(basket);
        }

        // ===============================
        // Add Item to Basket
        // ===============================
        public async Task<BasketDto> AddItemToBasketAsync(string basketId, BasketItemDto itemDto)
        {
            if (string.IsNullOrEmpty(basketId)) throw new ArgumentNullException(nameof(basketId));
            if (itemDto == null) throw new ArgumentNullException(nameof(itemDto));

            // Get basket or create a new one
            var basket = await _basketRepository.GetBasketAsync(basketId) ?? new CustomerBasket { Id = basketId };

            // Check if item already exists
            var existingItem = basket.Items.FirstOrDefault(x => x.Id ==itemDto.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += itemDto.Quantity;
            }
            else
            {
                basket.Items.Add(_mapper.Map<BasketItem>(itemDto));
            }

            await _basketRepository.CreateOrUpdateBasketAsync(basket);
            return _mapper.Map<BasketDto>(basket);
        }

        // ===============================
        // Remove Item from Basket
        // ===============================
        public async Task<BasketDto> RemoveItemFromBasketAsync(string basketId, string itemId)
        {
            if (string.IsNullOrEmpty(basketId)) throw new ArgumentNullException(nameof(basketId));
            if (string.IsNullOrEmpty(itemId)) throw new ArgumentNullException(nameof(itemId));

            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket == null)
                throw new BasketNotFoundException(basketId);

            var item = basket.Items.FirstOrDefault(x => x.Id == itemId);
            if (item != null)
            {
                basket.Items.Remove(item);
                await _basketRepository.CreateOrUpdateBasketAsync(basket);
            }

            return _mapper.Map<BasketDto>(basket);
        }

        // ===============================
        // Set Shipping Price
        // ===============================
        public async Task<BasketDto> SetShippingPriceAsync(string basketId, decimal shippingPrice, int? deliveryMethodId = null)
        {
            if (string.IsNullOrEmpty(basketId)) throw new ArgumentNullException(nameof(basketId));

            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket == null)
                throw new BasketNotFoundException(basketId);

            basket.ShippingPrice = shippingPrice;

            if (deliveryMethodId.HasValue)
                basket.DeliveryMethodId = deliveryMethodId.Value;

            await _basketRepository.CreateOrUpdateBasketAsync(basket);

            return _mapper.Map<BasketDto>(basket);
        }
    }
}
