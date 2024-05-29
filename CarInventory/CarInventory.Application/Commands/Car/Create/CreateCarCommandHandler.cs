using AutoMapper;
using CarInventory.Domain.Interfaces.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarInventory.Application.Commands.Car.Create
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCarCommandHandler> _logger;


        public CreateCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCarCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {

            var car = _mapper.Map<Domain.Entities.Car>(request);

            await _unitOfWork.Cars.AddAsync(car);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation($"Car with ID {car.Id} created successfully.");

            return car.Id;

        }
    }
}
