using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
        public async Task<T?> GetFromJsonAsync<T>(string url)
        {
            var options = new JsonSerializerOptions
            {
                // tell serializer response properties are in snake_case so they can be mapped to C# properties
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };
            return await _httpClient.GetFromJsonAsync<T?>(url, options);
        }
    }
}
