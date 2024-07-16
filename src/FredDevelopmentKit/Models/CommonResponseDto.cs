
namespace FredDevelopmentKit.Models
{
    public class CommonResponseDto
    {
        public string RealtimeStart { get; set; }
        public string RealtimeEnd { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
        public int Count { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
