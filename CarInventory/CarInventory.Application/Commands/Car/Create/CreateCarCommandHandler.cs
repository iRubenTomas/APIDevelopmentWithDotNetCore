using AutoMapper;
using CarInventory.Application.Dtos;
using CarInventory.Domain.Exceptions;
using CarInventory.Domain.Interfaces.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarInventory.Application.Commands.Car.Create
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCarCommandHandler> _logger;
        private readonly IValidator<CreateCarCommand> _validator;

        public CreateCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCarCommandHandler> logger, IValidator<CreateCarCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            // Validate the incoming command
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Invalid Car Data", validationResult);
            }

            // Map the validated command to a Car entity
            var car = _mapper.Map<Domain.Entities.Cars.Car>(request);

            await _unitOfWork.Cars.AddAsync(car);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation($"Car with ID {car.Id} created successfully.");

            return car.Id;

        }
    }
}
