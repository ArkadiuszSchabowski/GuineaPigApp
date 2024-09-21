using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuineaPigApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("bad")]
        public ActionResult<List<Product>> GetBadProducts()
        {
            List<Product> products = _service.GetBadProducts();

            return Ok(products);
        }
        [HttpGet("good")]
        public ActionResult<List<Product>> GetGoodProducts()
        {
            List<Product> products = _service.GetGoodProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            Product product = _service.GetProduct(id);

            return Ok(product);
        }
        [HttpPost]
        public ActionResult AddProduct(ProductDto dto)
        {
            _service.AddProduct(dto);

            return Created();
        }
        [HttpDelete("{id}")]
        public ActionResult RemoveProduct(int id)
        {
            _service.RemoveProduct(id);
            return NoContent();
        }

    }
}
