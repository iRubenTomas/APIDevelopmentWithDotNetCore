using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarInventory.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Entering ValidatorPipelineBehavior for {RequestType}", typeof(TRequest).Name);

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(result => result.Errors).Where(error => error != null).ToList();

                if (failures.Any())
                {
                    _logger.LogWarning("Validation failed for {RequestType}", typeof(TRequest).Name);
                    throw new ValidationException(failures);
                }
            }

            _logger.LogInformation("Validation succeeded for {RequestType}", typeof(TRequest).Name);
            return await next();
        }
    }
}
