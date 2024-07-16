using System.Text.Json.Serialization;
namespace FredDevelopmentKit.Models
{
    // response from the category/series endpoint
    public class CategorySeriesResponseDto
    { 
        public string RealtimeStart { get; set; }
        public string RealtimeEnd { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
        public int Count { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        [JsonPropertyName("seriess")]
        public List<CategorySeriesDto> Series { get; set; }
    }
}
