namespace Market.Sales.Application.Dtos
{
    public class SaleReadDto
    {
        
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        
        public  int Quantity { get; set; }
       
        public  double Price { get; set; }

        public DateTime CreatedAt { get; set; }
        
    }
}