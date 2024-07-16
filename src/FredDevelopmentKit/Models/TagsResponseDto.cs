using System.Text.Json.Serialization;

namespace FredDevelopmentKit.Models
{
    // response from the category/tags and category/related_tags endpoint
    public class TagsResponseDto : CommonResponseDto
    {
        [JsonPropertyName("tags")]
        public List<TagDto> Tags { get; set; }
    }
}
