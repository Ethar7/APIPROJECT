using System.ComponentModel.DataAnnotations;

namespace Ecommerence.Shared.DTOS.BasketDtos
{
    public class BasketItemDto
    {
        public string Id{get; set;}
        public string ProductName {get; set;} = null;

        public string PictureUrl{get; set;} = null;
        [Range(1,double.MaxValue)]
        public decimal Price{get; set;}
        [Range(1,double.MaxValue)]
        public int Quantity{get; set;}
    }
}