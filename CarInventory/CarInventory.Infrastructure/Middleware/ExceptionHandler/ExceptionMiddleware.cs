using CarInventory.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;


namespace CarInventory.Infrastructure.Middleware.ExceptionHandler
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomValidationProblemsDetails problem;

            switch (ex)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomValidationProblemsDetails
                    {
                        Title = "Validation failed",
                        Status = (int)statusCode,
                        Type = nameof(ValidationException),
                        Errors = validationException.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray())
                    };
                    problem.Extensions.Add("traceId", httpContext.TraceIdentifier);
                    break;

                case BadRequestException BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomValidationProblemsDetails
                    {
                        Title = BadRequestException.Message,
                        Status = (int)statusCode,
                        Detail = BadRequestException.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = BadRequestException.ValidationErrors
                    };
                    problem.Extensions.Add("traceId", httpContext.TraceIdentifier);
                    break;

                case NotFoundException NotFound:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomValidationProblemsDetails
                    {
                        Title = NotFound.Message,
                        Status = (int)statusCode,
                        Type = nameof(NotFoundException),
                        Detail = NotFound.InnerException?.Message
                    };
                    problem.Extensions.Add("traceId", httpContext.TraceIdentifier);
                    break;

                default:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomValidationProblemsDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.InnerException?.Message
                    };
                    problem.Extensions.Add("traceId", httpContext.TraceIdentifier);
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
