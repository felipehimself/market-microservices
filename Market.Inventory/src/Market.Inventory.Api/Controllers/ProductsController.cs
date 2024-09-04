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
            public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll() 
            {
                
                return Ok(await _productService.GetProductsAsync());

            }

            [HttpGet("{id}", Name = "GetProduct")]
            public async Task<ActionResult<ProductReadDto>> GetProduct(Guid id)
            {   
                return Ok(await _productService.GetProductAsync(id));
            }

            [HttpPost]
            public async Task<ActionResult<ProductReadDto>> CreateProduct(ProductCreateDto product)
            {

                var created = await _productService.CreateProductAsync(product);

                return CreatedAtRoute(nameof(GetProduct), new { Id = created.Id }, created); 

            }

    }
}