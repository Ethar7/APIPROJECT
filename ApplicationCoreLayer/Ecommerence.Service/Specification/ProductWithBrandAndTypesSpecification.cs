using Ecommerence.Shared;
using ECommerence.Domain.Entities.ProductModule;
using ECommerence.Domain.Contracts;

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

            switch (queryParams.Sort)
            {
                case ProductSortingObjects.NameAsc:
                        AddOrderBy(P => P.Name);
                        break;
                case ProductSortingObjects.NameDesc:
                        AddOrderByDescending(P => P.Name);
                        break;

                case ProductSortingObjects.PriceAsc:
                        AddOrderBy(P => P.Price);
                        break;

                case ProductSortingObjects.PriceDesc:
                        AddOrderByDescending(P => P.Price);
                        break;
                default:
                        AddOrderBy(P => P.Id);
                        break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.pageIndex);
        }

        public ProductWithBrandAndTypesSpecification(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}