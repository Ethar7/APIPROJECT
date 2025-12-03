namespace Ecommerence.Shared.DTOS.ProductDtos
{
    public class ProductDtos
    {
        public int Id {get; set;}
        public string Name { get; set; } = null!;

        public string Description{get; set;} = null!;

        public string PictureUrl {get; set;} = null!;

        public decimal Price {get; set;}

        public string ProductPrand {get; set;} = null!;

        public string ProductType {get; set;} = null!;
    }
}