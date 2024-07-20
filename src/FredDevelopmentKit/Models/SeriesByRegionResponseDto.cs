using System.Text.Json.Serialization;

namespace FredDevelopmentKit.Models
{
    public class SeriesByRegionResponseDto
    {
        [JsonPropertyName("meta")]
        public SeriesByRegion SeriesByRegion { get; set; }
    }
}
