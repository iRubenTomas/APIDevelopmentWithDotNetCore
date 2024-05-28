using CarInventory.Domain.Interfaces.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
namespace CarInventory.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableQuery<TResponse> // Apply only to cacheable queries
    {
        private readonly ICache _cache;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

        public CachingBehavior(ICache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cacheKey = GenerateCacheKey(request);

            _logger.LogInformation($"Checking cache for key: {cacheKey}");

            var cachedResponse = await _cache.GetAsync<TResponse>(cacheKey);
            if (cachedResponse != null )
            {
                _logger.LogInformation($"Cache hit for key: {cacheKey}");
                return cachedResponse;
            }

            _logger.LogInformation($"Cache miss for key: {cacheKey}");

            var response = await next();

            await _cache.SetAsync(cacheKey, response, TimeSpan.FromSeconds(30)); // Set cache expiration to 30 seconds

            _logger.LogInformation($"Response cached for key: {cacheKey}");

            return response;
        }

        private string GenerateCacheKey(TRequest request)
        {
            // Generate a unique cache key based on the request data
            return $"{typeof(TRequest).FullName}:{string.Join(":", request.GetType().GetProperties().Select(p => p.GetValue(request)?.ToString() ?? string.Empty))}";
        }
    }
}
