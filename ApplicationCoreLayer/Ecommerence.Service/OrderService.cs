using AutoMapper;
using DomainLayer.Models.OrderModels;
using Ecommerence.Service.Specification.OrderSpecification;
using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.IdentityDTOS;
using Ecommerence.Shared.DTOS.OrderDTOS;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities.ProductModule;
using ECommerence.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Ecommerence.Service
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        // Constructor injection بالشكل التقليدي
        public OrderService(IMapper mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string email)
        {
            var orderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.ShipToAddress);

            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId)
                        ?? throw new BasketNotFoundException(orderDto.BasketId);

            var orderItems = new List<OrderItem>();

            var productRepo = _unitOfWork.GetRebository<Product, int>();
            foreach (var basketItem in basket.Items)
            {
                var productId = int.Parse(basketItem.Id); // تأكد إنه رقم صالح
                var product = await productRepo.GetByIdAsync(productId)
                    ?? throw new ProductNotFoundException(productId);

                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductId = product.Id,
                        PictureUrl = product.PictureUrl,
                        ProductName = product.Name,
                    },
                    Price = product.Price,
                    Quantity = basketItem.Quantity,
                };

                orderItems.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.GetRebository<DeliveryMethod, int>()
                                    .GetByIdAsync(orderDto.DeliveryMethodId)
                                    ?? throw new DeliveryNotFoundException(orderDto.DeliveryMethodId);

            var subTotal = orderItems.Sum(i => i.Quantity * i.Price);

            var order = new Order(email, orderAddress, deliveryMethod, deliveryMethod.Id, orderItems, subTotal);

            await _unitOfWork.GetRebository<Order, Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var orderToReturn = _mapper.Map<Order, OrderToReturnDto>(order);
            return orderToReturn;
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email)
        {
            var specs = new OrderSpec(email);
            var orders = await _unitOfWork.GetRebository<Order, Guid>().GetAllAsync(specs);
            return _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRebository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }

        public async Task<OrderToReturnDto> GetOrderAsync(Guid id)
        {
            var specs = new OrderSpec(id);
            var order = await _unitOfWork.GetRebository<Order, Guid>().GetByIdAsync(specs);
            return _mapper.Map<OrderToReturnDto>(order);
        }
    }
}
