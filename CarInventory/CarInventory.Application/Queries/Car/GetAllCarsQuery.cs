using CarInventory.Application.Dtos;
using CarInventory.Domain.Interfaces.Shared;


namespace CarInventory.Application.Queries.Car
{
    public class GetAllCarsQuery : ICacheableQuery<PaginatedList<CarDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllCarsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber ;
            PageSize = pageSize;
        }
    }
}
