using Microsoft.Extensions.Options;
using Ardalis.Result;
using FredDevelopmentKit.Configuration;
using FredDevelopmentKit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FredDevelopmentKit.Services
{
    public class FredMapsService : FredService, IFredMapsService
    {
        public FredMapsService(IFredHttpClient fredClient, IOptions<FredClientOptions> options) :
            base(fredClient, options)
        {
        }
        public async Task<Result<SeriesGroup>> GetSeriesGroup(string seriesId)
        {
            string seriesGroupUrl = $"geofred/series/group?series_id={seriesId}&{GetCommonUrlSegments()}";

            Result<SeriesGroupResponseDto> result = await _fredClient.GetFromJsonAsync<SeriesGroupResponseDto>(seriesGroupUrl);
            if (result.IsSuccess)
            {
                SeriesGroupResponseDto? seriesGroupResponse = result.Value;
                if (seriesGroupResponse == null || seriesGroupResponse.SeriesGroup == null)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success<SeriesGroup>(seriesGroupResponse.SeriesGroup);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
        public async Task<Result<SeriesByRegion>> GetSeriesByRegion(string seriesId, 
            DateOnly? requestDate = null, DateOnly? startDate = null)
        {
            string seriesByRegionUrl = $"geofred/series/data?series_id={seriesId}&{GetCommonUrlSegments()}";
            if (requestDate.HasValue)
            {
                seriesByRegionUrl += $"&date={requestDate.Value:yyyy-MM-dd}";
            }
            if (startDate.HasValue)
            {
                seriesByRegionUrl += $"&start_date={startDate.Value:yyyy-MM-dd}";
            }
            Result<SeriesByRegionResponseDto> result = await _fredClient.GetFromJsonAsync<SeriesByRegionResponseDto>(seriesByRegionUrl);
            if (result.IsSuccess)
            {
                SeriesByRegionResponseDto? seriesByRegionResponse = result.Value;
                if (seriesByRegionResponse == null || seriesByRegionResponse.SeriesByRegion == null)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success<SeriesByRegion>(seriesByRegionResponse.SeriesByRegion);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
    }
}
