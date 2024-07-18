using Ardalis.Result;
using FredDevelopmentKit.Configuration;
using FredDevelopmentKit.Models;
using Microsoft.Extensions.Options;

namespace FredDevelopmentKit.Services
{
    public class FredReleaseService : FredService, IFredReleaseService
    {
        public FredReleaseService(FredHttpClient fredClient, IOptions<FredClientOptions> options) : 
            base(fredClient, options)
        {
        }
        public async Task<Result<ReleaseResponseDto>> GetReleases()
        {             
            string releaseUrl = $"releases?{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<ReleaseResponseDto>(releaseUrl);
        }
        public async Task<Result<ReleasesDatesResponseDto>> GetReleasesDates()
        {
            string releaseDatesUrl = $"releases/dates?{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<ReleasesDatesResponseDto>(releaseDatesUrl);
        }

        public async Task<Result<Release>> GetRelease(int releaseId)
        {
            string releaseUrl = $"release?release_id={releaseId}&{GetCommonUrlSegments()}";
            Result<ReleaseResponseDto> result = await _fredClient.GetFromJsonAsync<ReleaseResponseDto>(releaseUrl);
            if (result.IsSuccess)
            {
                ReleaseResponseDto? ReleaseResponse = result.Value;
                if (ReleaseResponse == null || ReleaseResponse.Releases == null || ReleaseResponse.Releases.Count == 0)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success<Release>(ReleaseResponse.Releases[0]);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
        public async Task<Result<ReleaseDateResponseDto>> GetReleaseDates(int releaseId)
        {
            string releaseDatesUrl = $"release/dates?{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<ReleaseDateResponseDto>(releaseDatesUrl);
        }
        public async Task<Result<RelatedSeriesResponseDto>> GetSeries(int releaseId)
        {
            string seriesUrl = $"release/series?release_id={releaseId}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<RelatedSeriesResponseDto>(seriesUrl);
        }
        public async Task<Result<SourcesResponseDto>> GetSources(int releaseId)
        {
            string sourcesUrl = $"release/sources?release_id={releaseId}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<SourcesResponseDto>(sourcesUrl);
        }

        public async Task<Result<TagsResponseDto>> GetTags(int releaseId)
        {
            string tagsUrl = $"release/tags?release_id={releaseId}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<TagsResponseDto>(tagsUrl);
        }
        public async Task<Result<TagsResponseDto>> GetRelatedTags(int releaseId, List<string> searchTagNames)
        {
            string tagsUrl = $"release/related_tags?release_id={releaseId}&{GetCommonUrlSegments()}";
            if (searchTagNames != null && searchTagNames.Count > 0)
            {
                tagsUrl += "&tag_names=" + String.Join(';', searchTagNames);
            }

            return await _fredClient.GetFromJsonAsync<TagsResponseDto>(tagsUrl);
        }
    }
}
