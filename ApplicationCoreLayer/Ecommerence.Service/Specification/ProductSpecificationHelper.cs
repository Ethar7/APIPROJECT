using System.Linq.Expressions;
using Ecommerence.Shared;
using ECommerence.Domain.Entities.ProductModule;

namespace Ecommerence.Service.Specification
{
    public class ProductSpecificationHelper
    {
        // base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value) 
        // && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value) &&
        // (string.IsNullOrEmpty(queryParams.Search) || P.Name.ToLower().Contains(queryParams.Search.ToLower())))
        
        public static Expression<Func<Product , bool>> GetProductCriteria(ProductQueryParams queryParams)
        {
            return P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value) 
        && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value) &&
        (string.IsNullOrEmpty(queryParams.Search) || P.Name.ToLower().Contains(queryParams.Search.ToLower()));
        }
    }
}