using System.Net.Http.Json;
using System.Text.Json;
using Ardalis.Result;

namespace FredDevelopmentKit.Services
{
    /// <summary>
    /// Custom HttpClient for FRED API.  Sets the base URL, the default property naming policy (SnakeCaseLower), and 
    /// provides a method to get JSON data from the API.  The method is a wrapper around the standard GetFromJsonAsync
    /// that handles exceptions and returns a Result object.
    /// </summary>
    public class FredHttpClient : IFredHttpClient
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
