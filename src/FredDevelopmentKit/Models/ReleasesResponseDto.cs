namespace FredDevelopmentKit.Models
{
    public class ReleasesResponseDto : CommonResponseDto
    {
        public List<Release> Releases { get; set; } = new List<Release>();
    }
}
