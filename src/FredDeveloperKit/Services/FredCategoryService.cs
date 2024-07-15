using FredDeveloperKit.Configuration;
using FredDeveloperKit.Models;
using System.Net.Http.Json;

namespace FredDeveloperKit.Services
{
    public class FredCategoryService : IFredCategoryService
    {
        private readonly HttpClient _httpClient;
        private string _apiKey = "";

        public FredCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.stlouisfed.org/fred/");
        }
        public void SetApiKey(string apiKey)
        {
            this._apiKey = apiKey;
        }

        public async Task<List<Category>?> GetChildCategories(int categoryId)
        {
            string childCategoriesUrl = $"category/children?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            CategoryResponseDto? categoryResponse = await _httpClient.GetFromJsonAsync<CategoryResponseDto?>(childCategoriesUrl);
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
            CategoryResponseDto? categoryResponse = await _httpClient.GetFromJsonAsync<CategoryResponseDto?>(categoryUrl);
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
