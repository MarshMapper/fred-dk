using Ardalis.Result;
using FredDevelopmentKit.Models;

namespace FredDevelopmentKit.Services
{
    public interface IFredReleaseService
    {
        Task<Result<ReleaseResponseDto>> GetReleases();
        Task<Result<ReleasesDatesResponseDto>> GetReleasesDates(); 
        Task<Result<Release>> GetRelease(int ReleaseId);
        Task<Result<ReleaseDateResponseDto>> GetReleaseDates(int ReleaseId);
        Task<Result<SeriesResponseDto>> GetSeries(int ReleaseId);
        Task<Result<SourcesResponseDto>> GetSources(int ReleaseId);
        Task<Result<TagsResponseDto>> GetTags(int ReleaseId);
        Task<Result<TagsResponseDto>> GetRelatedTags(int ReleaseId, List<string> searchTagNames);
        void SetApiKey(string apiKey);
    }
}
