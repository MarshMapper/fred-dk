﻿using System.Text.Json.Serialization;

namespace FredDevelopmentKit.Models
{
    // response from the category/series endpoint
    public class SeriesResponseDto 
    {
        public string RealtimeStart { get; set; }
        public string RealtimeEnd { get; set; }
        [JsonPropertyName("seriess")]
        public List<SeriesDto> Series { get; set; }
    }
}
