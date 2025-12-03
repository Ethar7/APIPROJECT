using AutoMapper;
using Ecommerence.Shared.DTOS.ProductDtos;
using ECommerence.Domain.Entities.ProductModule;


namespace Ecommerence.Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductPrand , PrandDTO>();

            CreateMap<Product, ProductDtos>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductTypes.Name))
            .ForMember(dest => dest.ProductPrand, opt => opt.MapFrom(src => src.ProductPrands.Name));

            CreateMap<ProductType , TypeDTO>();
        }
    }
}