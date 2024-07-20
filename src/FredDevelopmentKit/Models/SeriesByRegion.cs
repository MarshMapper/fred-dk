using System.Text.Json.Serialization;
namespace FredDevelopmentKit.Models
{
    public class SeriesByRegion
    {
        public string Title { get; set; }
        [JsonPropertyName("region")]
        public string RegionType { get; set; }
        public string Seasonality { get; set; }
        public string Units { get; set; }
        public string Frequency { get; set; }
        public string Date { get; set; }
        [JsonPropertyName("data")]
        public RegionValue[] SeriesRegionValues { get; set; }
    }
}
