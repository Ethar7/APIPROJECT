using AutoMapper;
using DomainLayer.Models.OrderModels;
using Ecommerence.Shared.DTOS.IdentityDTOS;

namespace Ecommerence.Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>();
            
        }
    }
}