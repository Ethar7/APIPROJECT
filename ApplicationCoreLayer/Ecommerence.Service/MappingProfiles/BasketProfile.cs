using AutoMapper;
using Ecommerence.Shared.DTOS.BasketDtos;
using ECommerence.Domain.Entities;

namespace Ecommerence.Service.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

        }
    }
}