
namespace FredDevelopmentKit.Models
{
    public class ReleaseDateResponseDto : CommonResponseDto
    {
        public List<ReleaseDate> ReleaseDates { get; set; } = new List<ReleaseDate>();
    }
}
