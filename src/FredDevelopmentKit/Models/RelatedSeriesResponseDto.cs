using System.Text.Json.Serialization;
namespace FredDevelopmentKit.Models
{
    // response from the category/series endpoint
    public class RelatedSeriesResponseDto : CommonResponseDto
    { 
        [JsonPropertyName("seriess")]
        public List<SeriesDto> Series { get; set; }
    }
}
