using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerence.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        [Route("{id}")]
        [HttpGet]
        public ActionResult Get(int id)
        {
            
           return Ok(new Product {Id = 10, Name = "Chai"});
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            
           return Ok(new Product {Id = 20, Name = "Coffe"});
        }
        [HttpPut]
        public ActionResult Update(Product product)
        {
            return Ok(product);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            return Ok(product);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}