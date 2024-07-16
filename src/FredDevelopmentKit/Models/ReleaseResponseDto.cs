namespace FredDevelopmentKit.Models
{
    public class ReleaseResponseDto 
    {
        public string RealtimeStart { get; set; }
        public string RealtimeEnd { get; set; }
        public List<Release> Releases { get; set; } = new List<Release>();
    }
}
