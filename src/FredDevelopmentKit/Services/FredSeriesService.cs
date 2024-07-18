using Ardalis.Result;
using FredDevelopmentKit.Configuration;
using FredDevelopmentKit.Models;
using Microsoft.Extensions.Options;

namespace FredDevelopmentKit.Services
{
    public class FredSeriesService : FredService, IFredSeriesService
    {
        public FredSeriesService(FredHttpClient fredClient, IOptions<FredClientOptions> options) : 
            base(fredClient, options)
        {
        }
        public async Task<Result<SeriesDto>> GetSeries(string seriesId)
        {
            string seriesUrl = $"series?series_id={seriesId}&{GetCommonUrlSegments()}";
            Result<SeriesResponseDto> result = await _fredClient.GetFromJsonAsync<SeriesResponseDto>(seriesUrl);
            if (result.IsSuccess)
            {
                SeriesResponseDto? seriesResponse = result.Value;
                if (seriesResponse == null || seriesResponse.Series == null || seriesResponse.Series.Count == 0)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success<SeriesDto>(seriesResponse.Series[0]);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
        public async Task<Result<CategoryResponseDto>> GetCategories(string seriesId)
        {
            string categoriesUrl = $"series/categories?series_id={seriesId}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<CategoryResponseDto>(categoriesUrl);
        }
        public async Task<Result<ObservationsResponseDto>> GetObservations(string seriesId)
        {
            string categoriesUrl = $"series/observations?series_id={seriesId}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<ObservationsResponseDto>(categoriesUrl);
        }

        public async Task<Result<Release>> GetRelease(string seriesId)
        {
            string releaseUrl = $"series/release?series_id={seriesId}&{GetCommonUrlSegments()}";
            Result<ReleaseResponseDto> result = await _fredClient.GetFromJsonAsync<ReleaseResponseDto>(releaseUrl);
            if (result.IsSuccess)
            {
                ReleaseResponseDto? releaseResponse = result.Value;
                if (releaseResponse == null || releaseResponse.Releases == null || releaseResponse.Releases.Count == 0)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success<Release>(releaseResponse.Releases[0]);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
        public async Task<Result<SeriesResponseDto>> SearchSeries(string searchText)
        {
            string searchUrl = $"series/search?search_text={searchText}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<SeriesResponseDto>(searchUrl);
        }
        public async Task<Result<TagsResponseDto>> SearchSeriesTags(string searchText)
        {
            string searchUrl = $"series/search/tags?series_search_text={searchText}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<TagsResponseDto>(searchUrl);
        }
        public async Task<Result<TagsResponseDto>> SearchSeriesRelatedTags(string searchText)
        {
            string searchUrl = $"series/search/related_tags?series_search_text={searchText}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<TagsResponseDto>(searchUrl);
        }
        public async Task<Result<TagsResponseDto>> GetTags(string seriesId)
        {
            string tagsUrl = $"series/tags?series_id={seriesId}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<TagsResponseDto>(tagsUrl);
        }
        public async Task<Result<UpdateResponseDto>> GetSeriesUpdates()
        {
            string updatesUrl = $"series/updates?{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<UpdateResponseDto>(updatesUrl);
        }
        public async Task<Result<VintageDatesResponseDto>> GetVintageDates(string seriesId)
        {
            string vintageDatesUrl = $"series/vintagedates?series_id={seriesId}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<VintageDatesResponseDto>(vintageDatesUrl);
        }
    }
}
