
using CarInventory.Application.Dtos;
using MediatR;

namespace CarInventory.Application.Commands.Car.Update
{
    public class UpdateCarCommand : IRequest<CarDto>
    {
        public Guid Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string VIN { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Status { get; set; }

        public UpdateCarCommand(Guid id, string brand, string model, int year, string vin, decimal price, string status)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Year = year;
            VIN = vin;
            Price = price;
            Status = status;
        }
    }
}
