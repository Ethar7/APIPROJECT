using AutoMapper;
using Ecommerence.Shared.DTOS.IdentityDTOS;
using ECommerence.Domain.Entities.IdentityModules;

namespace Ecommerence.Service.MappingProfiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}