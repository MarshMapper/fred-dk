using Ardalis.Result;
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

        public async Task<Result<Category>> GetCategory(int categoryId)
        {
            string categoryUrl = $"category?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            Result<CategoryResponseDto> result = await _fredClient.GetFromJsonAsync<CategoryResponseDto>(categoryUrl);
            if (result.IsSuccess)
            {
                CategoryResponseDto? categoryResponse = result.Value;
                if (categoryResponse == null || categoryResponse.Categories == null || categoryResponse.Categories.Count == 0)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success<Category>(categoryResponse.Categories[0]);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
        public async Task<Result<List<Category>>> GetChildCategories(int categoryId)
        {
            string childCategoriesUrl = $"category/children?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            Result<CategoryResponseDto> result = await _fredClient.GetFromJsonAsync<CategoryResponseDto>(childCategoriesUrl);
            if (result.IsSuccess)
            {
                CategoryResponseDto? categoryResponse = result.Value;
                if (categoryResponse == null || categoryResponse.Categories == null || categoryResponse.Categories.Count == 0)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success<List<Category>>(categoryResponse.Categories);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
        public async Task<Result<List<Category>>> GetRelatedCategories(int categoryId)
        {
            string relatedCategoriesUrl = $"category/related?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            Result<CategoryResponseDto> result = await _fredClient.GetFromJsonAsync<CategoryResponseDto>(relatedCategoriesUrl);
            if (result.IsSuccess)
            {
                CategoryResponseDto? categoryResponse = result.Value;
                if (categoryResponse == null || categoryResponse.Categories == null || categoryResponse.Categories.Count == 0)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success<List<Category>>(categoryResponse.Categories);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
        public async Task<Result<CategorySeriesResponseDto>> GetSeries(int categoryId)
        {
            string childCategoriesUrl = $"category/series?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            return await _fredClient.GetFromJsonAsync<CategorySeriesResponseDto>(childCategoriesUrl);
        }
        public async Task<Result<CategoryTagsResponseDto>> GetTags(int categoryId)
        {
            string childCategoriesUrl = $"category/tags?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            return await _fredClient.GetFromJsonAsync<CategoryTagsResponseDto>(childCategoriesUrl);
        }
        public async Task<Result<CategoryTagsResponseDto>> GetRelatedTags(int categoryId, List<string> searchTagNames)
        {
            string childCategoriesUrl = $"category/related_tags?category_id={categoryId}&api_key={_apiKey}&file_type=json";
            if (searchTagNames != null && searchTagNames.Count > 0)
            {
                childCategoriesUrl += "&tag_names=" + String.Join(';', searchTagNames);
            }

            return await _fredClient.GetFromJsonAsync<CategoryTagsResponseDto>(childCategoriesUrl);
        }
    }
}
