using System.Net.Http.Headers;
using Ecommerence.Shared.DTOS.ProductDtos;

namespace Ecommerence.ServiceAppstraction
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDtos>> GetAllProductAsync();

        Task<ProductDtos> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
    }
}