using Microsoft.Extensions.Options;
using Ardalis.Result;
using FredDevelopmentKit.Configuration;
using FredDevelopmentKit.Models;

namespace FredDevelopmentKit.Services
{
    public class FredSourceService : FredService, IFredSourceService
    {
        public FredSourceService(IFredHttpClient fredClient, IOptions<FredClientOptions> options) :
            base(fredClient, options)
        {
        }
        public async Task<Result<SourcesResponseDto>> GetSources()
        {
            string sourcesUrl = $"sources?{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<SourcesResponseDto>(sourcesUrl);
        }
        public async Task<Result<SourceDto>> GetSource(int sourceId)
        {
            string sourceUrl = $"source?source_id={sourceId}&{GetCommonUrlSegments()}";
            Result<SourcesResponseDto> result = await _fredClient.GetFromJsonAsync<SourcesResponseDto>(sourceUrl);
            if (result.IsSuccess)
            {
                SourcesResponseDto? sourceResponse = result.Value;
                if (sourceResponse == null || sourceResponse.Sources == null || sourceResponse.Sources.Count == 0)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success(sourceResponse.Sources[0]);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
        public async Task<Result<ReleasesResponseDto>> GetReleases(int sourceId)
        {
            string releasesUrl = $"source/releases?source_id={sourceId}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<ReleasesResponseDto>(releasesUrl);
        }
    }
}
