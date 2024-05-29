using Asp.Versioning;
using CarInventory.Application.Commands.Car.Create;
using CarInventory.Application.Commands.Car.Delete;
using CarInventory.Application.Commands.Car.Update;
using CarInventory.Application.Dtos;
using CarInventory.Application.Queries.Car;
using CarInventory.Application.Queries.Car.Paginated;
using CarInventory.Domain.Interfaces.Shared;
using CarInventory.Infrastructure.Middleware.ExceptionHandler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarInventory.Api.Controllers
{
    [Route("api/v{version:apiVersion}/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
       
        private readonly ILogger<CarController> _logger;
        private readonly IMediator _mediator;
        public CarController(ILogger<CarController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new car.
        /// </summary>
        /// <param name="createCar">The car creation command.</param>
        /// <returns>The ID of the created car.</returns>
        /// <response code="201">Returns the ID of the created car</response>
        /// <response code="400">If the car is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(400)]
        [ApiVersion("1.0")]
        public async Task<ActionResult<Guid>> Post([FromBody] CreateCarCommand createCar)
        {
            var response = await _mediator.Send(createCar);
            return CreatedAtAction(nameof(GetById), new { id = response }, response);
        }

        /// <summary>
        /// Gets a car by ID.
        /// </summary>
        /// <param name="id">The ID of the car.</param>
        /// <returns>The car details.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CarDto), 200)]
        [ProducesResponseType(404)]
        [ApiVersion("1.0")]
        public async Task<ActionResult<CarDto>> GetById(Guid id)
        {
            var query = new GetCarByIdQuery(id);
            return await _mediator.Send(query);

        }

        /// <summary>
        /// Gets all cars with pagination (v1.0).
        /// </summary>
        /// <param name="pageNumber">The page number (default is 1).</param>
        /// <param name="pageSize">The page size (default is 10).</param>
        /// <returns>A paginated list of cars.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<CarDto>), 200)]
        [ProducesResponseType(400)]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllCarsQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets all cars with pagination (v2.0).
        /// </summary>
        /// <param name="pageNumber">The page number (default is 1).</param>
        /// <param name="pageSize">The page size (default is 10).</param>
        /// <returns>A paginated list of cars.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<CarDto>), 200)]
        [ProducesResponseType(400)]
        [ApiVersion("2.0")]
        public async Task<IActionResult> GetAllPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllCarsQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a car by ID.
        /// </summary>
        /// <param name="id">The ID of the car to delete.</param>
        /// <returns>No content if the deletion was successful.</returns>
        /// <response code="204">If the car was deleted successfully</response>
        /// <response code="404">If the car was not found</response>
        /// <response code="400">If the request is invalid</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ApiVersion("1.0")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCarCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing car.
        /// </summary>
        /// <param name="updateCar">The car details to update.</param>
        /// <returns>The updated car details.</returns>
        /// <response code="200">Returns the updated car details.</response>
        /// <response code="400">If the car data is invalid.</response>
        /// <response code="404">If the car is not found.</response>
        [HttpPut]
        [ProducesResponseType(typeof(CarDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(CustomValidationProblemsDetails), 400)]
        [ApiVersion("1.0")]
        public async Task<IActionResult> Update([FromBody] UpdateCarCommand updateCar)
        {
            var updatedCar = await _mediator.Send(updateCar);
            return Ok(updatedCar);
        }
    }
}
