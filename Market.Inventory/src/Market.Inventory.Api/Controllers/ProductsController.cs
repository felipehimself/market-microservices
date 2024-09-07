using Market.Inventory.Application.Dtos;
using Market.Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Inventory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetProducts()
        {

            return Ok(_productService.GetProducts());

        }

        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<ProductReadDto> GetProduct(Guid id)
        {
            return Ok(_productService.GetProduct(id));
        }

        [HttpPost]
        public ActionResult<ProductReadDto> CreateProduct(ProductCreateDto product)
        {

            var created = _productService.CreateProduct(product);

            return CreatedAtRoute(nameof(GetProduct), new { Id = created.Id }, created);

        }

    }
}