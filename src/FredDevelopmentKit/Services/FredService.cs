
using FredDevelopmentKit.Configuration;
using Microsoft.Extensions.Options;

namespace FredDevelopmentKit.Services
{
    public class FredService
    {
        protected readonly FredHttpClient _fredClient;
        private string _apiKey = "";

        public FredService(FredHttpClient fredClient, IOptions<FredClientOptions> options)
        {
            _fredClient = fredClient;
            _apiKey = options.Value.ApiKey;
        }
        public string GetApiUrlSegment()
        {
            return $"api_key={_apiKey}";
        }
        public string GetCommonUrlSegments()
        {
            return $"{GetApiUrlSegment()}&file_type=json";
        }
    }
}
