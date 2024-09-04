using System.ComponentModel.DataAnnotations;


namespace Market.Inventory.Application.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        public string Name   { get; set; } = String.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Category { get; set; } = String.Empty;

        [Required]
        public string Measure { get; set; } = String.Empty;

        [Required]
        public double Price  { get; set; }
    }
}