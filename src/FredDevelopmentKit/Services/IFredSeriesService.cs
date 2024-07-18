using Ardalis.Result;
using FredDevelopmentKit.Models;

namespace FredDevelopmentKit.Services
{
    public interface IFredSeriesService
    {
        Task<Result<SeriesDto>> GetSeries(string seriesId);
        Task<Result<CategoryResponseDto>> GetCategories(string seriesId);
        Task<Result<ObservationsResponseDto>> GetObservations(string seriesId);
        Task<Result<Release>> GetRelease(string seriesId);
        Task<Result<SeriesResponseDto>> SearchSeries(string searchText);
        Task<Result<TagsResponseDto>> SearchSeriesTags(string searchText);
        Task<Result<TagsResponseDto>> SearchSeriesRelatedTags(string searchText);
        Task<Result<TagsResponseDto>> GetTags(string seriesId);
        Task<Result<UpdateResponseDto>> GetSeriesUpdates();
        Task<Result<VintageDatesResponseDto>> GetVintageDates(string seriesId);
    }
}
