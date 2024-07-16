namespace FredDevelopmentKit.Models
{
    public class SourcesResponseDto 
    {
        public string RealtimeStart { get; set; }
        public string RealtimeEnd { get; set; }
        public List<SourceDto> Sources { get; set; } = new List<SourceDto>();
    }
}
