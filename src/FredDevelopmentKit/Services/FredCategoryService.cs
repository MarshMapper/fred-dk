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
        public async Task<List<Category>?> GetRelatedCategories(int categoryId)
        {
            string childCategoriesUrl = $"category/related?category_id={categoryId}&api_key={_apiKey}&file_type=json";
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
        public async Task<CategorySeriesResponseDto?> GetSeries(int categoryId)
        {
            string childCategoriesUrl = $"category/series?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            return await _fredClient.GetFromJsonAsync<CategorySeriesResponseDto?>(childCategoriesUrl);
        }
        public async Task<CategoryTagsResponseDto?> GetTags(int categoryId)
        {
            string childCategoriesUrl = $"category/tags?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            return await _fredClient.GetFromJsonAsync<CategoryTagsResponseDto?>(childCategoriesUrl);
        }
        public async Task<CategoryTagsResponseDto?> GetRelatedTags(int categoryId, List<string> searchTagNames)
        {
            string childCategoriesUrl = $"category/related_tags?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            if (searchTagNames != null && searchTagNames.Count > 0)
            {
                childCategoriesUrl += "&tag_names=" + String.Join(';', searchTagNames);
            }

            return await _fredClient.GetFromJsonAsync<CategoryTagsResponseDto?>(childCategoriesUrl);
        }
    }
}
