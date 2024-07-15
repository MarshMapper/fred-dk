using FredDevelopmentKit.Models;

namespace FredDevelopmentKit.Services
{
    public interface IFredCategoryService
    {
        Task<List<Category>?> GetChildCategories(int categoryId);
        Task<Category?> GetCategory(int categoryId);
        void SetApiKey(string apiKey);
    }
}
