using Ardalis.Result;
using FredDevelopmentKit.Models;

namespace FredDevelopmentKit.Services
{
    public interface IFredSourceService
    {
        Task<Result<SourcesResponseDto>> GetSources();
        Task<Result<SourceDto>> GetSource(int sourceId);
        Task<Result<ReleasesResponseDto>> GetReleases(int sourceId);
    }
}
