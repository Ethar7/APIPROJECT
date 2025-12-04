using Ecommerence.Shared;
using ECommerence.Domain.Entities.ProductModule;

namespace Ecommerence.Service.Specification
{
    public class ProductWithBrandAndTypesSpecification : BaseSpecification<Product , int>
    {
        public ProductWithBrandAndTypesSpecification(ProductQueryParams queryParams) : base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value) 
        && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value) &&
        (string.IsNullOrEmpty(queryParams.Search) || P.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        public ProductWithBrandAndTypesSpecification(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}