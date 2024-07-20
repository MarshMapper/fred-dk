using Ardalis.Result;
using FredDevelopmentKit.Models;

namespace FredDevelopmentKit.Services
{
    public interface IFredMapsService
    {
        Task<Result<SeriesGroup>> GetSeriesGroup(string seriesId);
        Task<Result<SeriesByRegion>> GetSeriesByRegion(string seriesId, DateOnly? requestDate = null, DateOnly? startDate = null);
    }
}
