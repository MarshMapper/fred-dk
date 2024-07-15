namespace FredDeveloperKit.Models
{
    // responses from the Fred API are wrapped in a JSON object called "categories" that
    // contains an array of "category" objects, even if only one category is requested
    public class CategoryResponseDto
    {
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
