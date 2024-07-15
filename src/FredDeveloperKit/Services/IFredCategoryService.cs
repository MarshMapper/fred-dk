using FredDeveloperKit.Models;

namespace FredDeveloperKit.Services
{
    public interface IFredCategoryService
    {
        Task<List<Category>?> GetChildCategories(int categoryId);
        Task<Category?> GetCategory(int categoryId);
    }
}
