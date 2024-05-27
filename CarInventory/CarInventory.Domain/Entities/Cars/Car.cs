using System.ComponentModel.DataAnnotations.Schema;

namespace CarInventory.Domain.Entities.Cars
{
    public class Car : BaseIdentity
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string VIN { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Status { get; set; } = "Available"; // Default status -> "Available", "Sold"
    }
}
