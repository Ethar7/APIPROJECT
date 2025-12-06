using Ecommerence.Shared;
using ECommerence.Domain.Entities.ProductModule;

namespace Ecommerence.Service.Specification
{
    public class ProductCountSpecification : BaseSpecification<Product , int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) : base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value) 
        && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value) &&
        (string.IsNullOrEmpty(queryParams.Search) || P.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
            
        }
    }
}