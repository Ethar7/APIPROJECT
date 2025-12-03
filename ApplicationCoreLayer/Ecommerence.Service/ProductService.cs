using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.ProductDtos;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities.ProductModule;
using AutoMapper;

namespace Ecommerence.Service
{
    public class ProductService : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRebository<ProductPrand, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<PrandDTO>>(Brands);
        }

        public async Task<IEnumerable<ProductDtos>> GetAllProductAsync()
        {
            var Products = await _unitOfWork.GetRebository<Product, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDtos>>(Products);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRebository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(Types);
        }   

        public async Task<ProductDtos> GetProductByIdAsync(int id)
        {
            var Product = await _unitOfWork.GetRebository<Product, int>().GetByIdAsync(id);
            return _mapper.Map<ProductDtos>(Product);
        }
    }
}