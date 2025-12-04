

using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.ProductDtos;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities.ProductModule;
using AutoMapper;
using Ecommerence.Service.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerence.Shared;

namespace Ecommerence.Service
{
    public class ProductService : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRebository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(brands);
        }

        
        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRebository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(types);
        }

       
        public async Task<IEnumerable<ProductDtos>> GetAllProductAsync(ProductQueryParams queryParams)
        {
            var spec = new ProductWithBrandAndTypesSpecification(queryParams);
            var products = await _unitOfWork.GetRebository<Product, int>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<ProductDtos>>(products);
        }

        public async Task<ProductDtos> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndTypesSpecification(id);
            // var products = await _unitOfWork.GetRebository<Product, int>().GetAllAsync(spec);
            var product = await _unitOfWork.GetRebository<Product, int>().GetByIdAsync(spec);
            return _mapper.Map<ProductDtos>(product);
        }
    }
}
