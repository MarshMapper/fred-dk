namespace FredDevelopmentKit.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Parent_id { get; set; }
    }
}
