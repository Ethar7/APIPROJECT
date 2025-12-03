
using Ecommerence.ServiceAppstraction;
using Ecommerence.Shared.DTOS.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerence.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        #region Get All Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDtos>>> GetAllProduct()
        {
            var Products = await _productServices.GetAllProductAsync();
            return Ok(Products);
        }

        #endregion

        #region Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDtos>> GetProductById(int id)
        {
            var Product = await _productServices.GetProductByIdAsync(id);
            return Ok(Product);
        }

        #endregion
    
    
        #region Get All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<PrandDTO>>> GetAllBrands()
        {
            var Brands = await _productServices.GetAllBrandsAsync();
            return Ok(Brands);
        }

        #endregion


        #region Get All Brands
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var Types = await _productServices.GetAllTypesAsync();
            return Ok(Types);
        }

        #endregion
    }
}