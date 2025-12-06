using Ecommerence.Shared;
using ECommerence.Domain.Entities.ProductModule;

namespace Ecommerence.Service.Specification
{
    public class ProductCountSpecification : BaseSpecification<Product , int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) : base(ProductSpecificationHelper.GetProductCriteria(queryParams))
        {
            
        }
    }
}