using AutoMapper;
using CarInventory.Application.Commands.Car.Create;
using CarInventory.Application.Dtos;
using CarInventory.Domain.Exceptions;
using CarInventory.Domain.Interfaces.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;


namespace CarInventory.Application.Queries.Car
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCarCommandHandler> _logger;
        private readonly IValidator<CreateCarCommand> _validator;

        public GetCarByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCarCommandHandler> logger, IValidator<CreateCarCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }
        public async Task<CarDto> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var car = await _unitOfWork.Cars.GetByIdAsync(request.CarId);

            if (car == null)
            {
                _logger.LogError($"Car with ID {request.CarId} not found.");
                throw new NotFoundException(nameof(Car), request.CarId);
            }

            return _mapper.Map<CarDto>(car);
        }
    }
}
