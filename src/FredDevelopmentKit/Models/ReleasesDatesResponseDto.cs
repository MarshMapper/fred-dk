
namespace FredDevelopmentKit.Models
{
    public class ReleasesDatesResponseDto : CommonResponseDto
    {
        public List<ReleasesDate> ReleaseDates { get; set; } = new List<ReleasesDate>();
    }
}
