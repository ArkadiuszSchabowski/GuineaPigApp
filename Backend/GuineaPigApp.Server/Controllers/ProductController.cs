using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp_Server.Models.Get;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuineaPigApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IAddService<ProductDto> _addService;
        private readonly IGetService<GetProductDto> _getService;

        public ProductController(IProductService service, IAddService<ProductDto> addService, IGetService<GetProductDto> getService)
        {
            _service = service;
            _addService = addService;
            _getService = getService;
        }

        [HttpGet("bad")]
        public ActionResult<ProductResultDto> GetBadProducts([FromQuery] PaginationDto dto)
        {
            ProductResultDto result = _service.GetBadProductsResult(dto);

            return Ok(result);
        }

        [HttpGet("good")]
        public ActionResult<ProductResultDto> GetGoodProducts([FromQuery] PaginationDto dto)
        {
            ProductResultDto result = _service.GetGoodProductsResult(dto);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<GetProductDto> Get(int id)
        {
            GetProductDto product = _getService.Get(id);

            return Ok(product);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public ActionResult AddProduct(ProductDto dto)
        {
            _addService.Add(dto);

            return Created();
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public ActionResult RemoveProduct(int id)
        {
            _service.RemoveProduct(id);
            return NoContent();
        }

    }
}
