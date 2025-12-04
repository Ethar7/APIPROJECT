using ECommerence.Domain.Entities.ProductModule;

namespace Ecommerence.Service.Specification
{
    public class ProductWithBrandAndTypesSpecification : BaseSpecification<Product , int>
    {
        public ProductWithBrandAndTypesSpecification(int? brandId, int? typeId) : base(P => (!brandId.HasValue || P.BrandId == brandId.Value) && (!typeId.HasValue || P.TypeId == typeId.Value))
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