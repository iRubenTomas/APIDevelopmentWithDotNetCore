using AutoMapper;
using CarInventory.Application.Dtos;
using CarInventory.Domain.Exceptions;
using CarInventory.Domain.Interfaces.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarInventory.Application.Commands.Car.Update
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, CarDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCarCommandHandler> _logger;

        public UpdateCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateCarCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CarDto> Handle(UpdateCarCommand updateCar, CancellationToken cancellationToken)
        {
            var carFromDb = await _unitOfWork.Cars.GetByIdAsync(updateCar.Id);

            if (carFromDb == null)
            {
                _logger.LogError($"Car with ID {updateCar.Id} not found.");
                throw new NotFoundException(nameof(Car), updateCar.Id);
            }

            // Map the updateCar to the car entity
            var mappedCar = _mapper.Map(updateCar, carFromDb);

            await _unitOfWork.Cars.UpdateAsync(mappedCar);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation($"Car with ID {carFromDb.Id} was updated successfully.");

            return _mapper.Map<CarDto>(mappedCar);
        }
    }
}
