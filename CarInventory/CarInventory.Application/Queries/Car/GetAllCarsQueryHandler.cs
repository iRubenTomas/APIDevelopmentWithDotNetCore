﻿using AutoMapper;
using CarInventory.Application.Dtos;
using CarInventory.Domain.Interfaces.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarInventory.Application.Queries.Car
{
    public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, PaginatedList<CarDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCarsQueryHandler> _logger;

        public GetAllCarsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllCarsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedList<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = _unitOfWork.Cars.GetAllAsync(); 

            var carDtos = _mapper.Map<List<CarDto>>(cars.Result.ToList());

            var paginatedCars = await PaginatedList<CarDto>.CreateAsync(carDtos.AsQueryable(), request.PageNumber, request.PageSize);
 
            return paginatedCars;
        }
    }
}