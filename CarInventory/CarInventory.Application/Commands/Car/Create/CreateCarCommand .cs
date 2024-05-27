using MediatR;


namespace CarInventory.Application.Commands.Car.Create
{
    public class CreateCarCommand : IRequest<Guid>
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string VIN { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
