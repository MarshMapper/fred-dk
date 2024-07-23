using Microsoft.Extensions.Options;
using Ardalis.Result;
using FredDevelopmentKit.Configuration;
using FredDevelopmentKit.Models;

namespace FredDevelopmentKit.Services
{
    public class FredTagsService : FredService, IFredTagsService
    {
        public FredTagsService(IFredHttpClient fredClient, IOptions<FredClientOptions> options) :
            base(fredClient, options)
        {
        }
        public async Task<Result<TagsResponseDto>> GetTags()
        {
            string tagsUrl = $"tags?{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<TagsResponseDto>(tagsUrl);
        }
        public async Task<Result<TagsResponseDto>> GetRelatedTags(List<string> tagNames)
        {
            string relatedTagsUrl = $"related_tags?tag_names={string.Join(";", tagNames)}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<TagsResponseDto>(relatedTagsUrl);
        }
        public async Task<Result<RelatedSeriesResponseDto>> GetSeries(List<string> tagNames)
        {
            string seriesUrl = $"tags/series?tag_names={string.Join(";", tagNames)}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<RelatedSeriesResponseDto>(seriesUrl);
        }
    }
}
