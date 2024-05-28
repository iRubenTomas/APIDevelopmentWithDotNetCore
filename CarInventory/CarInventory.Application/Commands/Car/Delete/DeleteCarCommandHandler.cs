using AutoMapper;
using CarInventory.Application.Commands.Car.Create;
using CarInventory.Domain.Exceptions;
using CarInventory.Domain.Interfaces.Shared;
using MediatR;
using Microsoft.Extensions.Logging;


namespace CarInventory.Application.Commands.Car.Delete
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCarCommandHandler> _logger;


        public DeleteCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCarCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _unitOfWork.Cars.GetByIdAsync(request.CarId);

            if (car == null)
            {
                _logger.LogError($"Car with ID {request.CarId} not found.");
                throw new NotFoundException(nameof(Car), request.CarId);
            }

            await _unitOfWork.Cars.DeleteAsync(car.Id);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation($"Car with ID {request.CarId} was deleted successfully.");

            return Unit.Value;
        }
    }
}
