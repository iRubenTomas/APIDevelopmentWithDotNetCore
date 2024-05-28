using CarInventory.Api.Controllers;
using CarInventory.Application.Commands.Car.Create;
using CarInventory.Application.Commands.Car.Delete;
using CarInventory.Application.Commands.Car.Update;
using CarInventory.Application.Dtos;
using CarInventory.Application.Queries.Car.Paginated;
using CarInventory.Application.Queries.Car;
using CarInventory.Domain.Interfaces.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarInventory.UnitTests
{
    public class CarControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<CarController>> _loggerMock;
        private readonly CarController _controller;

        public CarControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<CarController>>();
            _controller = new CarController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Post_ShouldReturnCreatedResponse()
        {
            // Arrange
            var createCarCommand = new CreateCarCommand("Toyota", "Corolla", 2021, "12345678901234567", 20000);
            var newCarId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCarCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(newCarId);

            // Act
            var result = await _controller.Post(createCarCommand);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(newCarId, actionResult.Value);
        }

        [Fact]
        public async Task GetById_ShouldReturnCarDto()
        {
            // Arrange
            var carId = Guid.NewGuid();
            var carDto = new CarDto { Id = carId, Brand = "Toyota", Model = "Corolla", Year = 2021, VIN = "12345678901234567", Price = 20000 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCarByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(carDto);

            // Act
            var result = await _controller.GetById(carId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CarDto>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(carDto, okResult.Value);
        }

        [Fact]
        public async Task GetAll_ShouldReturnPaginatedList()
        {
            // Arrange
            var query = new GetAllCarsPaginatedQuery(1, 10);
            var paginatedList = new PaginatedList<CarDto>(new List<CarDto>(), 0, 1, 10);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCarsPaginatedQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(paginatedList);

            // Act
            var result = await _controller.GetAll(query.PageNumber, query.PageSize);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(paginatedList, actionResult.Value);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent()
        {
            // Arrange
            var carId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCarCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);
           

            // Act
            var result = await _controller.Delete(carId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedCarDto()
        {
            // Arrange
            var updateCarCommand = new UpdateCarCommand(Guid.NewGuid(), "Toyota", "Corolla", 2021, "12345678901234567", 25000, "Available");
            var updatedCarDto = new CarDto { Id = updateCarCommand.Id, Brand = "Toyota", Model = "Corolla", Year = 2021, VIN = "12345678901234567", Price = 25000, Status = "Available" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCarCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedCarDto);

            // Act
            var result = await _controller.Update(updateCarCommand);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(updatedCarDto, actionResult.Value);
        }
    }
}
