using CarInventory.Application.Dtos;
using MediatR;

namespace CarInventory.Application.Queries.Car
{
    public class GetCarByIdQuery : IRequest<CarDto>
    {
        public Guid CarId { get; set; }

        public GetCarByIdQuery(Guid carId)
        {
            CarId = carId;
        }
    }
}
