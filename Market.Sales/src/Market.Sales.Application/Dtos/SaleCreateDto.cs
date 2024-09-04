using System.ComponentModel.DataAnnotations;

namespace Market.Sales.Application.Dtos
{
    public class SaleCreateDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

    }
}