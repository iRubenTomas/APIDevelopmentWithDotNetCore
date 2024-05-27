﻿using Asp.Versioning;
using CarInventory.Application.Queries.External;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarInventory.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ThirdPartyIntegration : ControllerBase
    {

        private readonly ILogger<ThirdPartyIntegration> _logger;
        private readonly IMediator _mediator;
        public ThirdPartyIntegration(ILogger<ThirdPartyIntegration> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpGet("ImportCars")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetData()
        {
            var requestUrl = "https://freetestapi.com/api/v1/cars"; // Sample API endpoint
            var query = new ImportCarsQuery(requestUrl);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
