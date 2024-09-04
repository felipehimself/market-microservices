namespace Market.Sales.Domain.Entities
{
    public class Sale : Base
    {
        public Guid ProductId { get; set; }
        public  int Quantity { get; set; }
        public  double Price { get; set; }
    }
}