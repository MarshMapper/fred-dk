using System.Text.Json.Serialization;

namespace FredDevelopmentKit.Models
{
    // response from the category/tags and category/related_tags endpoint
    public class CategoryTagsResponseDto
    {
        public string RealtimeStart { get; set; }
        public string RealtimeEnd { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
        public int Count { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        [JsonPropertyName("tags")]
        public List<CategoryTagDto> Tags { get; set; }
    }
}
