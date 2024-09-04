namespace Market.Inventory.Domain.Entities
{
    public class Product : Base
    {

        public  string Name {get; set;} = String.Empty;
        public  int Quantity {get; set;}
        public  string Category {get; set;} = String.Empty;
        public  string Measure {get; set;} = String.Empty;
        public  double Price {get; set;}

    }
}