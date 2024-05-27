using Polly.Extensions.Http;
using Polly;

namespace CarInventory.Application.Util.PollyPolicies
{
    public static class PollyPolicies
    {
        // Retry Policy
        // This policy handles transient HTTP errors by retrying the request up to 3 times,
        // with an exponential backoff strategy (2^retryAttempt seconds).
        // Use Case: When you expect occasional transient faults that can be resolved by retrying
        // the request after a short delay, such as network glitches or temporary service unavailability.
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        // Circuit Breaker Policy
        // This policy breaks the circuit after 2 consecutive failed requests and prevents
        // further requests for 1 minute, allowing the system to recover.
        // Use Case: When we want to prevent system overload by stopping repeated failed calls
        // to a service that is currently unavailable, giving it time to recover.
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));
        }

        // Timeout Policy
        // This policy sets a timeout for each HTTP request. If the request takes longer
        // than the specified timeout duration (10 seconds), it will be canceled.
        // Use Case: When we need to ensure that requests complete within a certain timeframe
        // to maintain responsiveness and avoid hanging indefinitely.
        public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy()
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
        }

        // Fallback Policy
        // This policy provides a fallback response when an HTTP request fails. The fallback
        // response is returned instead of propagating the failure.
        // Use Case: When we want to provide a graceful fallback response rather than letting
        // the failure propagate and disrupt the user experience.
        public static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        {
            return Policy<HttpResponseMessage>
                .Handle<Exception>()
                .FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent("Fallback response")
                });
        }

        // Bulkhead Isolation Policy
        // This policy limits the number of concurrent requests to a specified maximum
        // (10 concurrent requests), providing isolation to prevent system overload.
        // Use Case: When we want to limit the number of concurrent calls to a specific resource
        // to avoid overloading it and ensure system stability.
        public static IAsyncPolicy<HttpResponseMessage> GetBulkheadPolicy()
        {
            return Policy.BulkheadAsync<HttpResponseMessage>(10, int.MaxValue);
        }
    }
}
