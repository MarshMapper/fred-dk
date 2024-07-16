using System.Net.Http.Json;
using System.Text.Json;
using Ardalis.Result;

namespace FredDevelopmentKit.Services
{
    public class FredHttpClient
    {
        private readonly HttpClient _httpClient;
        public FredHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.stlouisfed.org/fred/");
        }
        public async Task<Result<T>> GetFromJsonAsync<T>(string url)
        {
            Result<T> result;
            var options = new JsonSerializerOptions
            {
                // tell serializer response properties are in snake_case so they can be mapped to C# properties
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };
            try
            {
                T? response = await _httpClient.GetFromJsonAsync<T?>(url, options);
                if (response == null)
                {
                    result = Result<T>.NotFound();
                }
                else
                {
                    result = Result<T>.Success(response);
                }
            }
            catch (Exception ex)
            {
                result = Result<T>.Error(ex.Message);
            }
            return result;
        }
    }
}
