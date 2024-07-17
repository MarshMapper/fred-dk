namespace FredDevelopmentKit.Models
{
    // one econmic series
    public class SeriesDto
    {
        public string Id { get; set; }
        public string RealtimeStart { get; set; }
        public string RealtimeEnd { get; set; }
        public string Title { get; set; }
        public string ObservationStart { get; set; }
        public string ObservationEnd { get; set; }
        public string Frequency { get; set; }
        public string FrequencyShort { get; set; }
        public string Units { get; set; }
        public string UnitsShort { get; set; }
        public string SeasonalAdjustment { get; set; }
        public string SeasonalAdjustmentShort { get; set; }
        public string LastUpdated { get; set; }
        public int Popularity { get; set; }
        public int GroupPopularity { get; set; }
        public string Notes { get; set; }
    }
}
