using System.Text.Json.Serialization;

namespace FredDevelopmentKit.Models
{
    public class UpdateResponseDto : CommonResponseDto
    {
        [JsonPropertyName("seriess")]
        public List<SeriesDto> Series { get; set; }
    }
}
