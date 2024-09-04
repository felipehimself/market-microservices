using System.ComponentModel.DataAnnotations;

namespace Market.Sales.Domain.Entities
{
    public class Base
    {
        [Key]
        public Guid Id { get; set; }

        private DateTime? _createdAt;
        public DateTime? CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value ?? DateTime.UtcNow; }
        }

        public DateTime? UpdatedAt { get; set; }
    }
}