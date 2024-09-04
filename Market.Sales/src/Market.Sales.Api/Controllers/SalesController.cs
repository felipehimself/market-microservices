using Market.Sales.Application.Dtos;
using Market.Sales.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Sales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController(ISaleService service) : ControllerBase
    {
        private readonly ISaleService _service = service;


        [HttpGet("{id}")]
        public async Task<ActionResult<SaleReadDto?>> GetSale(Guid id) 
        {
            var sale = await _service.GetSaleAsync(id);

            if (sale == null) return NotFound();

            return Ok(sale);

        }


        [HttpPost]
        public async Task<ActionResult> CreateSale(SaleCreateDto sale)
        {
            await _service.CreateSaleAsync(sale);

            return Ok();
        }


    }
}