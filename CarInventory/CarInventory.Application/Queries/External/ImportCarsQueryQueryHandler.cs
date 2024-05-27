using CarInventory.Application.Util.PollyPolicies;
using MediatR;
using Polly;

namespace CarInventory.Application.Queries.External
{
    public class ImportCarsQueryQueryHandler : IRequestHandler<ImportCarsQuery, string>
    {
        private readonly HttpClient _httpClient;
        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;

        public ImportCarsQueryQueryHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _retryPolicy = PollyPolicies.GetRetryPolicy(); // Apply retry policy here
        }

        public async Task<string> Handle(ImportCarsQuery request, CancellationToken cancellationToken)
        {
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync(request.RequestUrl, cancellationToken));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }

}
