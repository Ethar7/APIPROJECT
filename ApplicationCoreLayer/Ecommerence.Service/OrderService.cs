using AutoMapper;
using DomainLayer.Models.OrderModels;
using Ecommerence.Service.Specification.OrderSpecification;
using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.IdentityDTOS;
using Ecommerence.Shared.DTOS.OrderDTOS;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities.ProductModule;
using ECommerence.Domain.Exceptions;

namespace Ecommerence.Service
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string email)
        {
            
            var orderAddress = _mapper.Map<AddressDto,OrderAddress>(orderDto.ShipToAddress);

            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId)
                            ?? throw new BasketNotFoundException(orderDto.BasketId);



            List<OrderItem> orderItems = [];

            var productRepo = _unitOfWork.GetRebository<Product, int>();
            foreach (var basketItem in basket.Items)
            {
                var product = await productRepo.GetByIdAsync(basketItem.Id)
                                ?? throw new ProductNotFoundException(basketItem.Id);

                var OrderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductId = product.Id, 
                        PictureUrl= product.PictureUrl, 
                        ProductName= product.Name,
                        
                    },

                    Price = product.Price,
                    Quantity = basketItem.Quantity,

                };
                orderItems.Add(OrderItem);
            }

            var deliverymethod = await _unitOfWork.GetRebository<DeliveryMethod, int>()
                                .GetByIdAsync(orderDto.DeliveryMethodId)
                                ?? throw new DeliveryNotFoundException(orderDto.DeliveryMethodId);


            var subTotal = orderItems.Sum(i=> i.Quantity * i.Price);
            
            var order = new Order(email, orderAddress, deliverymethod,deliverymethod.Id, orderItems, subTotal);

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
            var deliverymethod = await _unitOfWork.GetRebository<DeliveryMethod, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<DeliveryMethodDto>>(deliverymethod);
        }

        public async Task<OrderToReturnDto> GetOrderAsync(Guid id)
        {
            var specs = new OrderSpec(id);
            var orders = await _unitOfWork.GetRebository<Order, Guid>().GetByIdAsync(specs);
            return _mapper.Map<OrderToReturnDto>(orders);
        }
    }
}