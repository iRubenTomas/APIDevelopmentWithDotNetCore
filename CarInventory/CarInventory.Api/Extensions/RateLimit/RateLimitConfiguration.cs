using AspNetCoreRateLimit;

namespace CarInventory.Api.Extensions.NewFolder
{
    public class RateLimitConfiguration
    {
        public IpRateLimitOptions IpRateLimitOptions { get; set; }
    }
}
