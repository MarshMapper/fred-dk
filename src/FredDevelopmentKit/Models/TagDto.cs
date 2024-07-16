
namespace FredDevelopmentKit.Models
{
    // one of the tags returned from the category/tags and category/related_tags endpoint
    public class TagDto
    {
        public string Name { get; set; }
        public string GroupId { get; set; }
        public string Notes { get; set; }
        public string Created { get; set; }
        public int Popularity { get; set; }
        public int SeriesCount { get; set; }
    }
}
