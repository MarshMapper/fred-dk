using Ardalis.Result;

namespace FredDevelopmentKit.Services
{
    public interface IFredHttpClient
    {
        Task<Result<T>> GetFromJsonAsync<T>(string url);
    }
}
