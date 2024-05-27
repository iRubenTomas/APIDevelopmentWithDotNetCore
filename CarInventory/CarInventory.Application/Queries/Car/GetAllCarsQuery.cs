using CarInventory.Application.Dtos;
using MediatR;

namespace CarInventory.Application.Queries.Car
{
    public class GetAllCarsQuery : IRequest<PaginatedList<CarDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetAllCarsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber > 0 ? pageNumber : 1;
            PageSize = pageSize > 0 ? pageSize : 10;
        }
    }
}
