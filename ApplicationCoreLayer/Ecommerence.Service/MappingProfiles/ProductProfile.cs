using AutoMapper;
using Ecommerence.Shared.DTOS.ProductDtos;
using ECommerence.Domain.Entities.ProductModule;


namespace Ecommerence.Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand , BrandDTO>();

            CreateMap<Product, ProductDtos>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
            .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name));

            CreateMap<ProductType , TypeDTO>();
        }
    }
}