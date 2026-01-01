using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.OrderModels;
using Ecommerence.Shared.DTOS.OrderDTOS;
using Microsoft.Extensions.Configuration;

namespace Ecommerence.Service.MappingProfiles
{
    public class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl)) return string.Empty;
            return $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
        }
    }
}