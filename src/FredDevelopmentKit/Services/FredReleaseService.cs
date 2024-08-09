using Ardalis.Result;
using FredDevelopmentKit.Configuration;
using FredDevelopmentKit.Models;
using Microsoft.Extensions.Options;

namespace FredDevelopmentKit.Services
{
    // service to get data related to releases of economic data
    public class FredReleaseService : FredService, IFredReleaseService
    {
        public FredReleaseService(IFredHttpClient fredClient, IOptions<FredClientOptions> options) : 
            base(fredClient, options)
        {
        }
        // get all releases, optionally filtering by date range.  the FRED API does not provide this filtering
        // in a single API, so this service will act as BFF / aggregator if a date range is provided.  in that
        // case it will call the ReleaseDates API and filter the releases based on that result.
        public async Task<Result<ReleasesResponseDto>> GetReleases(DateOnly? startDate = null, DateOnly? endDate = null)
        {             
            string releaseUrl = $"releases?{GetCommonUrlSegments()}";

            Result<ReleasesResponseDto> result = await _fredClient.GetFromJsonAsync<ReleasesResponseDto>(releaseUrl);
            if (result.IsSuccess)
            {
                if (startDate.HasValue || endDate.HasValue)
                {
                    Result<ReleasesDatesResponseDto> releaseDatesResult = await GetReleasesDates(startDate, endDate);
                    if (releaseDatesResult.IsSuccess)
                    {
                        // call the releases API again to get the releases in the given date range
                        ReleasesDatesResponseDto? releaseDatesResponse = releaseDatesResult.Value;
                        if (releaseDatesResponse != null && releaseDatesResponse.ReleaseDates != null)
                        {
                            // filter the releases based on the release dates response.  this response will contain
                            // the release IDs that are in the date range, so only those IDs will be included in the
                            // result.
                            var filteredReleases = GetReleasesInDateRange(result.Value.Releases, releaseDatesResponse);
                            result.Value.Releases = filteredReleases;
                        }
                    }
                    else
                    {
                        return Result.NotFound();
                    }
                }
                return result;
            }
            else
            {
                return Result.NotFound();
            }
        }
        // helper method to filter releases based on the release dates response.  this response will contain
        // the release IDs that are in the date range, so only those IDs will be included in the result.
        public List<Release> GetReleasesInDateRange(List<Release> releases, ReleasesDatesResponseDto releaseDatesResponse)
        {
            HashSet<int> releaseIds = new HashSet<int>(releaseDatesResponse.ReleaseDates.Select(rd => rd.ReleaseId));

            return releases.Where(r => releaseIds.Contains(r.Id)).ToList();
        }
        // get the dates of releases, optionally filtering by date range
        public async Task<Result<ReleasesDatesResponseDto>> GetReleasesDates(DateOnly? startDate = null, DateOnly? endDate = null)
        {
            string releaseDatesUrl = $"releases/dates?{GetCommonUrlSegments()}";
            
            // if dates are provided, just pass them along to the API - up to the caller whether they make sense
            if (startDate.HasValue)
            {
                releaseDatesUrl += $"&realtime_start={startDate.Value:yyyy-MM-dd}";
            }
            if (endDate.HasValue)
            {
                releaseDatesUrl += $"&realtime_end={endDate.Value:yyyy-MM-dd}";
            }
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
