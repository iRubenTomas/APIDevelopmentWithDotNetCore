using CarInventory.Application.Dtos;
using CarInventory.Domain.Interfaces.Shared;

namespace CarInventory.Application.Queries.Car.Paginated
{

    public class GetAllCarsPaginatedQuery : ICacheableQuery<PaginatedList<CarDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllCarsPaginatedQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
