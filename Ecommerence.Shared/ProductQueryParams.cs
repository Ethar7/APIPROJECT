namespace Ecommerence.Shared
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public string? Search { get; set; }

        public ProductSortingObjects? Sort{get; set;}
    }
}