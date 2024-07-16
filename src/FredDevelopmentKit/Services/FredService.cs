
namespace FredDevelopmentKit.Services
{
    public class FredService
    {
        protected readonly FredHttpClient _fredClient;
        private string _apiKey = "";

        public FredService(FredHttpClient fredClient)
        {
            _fredClient = fredClient;
        }
        public void SetApiKey(string apiKey)
        {
            this._apiKey = apiKey;
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
