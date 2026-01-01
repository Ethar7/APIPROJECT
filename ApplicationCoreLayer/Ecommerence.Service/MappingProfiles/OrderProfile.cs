using AutoMapper;
using DomainLayer.Models.OrderModels;
using Ecommerence.Shared.DTOS.IdentityDTOS;
using Ecommerence.Shared.DTOS.OrderDTOS;
using Microsoft.Extensions.Options;

namespace Ecommerence.Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order,OrderToReturnDto>()
                        .ForMember(dest=> dest.DeliveryMethod, options=> options.MapFrom(scr=>scr.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()

                .ForMember(dest=>dest.ProductName, options=>options.MapFrom(scr=> scr.Product.ProductName))

                .ForMember(dest=>dest.PictureUrl, options=>options.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    }
}