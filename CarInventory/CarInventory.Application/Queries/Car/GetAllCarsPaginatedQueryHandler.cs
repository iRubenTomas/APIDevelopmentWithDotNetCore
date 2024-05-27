using AutoMapper;
using CarInventory.Application.Dtos;
using CarInventory.Domain.Interfaces.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarInventory.Application.Queries.Car
{
    public class GetAllCarsPaginatedQueryHandler : IRequestHandler<GetAllCarsPaginatedQuery, PaginatedList<CarDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCarsPaginatedQueryHandler> _logger;

        public GetAllCarsPaginatedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllCarsPaginatedQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedList<CarDto>> Handle(GetAllCarsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var paginatedCars = await _unitOfWork.Cars.GetPaginatedAsync(request.PageNumber, request.PageSize);

            // Map the paginated list of entities to a paginated list of DTOs
            var carDtos = _mapper.Map<List<CarDto>>(paginatedCars.Items);
            return new PaginatedList<CarDto>(carDtos, paginatedCars.TotalCount, paginatedCars.PageIndex, request.PageSize);
        }
    }
}
