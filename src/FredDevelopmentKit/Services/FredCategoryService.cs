using FredDevelopmentKit.Models;
using System.Net.Http.Json;

namespace FredDevelopmentKit.Services
{
    public class FredCategoryService : IFredCategoryService
    {
        private readonly FredHttpClient _fredClient;
        private string _apiKey = "";

        public FredCategoryService(FredHttpClient fredClient)
        {
            _fredClient = fredClient;
        }
        public void SetApiKey(string apiKey)
        {
            this._apiKey = apiKey;
        }

        public async Task<List<Category>?> GetChildCategories(int categoryId)
        {
            string childCategoriesUrl = $"category/children?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            CategoryResponseDto? categoryResponse = await _fredClient.GetFromJsonAsync<CategoryResponseDto?>(childCategoriesUrl);
            if (categoryResponse == null || categoryResponse.Categories == null || categoryResponse.Categories.Count == 0)
            {
                return null;
            }
            else
            {
                return categoryResponse.Categories;
            }
        }

        public async Task<Category?> GetCategory(int categoryId)
        {
            string categoryUrl = $"category?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            CategoryResponseDto? categoryResponse = await _fredClient.GetFromJsonAsync<CategoryResponseDto?>(categoryUrl);
            if (categoryResponse == null || categoryResponse.Categories == null || categoryResponse.Categories.Count == 0)
            {
                return null;
            }
            else
            {
                return categoryResponse.Categories[0];
            }
        }
    }
}
