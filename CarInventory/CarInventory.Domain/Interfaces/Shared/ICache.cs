
namespace CarInventory.Domain.Interfaces.Shared
{
    public interface ICache
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
    }
}
