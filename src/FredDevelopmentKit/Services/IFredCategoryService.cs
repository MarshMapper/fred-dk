using FredDevelopmentKit.Models;

namespace FredDevelopmentKit.Services
{
    public interface IFredCategoryService
    {
        Task<Category?> GetCategory(int categoryId);
        Task<List<Category>?> GetChildCategories(int categoryId);
        Task<List<Category>?> GetRelatedCategories(int categoryId);
        Task<CategorySeriesResponseDto?> GetSeries(int categoryId);
        Task<CategoryTagsResponseDto?> GetTags(int categoryId);
        Task<CategoryTagsResponseDto?> GetRelatedTags(int categoryId, List<string> searchTagNames);
        void SetApiKey(string apiKey);
    }
}
