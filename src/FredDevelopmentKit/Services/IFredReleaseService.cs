using Ardalis.Result;
using FredDevelopmentKit.Models;

namespace FredDevelopmentKit.Services
{
    public interface IFredReleaseService
    {
        Task<Result<ReleaseResponseDto>> GetReleases();
        Task<Result<ReleasesDatesResponseDto>> GetReleasesDates(); 
        Task<Result<Release>> GetRelease(int releaseId);
        Task<Result<ReleaseDateResponseDto>> GetReleaseDates(int releaseId);
        Task<Result<RelatedSeriesResponseDto>> GetSeries(int releaseId);
        Task<Result<SourcesResponseDto>> GetSources(int releaseId);
        Task<Result<TagsResponseDto>> GetTags(int releaseId);
        Task<Result<TagsResponseDto>> GetRelatedTags(int releaseId, List<string> searchTagNames);
    }
}
