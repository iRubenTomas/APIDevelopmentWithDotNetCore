using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CarInventory.Api.Util.CustomHealthCheck
{
    public class ThirdPartyApiHealthCheck : IHealthCheck
    {
        private readonly HttpClient _httpClient;

        public ThirdPartyApiHealthCheck(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // Replace with the actual third-party API URL
            var apiUrl = "https://api.example.com/health";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy("The third-party API is available.");
                }

                return HealthCheckResult.Unhealthy("The third-party API is unavailable.");
            }
            catch (HttpRequestException ex)
            {
                return HealthCheckResult.Unhealthy("The third-party API is unavailable.", ex);
            }
        }
    }
}
