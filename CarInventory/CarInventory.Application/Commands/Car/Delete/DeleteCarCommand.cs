using MediatR;

namespace CarInventory.Application.Commands.Car.Delete
{
    public class DeleteCarCommand : IRequest<Unit>
    {
        public Guid CarId { get; set; }

        public DeleteCarCommand(Guid carId)
        {
            CarId = carId;
        }
    }
}
