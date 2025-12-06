using System.Net.Http.Headers;
using Ecommerence.Shared;
using Ecommerence.Shared.DTOS.ProductDtos;

namespace Ecommerence.ServiceAppstraction
{
    public interface IProductServices
    {
        Task<PaginatedResult<ProductDtos>> GetAllProductAsync(ProductQueryParams queryParams);

        Task<ProductDtos> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
    }
}