using ECommerence.Domain.Entities.ProductModule;

namespace Ecommerence.Service.Specification
{
    public class ProductWithBrandAndTypesSpecification : BaseSpecification<Product , int>
    {
        public ProductWithBrandAndTypesSpecification() : base(null)
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