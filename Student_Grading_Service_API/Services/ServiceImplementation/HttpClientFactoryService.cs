using Polly;

namespace Student_Grading_Service_API.Services.ServiceImplementation
{
    public class HttpClientFactoryService<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientFactoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        public async Task<T> ExecuteWithRetryAsync(Func<HttpClient, Task<T>> apiCall, string httpClientName, int maxRetries = 3)
        {
            if (maxRetries < 0)
            {
                throw new ArgumentException("Max retries must be a non-negative number.");
            }

            var httpClient = _httpClientFactory.CreateClient(httpClientName);

            var retryPolicy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(maxRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            return await retryPolicy.ExecuteAsync(async () => await apiCall(httpClient));
        }
    }
}
