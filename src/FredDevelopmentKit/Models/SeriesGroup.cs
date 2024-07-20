using System.Text.Json.Serialization;

namespace FredDevelopmentKit.Models
{
    public class SeriesGroup
    {
        public string Title { get; set; }
        public string RegionType { get; set; }
        [JsonPropertyName("series_group")]
        public string SeriesGroupId { get; set; }
        public string Season { get; set; }
        public string Units { get; set; }
        public string Frequency { get; set; }
        public string MinDate { get; set; }
        public string MaxDate { get; set; }
    }
}
