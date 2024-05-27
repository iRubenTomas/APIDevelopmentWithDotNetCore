using CarInventory.Domain.Entities.Cars;
using CarInventory.Domain.Entities.Customers;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarInventory.Domain.Entities.Sales
{
    public class SalesRecord : BaseIdentity
    {
        public Guid CarId { get; set; }
        public Car Car { get; set; } = new Car(); // Navigation property
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = new Customer();  // Navigation property
        public DateTime SaleDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }
    }
}
