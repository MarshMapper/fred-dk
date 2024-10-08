﻿
using FredDevelopmentKit.Configuration;
using Microsoft.Extensions.Options;

namespace FredDevelopmentKit.Services
{
    public class FredService
    {
        protected readonly IFredHttpClient _fredClient;
        private string _apiKey = "";

        public FredService(IFredHttpClient fredClient, IOptions<FredClientOptions> options)
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
