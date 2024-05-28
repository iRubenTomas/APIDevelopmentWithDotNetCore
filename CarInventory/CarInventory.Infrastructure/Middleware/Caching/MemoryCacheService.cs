using CarInventory.Domain.Interfaces.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace CarInventory.Infrastructure.Middleware.Caching
{
    public class MemoryCacheService : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.FromResult(_memoryCache.TryGetValue(key, out T value) ? value : default);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            _memoryCache.Set(key, value, expiration);
            await Task.CompletedTask;
        }
    }
}
