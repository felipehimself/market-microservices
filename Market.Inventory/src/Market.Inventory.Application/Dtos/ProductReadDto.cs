namespace Market.Inventory.Application.Dtos
{
    public class ProductReadDto
    {
        public Guid Id { get; set; }
        public  string Name {get; set;} = String.Empty;
        public  int Quantity {get; set;}
        public  string Category {get; set;} = String.Empty;
        public  string Measure {get; set;} = String.Empty;
        public  double Price {get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}