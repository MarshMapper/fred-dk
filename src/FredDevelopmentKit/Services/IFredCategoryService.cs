using Ardalis.Result;
using FredDevelopmentKit.Models;

namespace FredDevelopmentKit.Services
{
    public interface IFredCategoryService
    {
        Task<Result<Category>> GetCategory(int categoryId);
        Task<Result<List<Category>>> GetChildCategories(int categoryId);
        Task<Result<List<Category>>> GetRelatedCategories(int categoryId);
        Task<Result<SeriesResponseDto>> GetSeries(int categoryId);
        Task<Result<TagsResponseDto>> GetTags(int categoryId);
        Task<Result<TagsResponseDto>> GetRelatedTags(int categoryId, List<string> searchTagNames);
        void SetApiKey(string apiKey);
    }
}
